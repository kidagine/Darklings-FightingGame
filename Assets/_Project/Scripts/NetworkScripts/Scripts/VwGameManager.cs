using SharedGame;
using System.Collections.Generic;
using UnityEngine;
using UnityGGPO;

public class VwGameManager : GameManager
{
    public override void StartLocalGame()
    {
        StartGame(new LocalRunner(new VwGame(2)));
    }

    public override void StartGGPOGame(IPerfUpdate perfPanel, IList<Connections> connections, int playerIndex)
    {
        var game = new GGPORunner("darklings", new VwGame(connections.Count), perfPanel);
        game.Init(connections, playerIndex);
        StartGame(game);
    }
}
