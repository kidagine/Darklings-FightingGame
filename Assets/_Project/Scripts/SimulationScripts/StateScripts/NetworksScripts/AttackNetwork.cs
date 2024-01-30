using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct AttackNetwork
{
    public CameraShakerNetwork cameraShakerNetwork;

    public DemonVector2 travelDistance;
    public DemonFloat knockbackForce;
    public AttackTypeEnum attackType;
    public ComboTimerStarterEnum comboTimerStarter;
    public DemonVector2 projectilePosition;
    public int knockbackDuration;
    public int knockbackArc;
    public int hitstop;
    public int damage;
    public int hitStun;
    public int blockStun;
    public int projectilePriority;
    public int startup;
    public int active;
    public int recovery;
    public bool teleport;
    public DemonVector2 teleportPosition;
    public DemonFloat projectileSpeed;
    public string name;
    public string moveMaterial;
    public string moveName;
    public string attackSound;
    public string impactSound;
    public string hurtEffect;
    public bool jumpCancelable;
    public bool softKnockdown;
    public bool hardKnockdown;
    public bool guardBreak;
    public int superArmor;
    public bool projectileDestroyOnHit;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write((float)travelDistance.x);
        bw.Write((float)travelDistance.y);
        bw.Write((float)projectilePosition.x);
        bw.Write((float)projectilePosition.y);
        bw.Write((float)knockbackForce);
        bw.Write((int)comboTimerStarter);
        bw.Write((int)attackType);
        bw.Write(knockbackDuration);
        bw.Write(moveMaterial);
        bw.Write(hitstop);
        bw.Write(damage);
        bw.Write(knockbackArc);
        bw.Write(hitStun);
        bw.Write(blockStun);
        bw.Write(projectilePriority);
        bw.Write((float)projectileSpeed);
        bw.Write(projectileDestroyOnHit);
        bw.Write(name);
        bw.Write(moveName);
        bw.Write(attackSound);
        bw.Write(impactSound);
        bw.Write(hurtEffect);
        bw.Write(jumpCancelable);
        bw.Write(softKnockdown);
        bw.Write(hardKnockdown);
        bw.Write(superArmor);
        bw.Write(guardBreak);
        cameraShakerNetwork.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        travelDistance.x = (DemonFloat)br.ReadSingle();
        travelDistance.y = (DemonFloat)br.ReadSingle();
        projectilePosition.x = (DemonFloat)br.ReadSingle();
        projectilePosition.y = (DemonFloat)br.ReadSingle();
        knockbackForce = (DemonFloat)br.ReadSingle();
        comboTimerStarter = (ComboTimerStarterEnum)br.ReadInt32();
        attackType = (AttackTypeEnum)br.ReadInt32();
        knockbackDuration = br.ReadInt32();
        hitstop = br.ReadInt32();
        damage = br.ReadInt32();
        knockbackArc = br.ReadInt32();
        hitStun = br.ReadInt32();
        blockStun = br.ReadInt32();
        projectilePriority = br.ReadInt32();
        projectileSpeed = (DemonFloat)br.ReadSingle();
        projectileDestroyOnHit = br.ReadBoolean();
        moveMaterial = br.ReadString();
        name = br.ReadString();
        moveName = br.ReadString();
        attackSound = br.ReadString();
        impactSound = br.ReadString();
        hurtEffect = br.ReadString();
        jumpCancelable = br.ReadBoolean();
        softKnockdown = br.ReadBoolean();
        hardKnockdown = br.ReadBoolean();
        superArmor = br.ReadInt32();
        guardBreak = br.ReadBoolean();
        cameraShakerNetwork.Deserialize(br);
    }
};