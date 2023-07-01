using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct InputBufferNetwork
{
    public InputItemNetwork[] triggers;
    public InputItemNetwork[] sequences;
    public int indexTrigger;
    public int indexSequence;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(indexTrigger);
        bw.Write(indexSequence);
        for (int i = 0; i < triggers.Length; ++i)
            triggers[i].Serialize(bw);
        for (int i = 0; i < sequences.Length; ++i)
            sequences[i].Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        indexTrigger = br.ReadInt32();
        indexSequence = br.ReadInt32();
        for (int i = 0; i < triggers.Length; ++i)
            triggers[i].Deserialize(br);
        for (int i = 0; i < sequences.Length; ++i)
            sequences[i].Deserialize(br);
    }

    public void AddTrigger(InputItemNetwork inputItem)
    {
        indexTrigger++;
        if (indexTrigger >= triggers.Length)
            indexTrigger = 0;
        triggers[indexTrigger] = inputItem;
    }

    public void AddSequence(InputItemNetwork inputItem)
    {
        indexSequence++;
        if (indexSequence >= sequences.Length)
            indexSequence = 0;
        sequences[indexSequence] = inputItem;
    }

    public InputItemNetwork CurrentTrigger() => triggers[indexTrigger];
};