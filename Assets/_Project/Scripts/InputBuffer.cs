using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
	[SerializeField] private PlayerStateManager _playerStateManager = default;
	private readonly Queue<InputBufferItem> _inputBuffer = new Queue<InputBufferItem>();
	private InputHistory _inputHistory;
	private bool _isExecuting;
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
		InputBufferItem inputBufferItem = new(Time.time);
		_inputBuffer.Enqueue(inputBufferItem);

		if (inputEnum != InputEnum.Direction)
		{
			inputBufferItem.Execute += () => ExecuteInputBuffer(inputEnum);
		}
		else
		{
			inputBufferItem.Execute += () => { _lastInputDirection = inputDirectionEnum; return true; };
		}
		CheckInputBuffer();
	}

	public void CheckInputBuffer()
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

	public bool ExecuteInputBuffer(InputEnum inputEnum)
	{
		if (inputEnum == InputEnum.Assist)
		{
			return _playerStateManager.TryToAssistCall();
		}
		else if (inputEnum == InputEnum.Special)
		{
			return _playerStateManager.TryToArcanaState();
		}
		else
		{
			_playerStateManager.TryToAttackState(inputEnum, _lastInputDirection);
			_lastInputDirection = InputDirectionEnum.None;
			return true;
		}
	}

	public void ClearInputBuffer()
	{
		_inputBuffer.Clear();
	}
}
