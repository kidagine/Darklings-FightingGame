using SharedGame;
using System;
using UnityEngine;


public class VwGameView : MonoBehaviour, IGameView
{
    public VwShipView shipPrefab;
    [SerializeField] private Player _player = default;
    private VwShipView[] shipViews = Array.Empty<VwShipView>();
    private Player[] playerViews = Array.Empty<Player>();
    private GameManager gameManager => GameManager.Instance;

    private void ResetView(VwGame gs)
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
            ResetView(game);
        }
        playerViews[0].Populate(playersGss[0], gameInfo.players[0]);
        playerViews[1].Populate(playersGss[1], gameInfo.players[1]);
    }

    private void Update()
    {
        if (gameManager.IsRunning)
        {
            UpdateGameView(gameManager.Runner);
        }
    }
}
