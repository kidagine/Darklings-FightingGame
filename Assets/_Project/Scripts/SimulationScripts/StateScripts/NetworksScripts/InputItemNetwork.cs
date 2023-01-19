using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct InputItemNetwork
{
    public int frame;
    public InputEnum inputEnum;
    public InputDirectionEnum inputDirection;
    public bool pressed;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(frame);
        bw.Write((int)inputEnum);
        bw.Write((int)inputDirection);
        bw.Write(pressed);
    }

    public void Deserialize(BinaryReader br)
    {
        frame = br.ReadInt32();
        inputEnum = (InputEnum)br.ReadInt32();
        inputDirection = (InputDirectionEnum)br.ReadInt32();
        pressed = br.ReadBoolean();
    }
};