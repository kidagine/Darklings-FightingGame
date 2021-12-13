using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
	private readonly Queue<InputBufferItem> _inputBuffer = new Queue<InputBufferItem>();
	private InputHistory _inputHistory;
	private Player _player;


	void Start()
	{
		_player = GetComponent<Player>();
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
		if (inputEnum == InputEnum.Light)
		{
			_inputBuffer.Enqueue(new InputBufferItem(Time.time));
			_inputBuffer.Peek().Execute += _player.AttackAction;
			CheckForInputBufferItem();
		}
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
