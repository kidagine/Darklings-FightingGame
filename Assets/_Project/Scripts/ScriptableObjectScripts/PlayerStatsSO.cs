using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Scriptable Objects/Player Stat", order = 1)]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Main")]
    public int characterIndex;
    public Sprite[] portraits;
    public AnimationSO _animation;
    public DialogueSO _dialogue;
    public EffectsLibrarySO _effectsLibrary;
    public EffectsLibrarySO _particlesLibrary;
    public EffectsLibrarySO _projectilesLibrary;
    public CharacterTypeEnum characterName;
    [Header("Stats")]
    public int defenseLevel;
    public int arcanaLevel;
    public int speedLevel;
    public int jumpLevel;
    public int dashLevel;
    public bool canDoubleJump = true;
    public int arcanaRecharge = 1;
    [Header("Moves")]
    public AttackSO m2L;
    public AttackSO m5L;
    public AttackSO m2M;
    public AttackSO m5M;
    public AttackSO m2H;
    public AttackSO m5H;
    public AttackSO jL;
    public AttackSO jM;
    public AttackSO jH;
    public AttackSO mThrow;
    public AttackSO mParry;
    public AttackSO mRedFrenzy;
    public ArcanaSO m5Arcana;
    public ArcanaSO m2Arcana;
    public ArcanaSO jArcana;
    [HideInInspector] public int maxHealth = 10000;

    public int Arcana { get { return arcanaLevel * ARCANA_MULTIPLIER; } set { } }
    public float Defense { get { return (defenseLevel - 1) * 0.05f + 0.95f; } set { } }
    public DemonFloat SpeedWalk
    {
        get
        {
            switch (speedLevel)
            {
                case 1:
                    return (DemonFloat)0.48;
                case 2:
                    return (DemonFloat)0.8;
                case 3:
                    return (DemonFloat)1.12;
                default:
                    return (DemonFloat)0;
            }
        }
        set { }
    }
    public DemonFloat SpeedWalkBackwards
    {
        get
        {
            switch (speedLevel)
            {
                case 1:
                    return (DemonFloat)0.43;
                case 2:
                    return (DemonFloat)0.7;
                case 3:
                    return (DemonFloat)1.0;
                default:
                    return (DemonFloat)0;
            }
        }
        set { }
    }
    public DemonFloat SpeedRun
    {
        get
        {
            switch (speedLevel)
            {
                case 1:
                    return (DemonFloat)2.4;
                case 2:
                    return (DemonFloat)2.88;
                case 3:
                    return (DemonFloat)3.36;
                default:
                    return (DemonFloat)0;
            }
        }
        set { }
    }
    public DemonFloat JumpForce
    {
        get
        {
            switch (jumpLevel)
            {
                case 1:
                    return (DemonFloat)5.44;
                case 2:
                    return (DemonFloat)5.6;
                case 3:
                    return (DemonFloat)5.92;
                default:
                    return (DemonFloat)0;
            }
        }
        set { }
    }
    public DemonFloat DashForce
    {
        get
        {
            switch (dashLevel)
            {
                case 1:
                    return (DemonFloat)3.8;
                case 2:
                    return (DemonFloat)4;
                case 3:
                    return (DemonFloat)4.2;
                default:
                    return (DemonFloat)0;
            }
        }
        set { }
    }
    public DemonFloat DashBackForce
    {
        get
        {
            switch (dashLevel)
            {
                case 1:
                    return (DemonFloat)3.2;
                case 2:
                    return (DemonFloat)3.4;
                case 3:
                    return (DemonFloat)3.6;
                default:
                    return (DemonFloat)0;
            }
        }
        set { }
    }
    public DemonFloat DashAirForce
    {
        get
        {
            switch (dashLevel)
            {
                case 1:
                    return (DemonFloat)3.6;
                case 2:
                    return (DemonFloat)3.8;
                case 3:
                    return (DemonFloat)4.0;
                default:
                    return (DemonFloat)0;
            }
        }
        set { }
    }
    public DemonFloat DashBackAirForce
    {
        get
        {
            switch (dashLevel)
            {
                case 1:
                    return (DemonFloat)3.2;
                case 2:
                    return (DemonFloat)3.4;
                case 3:
                    return (DemonFloat)3.6;
                default:
                    return (DemonFloat)0;
            }
        }
        set { }
    }

    public const int ARCANA_MULTIPLIER = 1000;
}
