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
        if (inputItem.sequence)
        {
            indexSequence++;
            if (indexSequence >= sequences.Length)
                indexSequence = 0;
            sequences[indexSequence] = inputItem;
        }
        else
        {
            indexTrigger++;
            if (indexTrigger >= triggers.Length)
                indexTrigger = 0;
            triggers[indexTrigger] = inputItem;
        }
    }

    public readonly bool RecentDownInput()
    {
        bool foundItem = false;
        InputItemNetwork inputItem = default;
        for (int i = sequences.Length; i-- > 0;)
            if (sequences[i].inputEnum == InputEnum.Down)
            {
                foundItem = true;
                inputItem = sequences[i];
                break;
            }
        if (foundItem)
            if (GameSimulation.FramesStatic - inputItem.frame <= 15)
                return true;
        return false;
    }

    public readonly bool RecentTrigger(InputEnum inputEnum)
    {
        bool foundItem = false;
        InputItemNetwork inputItem = default;
        for (int i = triggers.Length; i-- > 0;)
            if (triggers[i].inputEnum == inputEnum)
            {
                foundItem = true;
                inputItem = triggers[i];
                break;
            }
        if (foundItem)
            if (GameSimulation.FramesStatic - inputItem.frame <= 15)
                return true;
        return false;
    }

    public InputItemNetwork CurrentSequence() => sequences[indexSequence];
    private InputItemNetwork PreviousSequence() => sequences[indexSequence - 1];
    public InputItemNetwork CurrentTrigger() => triggers[indexTrigger];
    private InputItemNetwork PreviousTrigger() => triggers[indexTrigger - 1];

    public bool GetBlueFrenzy()
    {
        int previousIndex = indexTrigger - 1;
        if (previousIndex < 0 || previousIndex > triggers.Length - 1)
            return false;
        if (CurrentTrigger().pressed && CurrentTrigger().inputEnum == InputEnum.Medium)
        {
            if (PreviousTrigger().inputEnum == InputEnum.Light)
            {
                int frameDifference = CurrentTrigger().frame - PreviousTrigger().frame;
                if (frameDifference >= 0 && frameDifference <= 1)
                    return true;
            }
        }
        return false;
    }

    public bool GetRedFrenzy()
    {
        int previousIndex = indexTrigger - 1;
        if (previousIndex < 0 || previousIndex > triggers.Length - 1)
            return false;
        if (CurrentTrigger().pressed && CurrentTrigger().inputEnum == InputEnum.Special)
        {
            if (PreviousTrigger().inputEnum == InputEnum.Heavy)
            {
                int frameDifference = CurrentTrigger().frame - PreviousTrigger().frame;
                if (frameDifference >= 0 && frameDifference <= 1)
                    return true;
            }
        }
        return false;
    }
};