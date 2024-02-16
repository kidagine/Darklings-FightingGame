using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct ShadowNetwork
{
    public AttackSO attack;
    public DemonVector2 position;
    public DemonVector2 spawnPoint;
    public DemonVector2 spawnRotation;
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
        position.x = (DemonFloat)br.ReadSingle();
        position.y = (DemonFloat)br.ReadSingle();
        spawnPoint.x = (DemonFloat)br.ReadSingle();
        spawnPoint.y = (DemonFloat)br.ReadSingle();
        spawnRotation.x = (DemonFloat)br.ReadSingle();
        spawnRotation.y = (DemonFloat)br.ReadSingle();
        isOnScreen = br.ReadBoolean();
        usedInCombo = br.ReadBoolean();
        flip = br.ReadInt32();
        animationFrames = br.ReadInt32();
        projectile.Deserialize(br);
    }
};
