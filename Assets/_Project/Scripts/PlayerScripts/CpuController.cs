using UnityEngine;

public class CpuController : BaseController
{
	[SerializeField] private PlayerStateManager _playerStateMachine = default;
	private Transform _otherPlayer;
	private float _movementInputX;
	private float _distance;
	private float _arcanaTimer;
	private float _attackTimer;
	private float _movementTimer;
	private bool _crouch;
	private bool _jump;
	private bool _dash;
	private bool _reset;

	public void SetOtherPlayer(Transform otherPlayer)
	{
		_otherPlayer = otherPlayer;
	}

	void Update()
	{
		if (GameManager.Instance.HasGameStarted)
		{
			if (!TrainingSettings.CpuOff || !SceneSettings.IsTrainingMode)
			{
				_reset = false;
				Movement();
				if (_distance <= 4f)
				{
					Attack();
				}
				Specials();
			}
			else
			{
				if (!_reset)
				{
					_reset = true;
					InputDirection = Vector2.zero;
					_playerStateMachine.ResetToInitialState();
				}
			}
		}
		else
		{
			InputDirection = Vector2.zero;
		}
	}

	private void Movement()
	{
		_distance = Mathf.Abs(_otherPlayer.transform.position.x - transform.position.x);
		_movementTimer -= Time.deltaTime;
		_jump = false;
		_dash = false;
		if (_movementTimer < 0)
		{
			int movementRandom;
			if (_distance <= 6.5f)
			{
				movementRandom = Random.Range(0, 6);
			}
			else
			{
				movementRandom = Random.Range(0, 9);
			}
			int jumpRandom = Random.Range(0, 12);
			int crouchRandom = Random.Range(0, 12);
			int standingRandom = Random.Range(0, 4);
			int dashRandom = Random.Range(0, 8);
			switch (movementRandom)
			{
				case 0:
					_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.None);
					_movementInputX = 0.0f;
					break;
				case > 0 and <= 4:
					_movementInputX = transform.localScale.x * -1.0f;
					if (_movementInputX == 1.0f)
					{
						_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Right);
					}
					else if (_movementInputX == -1.0f)
					{
						_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Left);
					}
					break;
				case > 5:
					_movementInputX = transform.localScale.x * 1.0f;
					if (_movementInputX == 1.0f)
					{
						_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Right);
					}
					else if (_movementInputX == -1.0f)
					{
						_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Left);
					}
					break;
			}
			if (jumpRandom == 2)
			{
				_jump = true;
				_movementInputX = transform.localScale.x * 1.0f;
				_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Up);
			}
			if (crouchRandom == 2)
			{
				_crouch = true;
				_inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Down);
			}
			if (standingRandom == 2)
			{
				_crouch = false;
			}
			if (dashRandom == 2)
			{
				_dash = true;
			}
			InputDirection = new Vector2(_movementInputX, 0.0f);
			_movementTimer = Random.Range(0.2f, 0.35f);
		}
	}

	private void Attack()
	{
		if (IsControllerEnabled)
		{
			_attackTimer -= Time.deltaTime;
			if (_attackTimer < 0)
			{
				int attackRandom = Random.Range(0, 8);
				if (attackRandom <= 2)
				{
					_inputBuffer.AddInputBufferItem(InputEnum.Light);
				}
				else if (attackRandom <= 4)
				{
					_inputBuffer.AddInputBufferItem(InputEnum.Medium);
				}
				else if (attackRandom <= 6)
				{
					_inputBuffer.AddInputBufferItem(InputEnum.Heavy);
				}
				else
				{
					_inputBuffer.AddInputBufferItem(InputEnum.Throw);
				}
				_attackTimer = Random.Range(0.15f, 0.35f);
			}
		}
	}

	private void Specials()
	{
		if (IsControllerEnabled)
		{
			_arcanaTimer -= Time.deltaTime;
			if (_arcanaTimer < 0)
			{
				int arcanaRandom = Random.Range(0, 2);
				if (arcanaRandom == 0)
				{
					_inputBuffer.AddInputBufferItem(InputEnum.Assist);
				}
				else if (arcanaRandom == 1)
				{
					_inputBuffer.AddInputBufferItem(InputEnum.Special);
				}
				_attackTimer = Random.Range(0.15f, 0.35f);
				_arcanaTimer = Random.Range(0.4f, 0.85f);
			}
		}
	}

	public override bool Crouch()
	{
		return _crouch;
	}

	public override bool StandUp()
	{
		return !_crouch;
	}

	public override bool Jump()
	{
		return _jump;
	}

	public override bool Dash(int direction)
	{
		return _dash;
	}

	public override void ActivateInput()
	{
		if (!TrainingSettings.CpuOff)
		{
			base.ActivateInput();
		}
	}

	public override void DeactivateInput()
	{
		if (!TrainingSettings.CpuOff)
		{
			base.DeactivateInput();
		}
	}
}