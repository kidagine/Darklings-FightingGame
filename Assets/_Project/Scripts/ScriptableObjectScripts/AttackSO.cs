using System;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack", order = 2)]
public class AttackSO : ScriptableObject
{
    [Header("Main")]
    public int damage;
    public AttackTypeEnum attackTypeEnum;
    public int hitStun;
    public int blockStun;
    public Vector2 travelDistance;
    public Vector2 knockbackForce;
    public int knockbackDuration;
    public int knockbackArc;

    [Header("Properties")]
    public int superArmor;
    public bool isAirAttack;
    public bool isProjectile;
    public bool isArcana;
    public bool jumpCancelable;
    public bool causesKnockdown;
    public bool causesSoftKnockdown;
    [Header("Sounds")]
    public string attackSound;
    public string impactSound;
    [Header("Visuals")]
    [Range(0, 150)]
    public int hitstop;
    public GameObject hitEffect;
    public Vector2 hitEffectPosition;
    public float hitEffectRotation;
    public string hurtEffect;
    public string moveMaterial;

    [HideInInspector] public Vector2 hurtEffectPosition;
    public float hurtEffectRotation;
    public CameraShakerSO cameraShaker;
    [Header("Information")]
    public string moveName;
    [TextArea(5, 7)]
    public string moveDescription;
    public VideoClip moveVideo;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(travelDistance.x);
        bw.Write(travelDistance.y);
        bw.Write(damage);
    }

    public void Deserialize(BinaryReader br)
    {
        travelDistance.x = br.ReadSingle();
        travelDistance.y = br.ReadSingle();
        damage = br.ReadInt32();
    }
}

public class ResultAttack
{
    public int startUpFrames;
    public int activeFrames;
    public int recoveryFrames;
    public AttackTypeEnum attackTypeEnum;
    public int damage;
    public int comboDamage;
}