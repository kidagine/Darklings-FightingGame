using System;
using System.IO;

[Serializable]
public struct EffectNetwork
{
    public string name;
    public int animationMaxFrames;
    public EffectGroupNetwork[] effectGroups;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(name);
        bw.Write(animationMaxFrames);
        for (int i = 0; i < effectGroups.Length; ++i)
        {
            effectGroups[i].Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        name = br.ReadString();
        animationMaxFrames = br.ReadInt32();
        for (int i = 0; i < effectGroups.Length; ++i)
        {
            effectGroups[i].Deserialize(br);
        }
    }
};