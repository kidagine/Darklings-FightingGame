using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct CameraShakerNetwork
{
    public float intensity;
    public float timer;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(intensity);
        bw.Write(timer);
    }

    public void Deserialize(BinaryReader br)
    {
        intensity = br.ReadSingle();
        timer = br.ReadSingle();
    }
};
