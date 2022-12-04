using SharedGame;
using System;
using UnityEngine;

namespace VectorWar
{

    public class VwGameView : MonoBehaviour, IGameView
    {
        public VwShipView shipPrefab;
        [SerializeField] private Player _player = default;
        private VwShipView[] shipViews = Array.Empty<VwShipView>();
        private Player[] playerViews = Array.Empty<Player>();
        private GameManager gameManager => GameManager.Instance;

        private void ResetView(VwGame gs)
        {
            var shipGss = gs._ships;
            shipViews = new VwShipView[shipGss.Length];
            playerViews = new Player[shipGss.Length];
            for (int i = 0; i < shipGss.Length; ++i)
            {
                shipViews[i] = Instantiate(shipPrefab, transform);
            }
            playerViews[0] = Instantiate(_player);
            playerViews[1] = Instantiate(_player);
            GameplayManager.Instance.InitializePlayers(playerViews[0].gameObject, playerViews[1].gameObject);
            GameplayManager.Instance.SetupGame();
        }

        public void UpdateGameView(IGameRunner runner)
        {
            VwGame game = (VwGame)runner.Game;
            GameInfo gameInfo = runner.GameInfo;

            var shipsGss = game._ships;
            if (shipViews.Length != shipsGss.Length)
            {
                ResetView(game);
            }
            for (int i = 0; i < shipsGss.Length; ++i)
            {
                shipViews[i].Populate(shipsGss[i], gameInfo.players[i]);
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