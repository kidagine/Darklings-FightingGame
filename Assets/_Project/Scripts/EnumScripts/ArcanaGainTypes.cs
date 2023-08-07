
using System;
using UnityEngine;

[Serializable]
public enum ArcanaGainTypes { AttackOnHit, AttackOnBlock, DefendOnHit, DefendOnBlock };

public class ArcanaGain : MonoBehaviour
{
    [SerializeField] private ArcanaGainTypes _arcanaGainTypeEnum = default;

    public ArcanaGainTypes ArcanaGainTypes { get { return _arcanaGainTypeEnum; } private set { } }
}