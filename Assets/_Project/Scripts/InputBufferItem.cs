using System;
using UnityEngine;

public class InputBufferItem
{
    private readonly DemonicsFloat _timeBeforeActionsExpire = (DemonicsFloat)0.29f;
    private readonly DemonicsFloat _timestamp;

    public Func<bool> Execute;

    public InputBufferItem(DemonicsFloat timestamp)
    {
        _timestamp = timestamp;
    }

    public bool CheckIfValid()
    {
        if (_timestamp + _timeBeforeActionsExpire >= (DemonicsFloat)Time.time)
        {
            return true;
        }
        return false;
    }
}
