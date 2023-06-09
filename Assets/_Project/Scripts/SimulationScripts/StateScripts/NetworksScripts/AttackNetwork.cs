using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct AttackNetwork
{
    public CameraShakerNetwork cameraShakerNetwork;

    public DemonicsVector2 travelDistance;
    public DemonicsFloat knockbackForce;
    public AttackTypeEnum attackType;
    public ComboTimerStarterEnum comboTimerStarter;
    public DemonicsVector2 projectilePosition;
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
    public DemonicsFloat projectileSpeed;
    public string name;
    public string moveName;
    public string attackSound;
    public string impactSound;
    public string hurtEffect;
    public bool jumpCancelable;
    public bool softKnockdown;
    public bool hardKnockdown;
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
        cameraShakerNetwork.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        travelDistance.x = (DemonicsFloat)br.ReadSingle();
        travelDistance.y = (DemonicsFloat)br.ReadSingle();
        projectilePosition.x = (DemonicsFloat)br.ReadSingle();
        projectilePosition.y = (DemonicsFloat)br.ReadSingle();
        knockbackForce = (DemonicsFloat)br.ReadSingle();
        comboTimerStarter = (ComboTimerStarterEnum)br.ReadInt32();
        attackType = (AttackTypeEnum)br.ReadInt32();
        knockbackDuration = br.ReadInt32();
        hitstop = br.ReadInt32();
        damage = br.ReadInt32();
        knockbackArc = br.ReadInt32();
        hitStun = br.ReadInt32();
        blockStun = br.ReadInt32();
        projectilePriority = br.ReadInt32();
        projectileSpeed = (DemonicsFloat)br.ReadSingle();
        projectileDestroyOnHit = br.ReadBoolean();
        name = br.ReadString();
        moveName = br.ReadString();
        attackSound = br.ReadString();
        impactSound = br.ReadString();
        hurtEffect = br.ReadString();
        jumpCancelable = br.ReadBoolean();
        softKnockdown = br.ReadBoolean();
        hardKnockdown = br.ReadBoolean();
        superArmor = br.ReadInt32();
        cameraShakerNetwork.Deserialize(br);
    }
};