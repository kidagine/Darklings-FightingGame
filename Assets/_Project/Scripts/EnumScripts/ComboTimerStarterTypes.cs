using System;
using UnityEngine;

[Serializable]
public enum ComboTimerStarterEnum { Blue, Yellow, Red };

public class ComboTimerStarterTypes : MonoBehaviour
{
    [SerializeField] private ComboTimerStarterEnum _comboTimerStarterEnum = default;

    public ComboTimerStarterEnum ComboTimerStarterEnum { get { return _comboTimerStarterEnum; } private set { } }
}
