using System;
using UnityEngine;

[Serializable]
public enum StageTypeEnum { Grid, TheVoid, Graveyard, Arena, Bridge, Temple, Ruins };

public class StageType : MonoBehaviour
{
    [SerializeField] private StageTypeEnum _stageTypeEnum = default;

    public StageTypeEnum StageTypeEnum { get { return _stageTypeEnum; } private set { } }
}