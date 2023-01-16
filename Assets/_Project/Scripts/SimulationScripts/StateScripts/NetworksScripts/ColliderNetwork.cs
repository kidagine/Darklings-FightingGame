using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct ColliderNetwork
{
    public DemonicsVector2 position;
    public DemonicsVector2 size;
    public DemonicsVector2 offset;
    public bool active;
    public bool enter;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write((float)position.x);
        bw.Write((float)position.y);
        bw.Write((float)size.x);
        bw.Write((float)size.y);
        bw.Write((float)offset.x);
        bw.Write((float)offset.y);
        bw.Write(active);
        bw.Write(enter);
    }

    public void Deserialize(BinaryReader br)
    {
        position.x = (DemonicsFloat)br.ReadSingle();
        position.y = (DemonicsFloat)br.ReadSingle();
        size.x = (DemonicsFloat)br.ReadSingle();
        size.y = (DemonicsFloat)br.ReadSingle();
        offset.x = (DemonicsFloat)br.ReadSingle();
        offset.y = (DemonicsFloat)br.ReadSingle();
        active = br.ReadBoolean();
        enter = br.ReadBoolean();
    }
};