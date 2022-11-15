using System;
using UnityEngine;

public class InputBufferItem
{
    private readonly int _timeBeforeActionsExpire = 20;
    public int _timestamp;
    public InputEnum _inputEnum;

    public Func<bool> Execute;

    public InputBufferItem(InputEnum inputEnum, int timestamp)
    {
        _inputEnum = inputEnum;
        _timestamp = timestamp;
    }

    public bool CheckIfValid()
    {
        if (_timestamp + _timeBeforeActionsExpire >= DemonicsWorld.Frame)
        {
            return true;
        }
        return false;
    }
}
