using UnityEngine;

public class ParryState : State
{
	[SerializeField] private GameObject _parryEffect = default;
	private IdleState _idleState;
	private HurtState _hurtState;
	private AirborneHurtState _airborneHurtState;
	private GrabbedState _grabbedState;
	private bool _parried;

	private void Awake()
	{
		_idleState = GetComponent<IdleState>();
		_hurtState = GetComponent<HurtState>();
		_airborneHurtState = GetComponent<AirborneHurtState>();
		_grabbedState = GetComponent<GrabbedState>();
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound("ParryStart").Play();
		_playerAnimator.Parry();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
	}


	private void ToIdleState()
	{
		_stateMachine.ChangeState(_idleState);
	}

	public override bool ToHurtState(AttackSO attack)
	{
		if (_player.Parrying)
		{
			Parry(attack);
			return false;
		}
		else
		{
			_hurtState.Initialize(attack);
			_stateMachine.ChangeState(_hurtState);
			return true;
		}
	}

	public override bool ToAirborneHurtState(AttackSO attack)
	{
		if (_player.Parrying)
		{
			Parry(attack);
			return false;
		}
		else
		{
			_airborneHurtState.Initialize(attack);
			_stateMachine.ChangeState(_airborneHurtState);
			return true;
		}
	}

	private void Parry(AttackSO attack)
	{
		_audio.Sound("Parry").Play();
		_player.ArcanaGain(0.3f);
		_parried = true;
		GameManager.Instance.HitStop(0.2f);
		GameObject effect = Instantiate(_parryEffect);
		effect.transform.localPosition = attack.hurtEffectPosition;
		if (!_playerMovement.IsInCorner)
		{
			_playerMovement.Knockback(new Vector2(
				_player.OtherPlayer.transform.localScale.x, 0.0f), attack.knockback / 2.0f, attack.knockbackDuration);
		}
	}

	public override bool ToParryState()
	{
		if (_parried)
		{
			_stateMachine.ChangeState(this);
			return true;
		}
		return false;
	}

	public override bool ToGrabbedState()
	{
		_stateMachine.ChangeState(_grabbedState);
		return true;
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}

	public override void Exit()
	{
		base.Exit();
		_parried = false;
		_playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
	}
}