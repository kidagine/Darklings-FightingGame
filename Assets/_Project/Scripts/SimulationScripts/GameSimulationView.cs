using Demonics.Manager;
using SharedGame;
using System;
using UnityEngine;

public class GameSimulationView : MonoBehaviour, IGameView
{
    [SerializeField] private Player _player = default;
    [SerializeField] private TrainingMenu _trainingMenu = default;
    private Player[] playerViews = Array.Empty<Player>();
    private GameManager gameManager => GameManager.Instance;

    private void SetGame(GameSimulation gs)
    {
        var playersGss = GameSimulation._players;
        playerViews = new Player[playersGss.Length];
        playerViews[0] = Instantiate(_player);
        playerViews[1] = Instantiate(_player);
        GameplayManager.Instance.InitializePlayers(playerViews[0].gameObject, playerViews[1].gameObject);
    }

    public void UpdateGameView(IGameRunner runner)
    {
        GameSimulation game = (GameSimulation)runner.Game;
        GameInfo gameInfo = runner.GameInfo;
        var playersGss = GameSimulation._players;
        if (playerViews.Length != playersGss.Length)
        {
            SetGame(game);
        }
        if (GameSimulation.Start)
        {
            GameplayManager.Instance.SetupGame();
            GameSimulation.Start = false;
        }
        for (int i = 0; i < playersGss.Length; ++i)
        {
            playerViews[i].PlayerSimulation.Simulate(playersGss[i], gameInfo.players[i]);
            UpdateEffects(i, playersGss[i].effects);
            UpdateProjectiles(i, playersGss[i].projectiles);
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
                        effectObjects[j].transform.position = new Vector2((int)effects[i].effectGroups[j].position.x, (int)effects[i].effectGroups[j].position.y);
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
    private void UpdateProjectiles(int index, ProjectileNetwork[] projectiles)
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            GameObject[] projectileObjects = ObjectPoolingManager.Instance.GetProjectilePool(index, projectiles[i].name);
            if (projectileObjects.Length > 0)
            {
                projectileObjects[i].SetActive(projectiles[i].active);
                if (projectiles[i].active)
                {
                    projectileObjects[i].transform.position = new Vector2((int)projectiles[i].position.x, (int)projectiles[i].position.y);
                    projectileObjects[i].GetComponent<SpriteRenderer>().flipX = projectiles[i].flip;
                    projectileObjects[i].GetComponent<DemonicsAnimator>().SetAnimation("Idle", projectiles[i].animationFrames);
                    projectileObjects[i].transform.GetChild(0).GetComponent<CollisionVisualizer>().ShowBox(projectiles[i].hitbox);
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
