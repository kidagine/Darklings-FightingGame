using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct InputItemNetwork
{
    public int frame;
    public InputEnum inputEnum;
    public Vector2Int inputDirection;
    public bool pressed;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(frame);
        bw.Write((int)inputEnum);
        bw.Write((int)inputDirection.x);
        bw.Write((int)inputDirection.y);
        bw.Write(pressed);
    }

    public void Deserialize(BinaryReader br)
    {
        frame = br.ReadInt32();
        inputEnum = (InputEnum)br.ReadInt32();
        inputDirection.x = br.ReadInt32();
        inputDirection.y = br.ReadInt32();
        pressed = br.ReadBoolean();
    }
};