using System.Collections;
using UnityEngine;

public class AirborneHurtState : HurtParentState
{
	private WallSplatState _wallSplatState;
	private KnockdownState _knockdownState;
	private GrabbedState _grabbedState;
	private Coroutine _canCheckGroundCoroutine;
	private bool _canCheckGround;

	public bool WallSplat { get; set; }


	protected override void Awake()
	{
		base.Awake();
		_wallSplatState = GetComponent<WallSplatState>();
		_knockdownState = GetComponent<KnockdownState>();
		_grabbedState = GetComponent<GrabbedState>();
	}

	public override void Enter()
	{
		_playerAnimator.HurtAir(true);
		_rigidbody.velocity = Vector2.zero;
		_player.OtherPlayer.FreezeComboTimer();
		if (WallSplat)
		{
			_player.Flip((int) -_player.transform.localScale.x);
			_rigidbody.AddForce(new Vector2(-_player.transform.localScale.x * 5, 12), ForceMode2D.Impulse);
		}
		else
		{
			_player.Flip((int)-_player.OtherPlayer.transform.localScale.x);
			GameObject effect = Instantiate(_hurtAttack.hurtEffect);
			effect.transform.localPosition = _hurtAttack.hurtEffectPosition;
			_playerMovement.Knockback(new Vector2(
			_player.OtherPlayer.transform.localScale.x, _hurtAttack.knockbackDirection.y), new Vector2(_hurtAttack.knockback, _hurtAttack.knockback), _hurtAttack.knockbackDuration);
		}
		base.Enter();
		_player.SetAirPushBox(true);
		CameraShake.Instance.Shake(_hurtAttack.cameraShaker.intensity, _hurtAttack.cameraShaker.timer);
		_canCheckGroundCoroutine = StartCoroutine(CanCheckGroundCoroutine());
	}

	IEnumerator CanCheckGroundCoroutine()
	{
		yield return new WaitForSeconds(0.1f);
		_canCheckGround = true;
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		ToKnockdownState();
		ToWallSplatState();
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
	}

	private new void ToKnockdownState()
	{
		if (_playerMovement.IsGrounded && _canCheckGround)
		{
			_audio.Sound("Landed").Play();
			_stateMachine.ChangeState(_knockdownState);
		}
	}

	private void ToWallSplatState()
	{
		if (_playerMovement.OnWall() != Vector2.zero && !WallSplat)
		{
			_audio.Sound("Landed").Play();
			_stateMachine.ChangeState(_wallSplatState);
		}
	}

	public override bool ToAirborneHurtState(AttackSO attack)
	{
		this.Initialize(attack);
		_stateMachine.ChangeState(this);
		return true;
	}


	public override bool ToGrabbedState()
	{
		_stateMachine.ChangeState(_grabbedState);
		return true;
	}

	public override void Exit()
	{
		base.Exit();
		if (_canCheckGroundCoroutine != null)
		{
			StopCoroutine(_canCheckGroundCoroutine);
		}
		WallSplat = false;
		_canCheckGround = false;
		_player.SetAirPushBox(false);
	}
}