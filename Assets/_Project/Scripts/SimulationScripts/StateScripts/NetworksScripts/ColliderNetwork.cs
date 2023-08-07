using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct ColliderNetwork
{
    public DemonVector2 position;
    public DemonVector2 size;
    public DemonVector2 offset;
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
        position.x = (DemonFloat)br.ReadSingle();
        position.y = (DemonFloat)br.ReadSingle();
        size.x = (DemonFloat)br.ReadSingle();
        size.y = (DemonFloat)br.ReadSingle();
        offset.x = (DemonFloat)br.ReadSingle();
        offset.y = (DemonFloat)br.ReadSingle();
        active = br.ReadBoolean();
        enter = br.ReadBoolean();
    }
};