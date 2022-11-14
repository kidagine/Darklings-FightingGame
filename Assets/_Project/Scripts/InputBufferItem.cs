using System;
using UnityEngine;

public class InputBufferItem
{
    private readonly int _timeBeforeActionsExpire = 30;
    private readonly int _timestamp;

    public Func<bool> Execute;

    public InputBufferItem(int timestamp)
    {
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
