using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Replay", menuName = "Scriptable Objects/Replay", order = 1)]
public class ReplaySO : ScriptableObject
{
    public List<ReplayData> replays = new();
}

[Serializable]
public struct ReplayData
{
    //general
    public string version;
    public string date;
    //stage
    public int stage;
    public string music;
    public bool theme;
    //players
    public int playerOneCharacter;
    public int playerOneColor;
    public int playerOneAssist;
    public int playerTwoCharacter;
    public int playerTwoColor;
    public int playerTwoAssist;
    public int skipTime;
    //inputs
    public List<ReplayInput> playerOneInputs;
    public List<ReplayInput> playerTwoInputs;
}
