using Demonics.Manager;
using SharedGame;
using System;
using UnityEngine;

public class VwGameView : MonoBehaviour, IGameView
{
    [SerializeField] private Player _player = default;
    [SerializeField] private TrainingMenu _trainingMenu = default;
    private Player[] playerViews = Array.Empty<Player>();
    private GameManager gameManager => GameManager.Instance;

    private void SetGame(VwGame gs)
    {
        var playersGss = VwGame._players;
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
        var playersGss = VwGame._players;
        if (playerViews.Length != playersGss.Length)
        {
            SetGame(game);
        }
        for (int i = 0; i < playersGss.Length; ++i)
        {
            playerViews[i].Populate(playersGss[i], gameInfo.players[i]);
            UpdateEffects(i, playersGss[i].effects);
            _trainingMenu.SetState(true, playersGss[0].state);
            _trainingMenu.SetState(false, playersGss[1].state);
        }
    }
    private void UpdateEffects(int index, EffectNetwork[] effects)
    {
        for (int i = 0; i < effects.Length; i++)
        {
            GameObject[] effectObjects = ObjectPoolingManager.Instance.GetPool(index, effects[i].name);
            if (effectObjects.Length > 0)
            {
                for (int j = 0; j < effectObjects.Length; j++)
                {
                    effectObjects[j].SetActive(effects[i].effectGroups[j].active);
                    if (!effectObjects[j].activeSelf)
                    {
                        if (effectObjects[j].TryGetComponent(out PlayerGhost playerGhost))
                        {
                            playerGhost.SetSprite(playerViews[index].PlayerAnimator.GetCurrentSprite());
                        }
                    }
                    else
                    {
                        effectObjects[j].transform.position = effects[i].effectGroups[j].position;
                        effectObjects[j].GetComponent<SpriteRenderer>().flipX = effects[i].effectGroups[j].flip;
                        if (!effectObjects[j].TryGetComponent(out PlayerGhost playerGhost))
                        {
                            effectObjects[j].GetComponent<DemonicsAnimator>().SetAnimation("Idle", effects[i].effectGroups[j].animationFrames);
                        }
                    }
                }
            }
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
