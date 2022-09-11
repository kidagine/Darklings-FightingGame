using FixMath.NET;
using System;
using UnityEngine;

public class InputBufferItem
{
    private readonly Fix64 _timeBeforeActionsExpire = (Fix64)0.29f;
    private readonly Fix64 _timestamp;

    public Func<bool> Execute;

    public InputBufferItem(Fix64 timestamp)
    {
        _timestamp = timestamp;
    }

    public bool CheckIfValid()
    {
        if (_timestamp + _timeBeforeActionsExpire >= (Fix64)Time.time)
        {
            return true;
        }
        return false;
    }
}
