using System;
using UnityEngine;

public class InputBufferItem
{
    private readonly float _timeBeforeActionsExpire = 0.29f;
    private readonly float _timestamp;

    public Func<bool> Execute;

    public InputBufferItem(float timestamp)
    {
        _timestamp = timestamp;
    }

    public bool CheckIfValid()
    {
        if (_timestamp + _timeBeforeActionsExpire >= Time.time)
        {
            return true;
        }
        return false;
    }
}
