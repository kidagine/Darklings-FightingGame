using Demonics.Manager;
using SharedGame;
using System;
using UnityEngine;

public class VwGameView : MonoBehaviour, IGameView
{
    [SerializeField] private Player _player = default;
    private Player[] playerViews = Array.Empty<Player>();
    private GameManager gameManager => GameManager.Instance;

    private void SetGame(VwGame gs)
    {
        var playersGss = gs._players;
        playerViews = new Player[playersGss.Length];
        playerViews[0] = Instantiate(_player);
        playerViews[1] = Instantiate(_player);
        GameplayManager.Instance.InitializePlayers(playerViews[0].gameObject, playerViews[1].gameObject);
        GameplayManager.Instance.SetupGame();
    }

    public void UpdateGameView(IGameRunner runner)
    {
        VwGame game = (VwGame)runner.Game;
        GameInfo gameInfo = runner.GameInfo;
        var playersGss = game._players;
        if (playerViews.Length != playersGss.Length)
        {
            SetGame(game);
        }
        for (int i = 0; i < playersGss.Length; ++i)
        {
            playerViews[i].Populate(playersGss[i], gameInfo.players[i]);
            UpdateEffects(i, playersGss[i].effect);
        }
    }
    private void UpdateEffects(int index, EffectNetwork effects)
    {
        GameObject[] effectObjects = ObjectPoolingManager.Instance.GetList(index, "effects.jumpEffects[i].name");
        for (int i = 0; i < effectObjects.Length; i++)
        {
            effectObjects[i].SetActive(effects.jumpEffects[i].active);
            effectObjects[i].transform.position = effects.jumpEffects[i].position;
            effectObjects[i].GetComponent<DemonicsAnimator>().SetAnimation("Idle", effects.jumpEffects[i].animationFrames);
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
