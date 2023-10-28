using System;
using UnityEngine;

[Flags]
[Serializable]
public enum InputEnum
{
    Neutral = 0, Up = 1, Down = 2, Left = 3, Right = 4,
    UpLeft = 5, UpRight = 6, DownLeft = 7, DownRight = 8,
    Light = 9, Medium = 10, Heavy = 11, Special = 12,
    Assist = 13, Throw = 14, Parry = 15,
    ForwardDash = 16, BackDash = 17, RedFrenzy = 18,
};


public class InputTypes : MonoBehaviour
{
    [SerializeField] private InputEnum _inputEnum = default;

    public InputEnum InputEnum { get { return _inputEnum; } private set { } }

}
