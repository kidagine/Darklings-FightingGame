using System;
using System.IO;

[Serializable]
public struct InputItemNetwork
{
    public int time;
    public int frame;
    public InputEnum inputEnum;
    public bool crouch;
    public bool pressed;
    public bool sequence;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(time);
        bw.Write(frame);
        bw.Write((int)inputEnum);
        bw.Write(pressed);
        bw.Write(crouch);
    }

    public void Deserialize(BinaryReader br)
    {
        time = br.ReadInt32();
        frame = br.ReadInt32();
        inputEnum = (InputEnum)br.ReadInt32();
        pressed = br.ReadBoolean();
        crouch = br.ReadBoolean();
    }
};