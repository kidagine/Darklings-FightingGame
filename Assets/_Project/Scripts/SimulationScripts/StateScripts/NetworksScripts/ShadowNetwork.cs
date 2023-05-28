using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct ShadowNetwork
{
    public AttackSO attack;
    public DemonicsVector2 position;
    public DemonicsVector2 spawnPoint;
    public DemonicsVector2 spawnRotation;
    public bool isOnScreen;
    public int flip;
    public int animationFrames;
    public ProjectileNetwork projectile;
    public bool usedInCombo;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write((float)position.x);
        bw.Write((float)position.y);
        bw.Write((float)spawnPoint.x);
        bw.Write((float)spawnPoint.y);
        bw.Write((float)spawnRotation.x);
        bw.Write((float)spawnRotation.y);
        bw.Write(isOnScreen);
        bw.Write(usedInCombo);
        bw.Write(flip);
        bw.Write(animationFrames);
        projectile.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = (DemonicsFloat)br.ReadSingle();
        position.y = (DemonicsFloat)br.ReadSingle();
        spawnPoint.x = (DemonicsFloat)br.ReadSingle();
        spawnPoint.y = (DemonicsFloat)br.ReadSingle();
        spawnRotation.x = (DemonicsFloat)br.ReadSingle();
        spawnRotation.y = (DemonicsFloat)br.ReadSingle();
        isOnScreen = br.ReadBoolean();
        usedInCombo = br.ReadBoolean();
        flip = br.ReadInt32();
        animationFrames = br.ReadInt32();
        projectile.Deserialize(br);
    }
};
