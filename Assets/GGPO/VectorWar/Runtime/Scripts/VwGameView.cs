using SharedGame;
using System;
using UnityEngine;

namespace VectorWar
{

    public class VwGameView : MonoBehaviour, IGameView
    {
        public VwShipView shipPrefab;
        // [SerializeField] private Player _player = default;
        private VwShipView[] shipViews = Array.Empty<VwShipView>();
        private GameManager gameManager => GameManager.Instance;

        private void ResetView(VwGame gs)
        {
            var shipGss = gs._ships;
            shipViews = new VwShipView[shipGss.Length];
            for (int i = 0; i < shipGss.Length; ++i)
            {
                shipViews[i] = Instantiate(shipPrefab, transform);
            }
        }

        public void UpdateGameView(IGameRunner runner)
        {
            var gs = (VwGame)runner.Game;
            var ngs = runner.GameInfo;

            var shipsGss = gs._ships;
            if (shipViews.Length != shipsGss.Length)
            {
                ResetView(gs);
            }
            for (int i = 0; i < shipsGss.Length; ++i)
            {
                shipViews[i].Populate(shipsGss[i], ngs.players[i]);
            }
        }

        private void UpdateBullets(Bullet[] bullets, Transform[] bulletList)
        {
            for (int j = 0; j < bulletList.Length; ++j)
            {
                bulletList[j].position = bullets[j].position;
                bulletList[j].rotation = Quaternion.LookRotation(bullets[j].velocity, Vector3.up);
                bulletList[j].gameObject.SetActive(bullets[j].active);
            }
        }

        private void Update()
        {
            if (gameManager.IsRunning)
            {
                UpdateGameView(gameManager.Runner);
            }
        }
    }
}