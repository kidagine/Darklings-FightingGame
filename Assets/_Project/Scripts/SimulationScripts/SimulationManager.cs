using SharedGame;
using System.Collections.Generic;
using UnityGGPO;

public class SimulationManager : GameManager
{
    public override void StartLocalGame()
    {
        StartGame(new LocalRunner(new GameSimulation(GameplayManager.Instance.GetPlayerStats(), GameplayManager.Instance.GetAssistStats())));
    }

    public override void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex)
    {
        var game = new GGPORunner("darklings", new GameSimulation(GameplayManager.Instance.GetPlayerStats(), GameplayManager.Instance.GetAssistStats()), perfPanel);
        game.Init(connections, playerIndex);
        StartGame(game);
    }
}
