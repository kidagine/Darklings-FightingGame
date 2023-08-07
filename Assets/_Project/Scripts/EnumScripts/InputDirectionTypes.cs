using System;
using UnityEngine;

[Flags]
[Serializable]
public enum InputDirectionEnum { Neutral, Up, Down, Left, Right, UpLeft, UpRight, DownLeft, DownRight };


public class InputDirectionTypes : MonoBehaviour
{
    [SerializeField] private InputDirectionEnum _inputDirectionEnum = default;

    public static Vector2Int GetDirectionValue(InputDirectionEnum inputDirection)
    {
        if (inputDirection == InputDirectionEnum.Up)
            return new Vector2Int(0, 1);
        if (inputDirection == InputDirectionEnum.Down)
            return new Vector2Int(0, -1);
        if (inputDirection == InputDirectionEnum.Left)
            return new Vector2Int(-1, 0);
        if (inputDirection == InputDirectionEnum.Right)
            return new Vector2Int(1, 0);
        if (inputDirection == InputDirectionEnum.UpLeft)
            return new Vector2Int(-1, 1);
        if (inputDirection == InputDirectionEnum.UpRight)
            return new Vector2Int(1, 1);
        if (inputDirection == InputDirectionEnum.DownLeft)
            return new Vector2Int(-1, -1);
        if (inputDirection == InputDirectionEnum.DownRight)
            return new Vector2Int(1, -1);
        return new Vector2Int(0, 0);
    }

    public InputDirectionEnum InputDirectionEnum { get { return _inputDirectionEnum; } private set { } }
}