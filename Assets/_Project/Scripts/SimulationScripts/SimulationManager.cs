using SharedGame;
using System.Collections.Generic;
using UnityEngine;
using UnityGGPO;

public class SimulationManager : GameManager
{
    public override void StartLocalGame()
    {
        StartGame(new LocalRunner(new GameSimulation(GameplayManager.Instance.GetPlayerStats())));
    }

    public override void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex)
    {
        Debug.Log("a");
        var game = new GGPORunner("darklings", new GameSimulation(GameplayManager.Instance.GetPlayerStats()), perfPanel);
        game.Init(connections, playerIndex);
        StartGame(game);
    }
}
