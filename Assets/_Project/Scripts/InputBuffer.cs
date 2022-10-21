using FixMath.NET;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    [SerializeField] private PlayerStateManager _playerStateManager = default;
    private readonly Queue<InputBufferItem> _inputBuffer = new();
    private InputHistory _inputHistory;
    private InputDirectionEnum _lastInputDirection;

    void Update()
    {
        if (_inputBuffer.Count > 0)
        {
            InputBufferItem inputBufferItem = _inputBuffer.Peek();
            if (!inputBufferItem.CheckIfValid())
            {
                _inputBuffer.Dequeue();
            }
        }
    }

    public void Initialize(InputHistory inputHistory)
    {
        _inputHistory = inputHistory;
    }

    public void AddInputBufferItem(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum = InputDirectionEnum.None)
    {
        _inputHistory.AddInput(inputEnum, inputDirectionEnum);
        InputBufferItem inputBufferItem = new((Fix64)Time.time);
        _inputBuffer.Enqueue(inputBufferItem);

        if (inputEnum != InputEnum.Direction)
        {
            inputBufferItem.Execute += () => ExecuteInputBuffer(inputEnum);
        }
        else
        {
            inputBufferItem.Execute += () => { _lastInputDirection = inputDirectionEnum; return true; };
        }
    }

    private void FixedUpdate()
    {
        CheckInputBuffer();
    }

    public void CheckInputBuffer()
    {
        if (_inputBuffer.Count > 0)
        {
            if (_inputBuffer.Peek().Execute.Invoke())
            {
                _inputBuffer.Dequeue();
            }
        }
    }

    public bool ExecuteInputBuffer(InputEnum inputEnum)
    {
        if (inputEnum == InputEnum.Parry)
        {
            return _playerStateManager.TryToParryState();
        }
        if (inputEnum == InputEnum.RedFrenzy)
        {
            return _playerStateManager.TryToRedFrenzyState();
        }
        if (inputEnum == InputEnum.Throw)
        {
            return _playerStateManager.TryToGrabState();
        }
        if (inputEnum == InputEnum.ForwardDash)
        {
            return _playerStateManager.TryToDashState(1);
        }
        if (inputEnum == InputEnum.BackDash)
        {
            return _playerStateManager.TryToDashState(-1);
        }
        if (inputEnum == InputEnum.Assist)
        {
            return _playerStateManager.TryToAssistCall();
        }
        else if (inputEnum == InputEnum.Special)
        {
            bool value = _playerStateManager.TryToArcanaState(_lastInputDirection);
            if (value)
            {
                _lastInputDirection = InputDirectionEnum.None;
            }
            return value;
        }
        else
        {
            bool value = _playerStateManager.TryToAttackState(inputEnum, _lastInputDirection);
            if (value)
            {
                _lastInputDirection = InputDirectionEnum.None;
            }
            return value;
        }
    }

    public void ClearInputBuffer()
    {
        _inputBuffer.Clear();
    }
}
