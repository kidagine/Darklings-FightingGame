using System;
using System.IO;

[Serializable]
public struct InputHistoryNetwork
{
    public InputItemNetwork[] InputItems;
    public int length;

    public void Serialize(BinaryWriter bw)
    {
        for (int i = 0; i < InputItems.Length; i++)
            InputItems[i].Serialize(bw);
        bw.Write(length);
    }

    public void Deserialize(BinaryReader br)
    {
        for (int i = 0; i < InputItems.Length; i++)
            InputItems[i].Deserialize(br);
        length = br.ReadInt32();
    }
}