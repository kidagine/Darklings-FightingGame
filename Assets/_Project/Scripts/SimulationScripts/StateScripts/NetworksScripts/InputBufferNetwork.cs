using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct InputBufferNetwork
{
    public InputItemNetwork[] inputItems;

    public void Serialize(BinaryWriter bw)
    {
        for (int i = 0; i < inputItems.Length; ++i)
        {
            inputItems[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        for (int i = 0; i < inputItems.Length; ++i)
        {
            inputItems[i].Deserialize(br);
        }
    }
};