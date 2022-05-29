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

	public void SetOtherPlayer(Transform otherPlayer)
	{
		_otherPlayer = otherPlayer;
	}

	void Update()
	{
		if (!GameManager.Instance.IsCpuOff)
		{
			Movement();
			if (_distance <= 5.5f)
			{
				Attack();
			}
			Specials();
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
			_movementInputX = movementRandom switch
			{
				1 => 0.0f,
				2 => transform.localScale.x * -1.0f,
				_ => transform.localScale.x * 1.0f,
			};
			if (jumpRandom == 2)
			{
				_jump = true;
				_movementInputX = transform.localScale.x * 1.0f;
			}
			if (crouchRandom == 2)
			{
				_crouch = true;
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
					_playerStateMachine.TryToAttackState(InputEnum.Light);
				}
				else if (attackRandom <= 4)
				{
					_playerStateMachine.TryToAttackState(InputEnum.Medium);
				}
				else if (attackRandom <= 6)
				{
					_playerStateMachine.TryToAttackState(InputEnum.Heavy);
				}
				else
				{
				   // _player.ThrowAction();
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
					_playerStateMachine.TryToAssistCall();
				}
				else if (arcanaRandom == 1)
				{
					_playerStateMachine.TryToArcanaState();
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

	public override bool DashForward()
	{
		return _dash;
	}

	public override void ActivateInput()
	{
		if (!GameManager.Instance.IsCpuOff)
		{
			base.ActivateInput();
		}
	}

	public override void DeactivateInput()
	{
		if (!GameManager.Instance.IsCpuOff)
		{
			base.DeactivateInput();
		}
	}
}