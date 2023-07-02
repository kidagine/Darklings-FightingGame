using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct ProjectileNetwork
{
    public AttackNetwork attackNetwork;
    public DemonVector2 position;
    public bool active;
    public bool flip;
    public bool hitstop;
    public bool fire;
    public bool destroyOnHit;
    public string name;
    public DemonFloat speed;
    public int priority;
    public int animationFrames;
    public int animationMaxFrames;
    public ColliderNetwork hitbox;


    public void Serialize(BinaryWriter bw)
    {
        bw.Write((float)position.x);
        bw.Write((float)position.y);
        bw.Write((float)speed);
        bw.Write(name);
        bw.Write(priority);
        bw.Write(animationFrames);
        bw.Write(animationMaxFrames);
        bw.Write(active);
        bw.Write(fire);
        bw.Write(flip);
        bw.Write(hitstop);
        bw.Write(destroyOnHit);
        attackNetwork.Serialize(bw);
        hitbox.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = (DemonFloat)br.ReadSingle();
        position.y = (DemonFloat)br.ReadSingle();
        speed = (DemonFloat)br.ReadSingle();
        name = br.ReadString();
        priority = br.ReadInt32();
        animationFrames = br.ReadInt32();
        animationMaxFrames = br.ReadInt32();
        active = br.ReadBoolean();
        fire = br.ReadBoolean();
        flip = br.ReadBoolean();
        hitstop = br.ReadBoolean();
        destroyOnHit = br.ReadBoolean();
        attackNetwork.Deserialize(br);
        hitbox.Deserialize(br);
    }
};
