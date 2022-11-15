using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    [SerializeField] private PlayerStateManager _playerStateManager = default;
    private readonly Queue<InputBufferItem> _inputBuffer = new();
    private readonly Queue<InputBufferItem> _inputBufferAttacks = new();
    private InputHistory _inputHistory;
    private InputDirectionEnum _lastInputDirection;
    private InputBufferItem _cachedInputBufferItem;
    private Player _player;


    void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void Initialize(InputHistory inputHistory)
    {
        _inputHistory = inputHistory;
    }

    public void AddInputBufferItem(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum = InputDirectionEnum.None)
    {
        if (inputEnum != InputEnum.Direction)
        {
            _inputHistory.AddInput(inputEnum, inputDirectionEnum);
            InputBufferItem inputBufferItem = new(inputEnum, DemonicsWorld.Frame);
            _inputBufferAttacks.Enqueue(inputBufferItem);
            inputBufferItem.Execute += () => ExecuteInputBuffer(inputEnum);
            CheckInputBufferAttacks();
        }
        else
        {
            _inputHistory.AddInput(inputEnum, inputDirectionEnum);
            InputBufferItem inputBufferItem = new(inputEnum, DemonicsWorld.Frame);
            _inputBuffer.Enqueue(inputBufferItem);
            inputBufferItem.Execute += () => { _lastInputDirection = inputDirectionEnum; return true; };
        }
    }

    void FixedUpdate()
    {
        if (_inputBuffer.Count > 0)
        {
            InputBufferItem inputBufferItem = _inputBuffer.Peek();
            if (!inputBufferItem.CheckIfValid())
            {
                _inputBuffer.Dequeue();
            }
        }
        if (_inputBufferAttacks.Count > 0)
        {
            InputBufferItem inputBufferItem = _inputBufferAttacks.Peek();
            if (!inputBufferItem.CheckIfValid())
            {
                _inputBufferAttacks.Dequeue();
            }
        }
        CheckInputBuffer();
        // if (_inputBufferItem != null)
        // {
        //     if (_inputBufferItem.Execute.Invoke())
        //     {
        //         if (_inputBufferAttacks.Count > 0)
        //         {
        //             _inputBufferAttacks.Dequeue();
        //             _inputBufferItem = _inputBufferAttacks.Peek();
        //         }
        //     }
        // }
    }

    private void CheckInputBuffer()
    {
        if (_inputBuffer.Count > 0)
        {
            if (_inputBuffer.Peek().Execute.Invoke())
            {
                _inputBuffer.Dequeue();
            }
        }
    }

    public void CheckInputBufferAttacks()
    {
        if (_inputBufferAttacks.Count > 0)
        {
            if (_cachedInputBufferItem != null)
            {
                Debug.Log(_inputBufferAttacks.Peek()._inputEnum + "|" + _cachedInputBufferItem._inputEnum);
                Debug.Log(_inputBufferAttacks.Peek()._timestamp + "|" + _cachedInputBufferItem._timestamp);
                if (_inputBufferAttacks.Peek()._timestamp == _cachedInputBufferItem._timestamp)
                {
                    if (_inputBufferAttacks.Peek()._inputEnum == InputEnum.Medium && _cachedInputBufferItem._inputEnum == InputEnum.Light)
                    {
                        if (_player.CheckRecoverableHealth())
                        {
                            if (_inputBufferAttacks.Peek().Execute.Invoke())
                            {
                                _cachedInputBufferItem = _inputBufferAttacks.Peek();
                                _inputBufferAttacks.Dequeue();
                            }
                            return;
                        }
                    }
                    else
                    {
                        if (_inputBufferAttacks.Peek().Execute.Invoke())
                        {
                            _cachedInputBufferItem = _inputBufferAttacks.Peek();
                            _inputBufferAttacks.Dequeue();
                        }
                        return;
                    }
                }
                else
                {
                    if (_inputBufferAttacks.Peek().Execute.Invoke())
                    {
                        _cachedInputBufferItem = _inputBufferAttacks.Peek();
                        _inputBufferAttacks.Dequeue();
                    }
                }
            }
            else
            {
                _cachedInputBufferItem = _inputBufferAttacks.Peek();
                CheckInputBufferAttacks();
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
