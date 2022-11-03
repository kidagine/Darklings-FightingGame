using FixMath.NET;
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
    public CharacterTypeEnum characterName;
    [Header("Stats")]
    public int defenseLevel;
    public int arcanaLevel;
    public int speedLevel;
    public int jumpLevel;
    public int dashLevel;
    public bool canDoubleJump = true;
    public float arcanaRecharge = 1;
    [Header("Moves")]
    public AttackSO m2L;
    public AttackSO m5L;
    public AttackSO m2M;
    public AttackSO m5M;
    public AttackSO m2H;
    public AttackSO m5H;
    public AttackSO jL;
    public AttackSO jM;
    public AttackSO mThrow;
    public AttackSO mParry;
    public AttackSO mRedFrenzy;
    public ArcanaSO m5Arcana;
    public ArcanaSO m2Arcana;
    public ArcanaSO jArcana;
    [HideInInspector] public int maxHealth = 10000;

    public int Arcana { get { return arcanaLevel; } set { } }
    public float Defense { get { return (defenseLevel - 1) * 0.05f + 0.95f; } set { } }
    public Fix64 SpeedWalk
    {
        get
        {
            switch (speedLevel)
            {
                case 1:
                    return (Fix64)0.05;
                case 2:
                    return (Fix64)0.07;
                case 3:
                    return (Fix64)0.09;
                default:
                    return (Fix64)0;
            }
        }
        set { }
    }
    public Fix64 SpeedRun
    {
        get
        {
            switch (speedLevel)
            {
                case 1:
                    return (Fix64)0.15;
                case 2:
                    return (Fix64)0.18;
                case 3:
                    return (Fix64)0.21;
                default:
                    return (Fix64)0;
            }
        }
        set { }
    }
    public Fix64 JumpForce
    {
        get
        {
            switch (jumpLevel)
            {
                case 1:
                    return (Fix64)0.4;
                case 2:
                    return (Fix64)0.4;
                case 3:
                    return (Fix64)0.4;
                default:
                    return (Fix64)0;
            }
        }
        set { }
    }
    public Fix64 DashForce
    {
        get
        {
            switch (dashLevel)
            {
                case 1:
                    return (Fix64)0.15;
                case 2:
                    return (Fix64)0.18;
                case 3:
                    return (Fix64)0.21;
                default:
                    return (Fix64)0;
            }
        }
        set { }
    }
}
