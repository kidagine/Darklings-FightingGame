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
		InputBufferItem inputBufferItem = new InputBufferItem(Time.time);
		_inputBuffer.Enqueue(inputBufferItem);


		if (inputEnum == InputEnum.Direction)
		{
			if (inputDirectionEnum == InputDirectionEnum.Up)
			{
				inputBufferItem.Execute += _playerMovement.StandUpAction;
			}
			else if (inputDirectionEnum == InputDirectionEnum.Down)
			{
				inputBufferItem.Execute += _playerMovement.CrouchAction;
			}
			else if (inputDirectionEnum == InputDirectionEnum.Left)
			{
				inputBufferItem.Execute += _playerMovement.StandUpAction;
			}
			else if (inputDirectionEnum == InputDirectionEnum.Right)
			{
				inputBufferItem.Execute += _playerMovement.StandUpAction;
			}
		}

		if (inputEnum == InputEnum.Light)
		{
			inputBufferItem.Execute += _player.AttackAction;
		}
		else if(inputEnum == InputEnum.Special)
		{
			inputBufferItem.Execute += _player.ArcaneAction;
		}
		else if(inputEnum == InputEnum.Assist)
		{
			inputBufferItem.Execute += _player.AssistAction;
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
