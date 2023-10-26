using System;
using UnityEngine;

[Flags]
[Serializable]
public enum InputDirectionEnum { Neutral, Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight };


public class InputDirectionTypes : MonoBehaviour
{
    [SerializeField] private InputDirectionEnum _inputDirectionEnum = default;

    public static Vector2Int GetDirectionValue(InputEnum inputEnum)
    {
        return inputEnum switch
        {
            InputEnum.Up => new Vector2Int(0, 1),
            InputEnum.Down => new Vector2Int(0, -1),
            InputEnum.Left => new Vector2Int(-1, 0),
            InputEnum.Right => new Vector2Int(1, 0),
            InputEnum.UpRight => new Vector2Int(1, 1),
            InputEnum.UpLeft => new Vector2Int(-1, 1),
            InputEnum.DownRight => new Vector2Int(1, -1),
            InputEnum.DownLeft => new Vector2Int(-1, -1),
            InputEnum.Neutral => Vector2Int.zero,
            _ => Vector2Int.zero
        };
    }

    public InputDirectionEnum InputDirectionEnum { get { return _inputDirectionEnum; } private set { } }
}