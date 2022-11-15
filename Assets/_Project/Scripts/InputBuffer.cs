using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    [SerializeField] private PlayerStateManager _playerStateManager = default;
    private readonly Queue<InputBufferItem> _inputBuffer = new();
    private readonly Queue<InputBufferItem> _inputBufferAttacks = new();
    private List<InputBufferItem> _inputBufferItems = new List<InputBufferItem>();
    private InputHistory _inputHistory;
    private InputDirectionEnum _lastInputDirection;
    private InputBufferItem _cachedInputBufferItem;
    private Player _player;
    private int _waitFrame;

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
            if (!_inputBuffer.Peek().CheckIfValid())
            {
                _inputBuffer.Dequeue();
            }
        }
        if (_inputBufferAttacks.Count > 0)
        {
            if (!_inputBufferAttacks.Peek().CheckIfValid())
            {
                _inputBufferAttacks.Dequeue();
            }
        }
        CheckInputBuffer();
        if (_waitFrame > 0)
        {
            if (DemonicsWorld.WaitFramesOnce(ref _waitFrame))
            {
                CheckInputBufferAttacksList();
            }
        }
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
    private void CheckInputBufferAttacksList()
    {
        InputBufferItem inputBufferItem = null;
        for (int i = 0; i < _inputBufferItems.Count; i++)
        {
            if (inputBufferItem != null)
            {
                if (_inputBufferItems[i]._inputEnum == InputEnum.Medium && inputBufferItem._inputEnum == InputEnum.Light)
                {
                    if (_player.CheckRecoverableHealth())
                    {
                        inputBufferItem = new(InputEnum.RedFrenzy, DemonicsWorld.Frame);
                        inputBufferItem.Execute += () => ExecuteInputBuffer(InputEnum.RedFrenzy);
                        break;
                    }
                }
                if (_inputBufferItems[i]._inputEnum == InputEnum.Heavy && inputBufferItem._inputEnum == InputEnum.Medium)
                {
                    Debug.Log("A");
                    inputBufferItem = new(InputEnum.Parry, DemonicsWorld.Frame);
                    inputBufferItem.Execute += () => ExecuteInputBuffer(InputEnum.Parry);
                    break;
                }
            }

            if (inputBufferItem == null)
            {
                inputBufferItem = _inputBufferItems[i];
            }
            else if (_inputBufferItems[i]._priority < inputBufferItem._priority)
            {
                inputBufferItem = _inputBufferItems[i];
            }
        }
        if (inputBufferItem != null)
        {
            if (inputBufferItem.Execute.Invoke())
            {
                _inputBufferItems.Clear();
            }
        }
    }

    public void CheckInputBufferAttacks()
    {
        if (_inputBufferAttacks.Count > 0)
        {
            if (_inputBufferItems.Count == 0 || _inputBufferItems[0]._timestamp == _inputBufferAttacks.Peek()._timestamp)
            {
                _inputBufferItems.Add(_inputBufferAttacks.Peek());
                _inputBufferAttacks.Dequeue();
                if (_inputBufferItems.Count == 1)
                {
                    _waitFrame = 2;
                }
            }
            else
            {
                _inputBufferItems.Clear();
                _inputBufferItems.Add(_inputBufferAttacks.Peek());
                _inputBufferAttacks.Dequeue();
                if (_inputBufferItems.Count == 1)
                {
                    _waitFrame = 2;
                }
            }
        }
        else
        {
            if (_inputBufferItems.Count > 0)
            {
                CheckInputBufferAttacksList();
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
