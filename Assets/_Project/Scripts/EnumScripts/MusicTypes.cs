using System;
using UnityEngine;

[Serializable]
public enum MusicTypeEnum { Random, Beat28, Lit, Lounge, Soap, Tussle, Yee, Upbeat, Snooze5, NowSmile, Home };

public class MusicTypes : MonoBehaviour
{
    [SerializeField] private MusicTypeEnum _musicTypeEnum = default;

    public MusicTypeEnum MusicTypeEnum { get { return _musicTypeEnum; } private set { } }
}
