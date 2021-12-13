using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
	private readonly Queue<InputBufferItem> _inputBuffer = new Queue<InputBufferItem>();
	private InputHistory _inputHistory;
	private Player _player;
	private PlayerMovement _playerMovement;


	void Start()
	{
		_player = GetComponent<Player>();
		_playerMovement = GetComponent<PlayerMovement>();
	}

	void LateUpdate()
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

	public void AddInputBufferItem(InputEnum inputEnum)
	{
		_inputHistory.AddInput(inputEnum);
		_inputBuffer.Enqueue(new InputBufferItem(Time.time));
		if (inputEnum == InputEnum.Down)
		{
			_inputBuffer.Peek().Execute += _playerMovement.CrouchAction;
		}
		if (inputEnum == InputEnum.Up)
		{
			_inputBuffer.Peek().Execute += _playerMovement.CrouchAction;
		}
		if (inputEnum == InputEnum.Down)
		{
			_inputBuffer.Peek().Execute += _playerMovement.CrouchAction;
		}
		if (inputEnum == InputEnum.Left)
		{
			_inputBuffer.Peek().Execute += _playerMovement.CrouchAction;
		}
		if (inputEnum == InputEnum.Right)
		{
			_inputBuffer.Peek().Execute += _playerMovement.CrouchAction;
		}
		if (inputEnum == InputEnum.Light)
		{
			_inputBuffer.Peek().Execute += _player.AttackAction;
		}
		if (inputEnum == InputEnum.Special)
		{
			_inputBuffer.Peek().Execute += _player.ArcaneAction;
		}
		if (inputEnum == InputEnum.Assist)
		{
			_inputBuffer.Peek().Execute += _player.AssistAction;
		}
		CheckForInputBufferItem();
	}

	public void CheckForInputBufferItem()
	{
		if (_inputBuffer.Count > 0)
		{
			if (_inputBuffer.Peek().Execute.Invoke())
			{
				_inputBuffer.Dequeue();
			}
		}
	}
}
