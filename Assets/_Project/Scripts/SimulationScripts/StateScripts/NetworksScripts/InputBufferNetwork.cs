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

    public void AddInputItem(InputItemNetwork inputItem)
    {
        index++;
        if (index >= inputItems.Length)
        {
            index = 0;
        }
        inputItems[index] = inputItem;
    }

    public InputItemNetwork CurrentInput()
    {
        int storedIndex = index;
        if (storedIndex >= 0 && storedIndex < inputItems.Length)
        {
            while (inputItems[storedIndex].inputEnum == InputEnum.Direction)
            {
                if (storedIndex == 0)
                {
                    break;
                }
                storedIndex--;
            }
        }
        return inputItems[storedIndex];
    }
};