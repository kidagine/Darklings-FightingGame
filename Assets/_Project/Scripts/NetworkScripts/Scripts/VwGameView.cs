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
            UpdateEffects(playersGss[i].jumpEffect);
        }
    }
    private void UpdateEffects(JumpEffectNetwork jumpEffect)
    {
        GameObject effect = ObjectPoolingManager.Instance.GetObject("Jump_effect");
        effect.SetActive(jumpEffect.active);
        effect.transform.position = jumpEffect.position;
        effect.GetComponent<DemonicsAnimator>().SetAnimation("Idle", jumpEffect.animationFrames);
    }
    private void Update()
    {
        if (gameManager.IsRunning)
        {
            UpdateGameView(gameManager.Runner);
        }
    }
}
