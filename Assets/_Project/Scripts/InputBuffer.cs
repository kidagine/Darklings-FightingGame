using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    [SerializeField] private PlayerStateManager _playerStateManager = default;
    private readonly Queue<InputBufferItem> _inputBuffer = new Queue<InputBufferItem>();
    private InputHistory _inputHistory;
    private Player _player;
    private PlayerMovement _playerMovement;
    private bool _isExecuting;


    void Start()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

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
        InputBufferItem inputBufferItem = new(Time.time);
        _inputBuffer.Enqueue(inputBufferItem);

        if (inputEnum == InputEnum.Direction)
        {
            if (inputDirectionEnum == InputDirectionEnum.Up)
            {
                inputBufferItem.Execute += () => { return true; };
            }
            else if (inputDirectionEnum == InputDirectionEnum.Down)
            {
                inputBufferItem.Execute += () => { return true; };
            }
            else if (inputDirectionEnum == InputDirectionEnum.Left)
            {
                inputBufferItem.Execute += () => { _playerMovement.MovementInput = Vector2.left; return true; };
            }
            else if (inputDirectionEnum == InputDirectionEnum.Right)
            {
                inputBufferItem.Execute += () => { _playerMovement.MovementInput = Vector2.right; return true; };
            }
        }
        else if (inputEnum == InputEnum.Light)
        {
            inputBufferItem.Execute += () => _playerStateManager.TryToAttackState(inputEnum);
        }
        else if (inputEnum == InputEnum.Medium)
        {
            inputBufferItem.Execute += () => _playerStateManager.TryToAttackState(inputEnum);
        }
        else if (inputEnum == InputEnum.Heavy)
        {
            inputBufferItem.Execute += () => _playerStateManager.TryToAttackState(inputEnum);
        }
        else if (inputEnum == InputEnum.Special)
        {
            inputBufferItem.Execute += () => _playerStateManager.TryToAttackState(inputEnum);
        }
        else if (inputEnum == InputEnum.Assist)
        {
            inputBufferItem.Execute += _player.AssistAction;
        }
        else if (inputEnum == InputEnum.Throw)
        {
            inputBufferItem.Execute += () => { return true; };
        }
        CheckForInputBufferItem();
    }

    public void CheckForInputBufferItem()
    {
        if (_inputBuffer.Count > 0 && !_isExecuting)
        {
            _isExecuting = true;
            if (_inputBuffer.Peek().Execute.Invoke())
            {
                _inputBuffer.Dequeue();
            }
            _isExecuting = false;
        }
    }

    public void ClearInputBuffer()
    {
        _inputBuffer.Clear();
    }
}
