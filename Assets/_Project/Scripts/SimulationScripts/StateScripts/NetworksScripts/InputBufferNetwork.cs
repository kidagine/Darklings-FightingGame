using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct InputBufferNetwork
{
    public InputItemNetwork[] inputItems;
    public int index;
    public void Serialize(BinaryWriter bw)
    {
        bw.Write(index);
        for (int i = 0; i < inputItems.Length; ++i)
        {
            inputItems[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        index = br.ReadInt32();
        for (int i = 0; i < inputItems.Length; ++i)
        {
            inputItems[i].Deserialize(br);
        }
    }

    public int NextInputIndex()
    {
        index++;
        if (index >= inputItems.Length)
        {
            index = 0;
        }
        return index;
    }
    public InputItemNetwork CurrentInput()
    {
        return inputItems[index];
    }
};