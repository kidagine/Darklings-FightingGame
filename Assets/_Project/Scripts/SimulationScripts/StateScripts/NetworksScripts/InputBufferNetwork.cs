using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct InputBufferNetwork
{
    public InputItemNetwork[] triggers;
    public int indexTrigger;
    public int indexSequence;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(indexTrigger);
        bw.Write(indexSequence);
        for (int i = 0; i < triggers.Length; ++i)
            triggers[i].Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        indexTrigger = br.ReadInt32();
        indexSequence = br.ReadInt32();
        for (int i = 0; i < triggers.Length; ++i)
            triggers[i].Deserialize(br);
    }

    public void AddTrigger(InputItemNetwork inputItem)
    {
        indexTrigger++;
        if (indexTrigger >= triggers.Length)
            indexTrigger = 0;
        triggers[indexTrigger] = inputItem;
    }
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
        if (CurrentTrigger().pressed && CurrentTrigger().inputEnum == InputEnum.Assist)
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