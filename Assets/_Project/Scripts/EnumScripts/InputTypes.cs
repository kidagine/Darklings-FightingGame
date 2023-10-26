using System;
using UnityEngine;

[Flags]
[Serializable]
public enum InputEnum
{
    Direction = 1, Light = 2, Medium = 4, Heavy = 8, Special = 12, Assist = 16, Throw = 32, Parry = 64,
    ForwardDash = 128, BackDash = 264, RedFrenzy = 528, Up = 1056, Down = 2112, Left = 4224, Right = 8448,
    UpRight, UpLeft, DownRight, DownLeft, Neutral
};


public class InputTypes : MonoBehaviour
{
    [SerializeField] private InputEnum _inputEnum = default;

    public InputEnum InputEnum { get { return _inputEnum; } private set { } }

}
