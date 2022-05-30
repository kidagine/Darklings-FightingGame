using System.Collections;
using UnityEngine;

public class AirborneHurtState : HurtParentState
{
	[SerializeField] private GameObject _groundedPrefab = default;
	private KnockdownState _knockdownState;
	private Coroutine _canCheckGroundCoroutine;
	private bool _canCheckGround;

	protected override void Awake()
	{
		base.Awake();
		_knockdownState = GetComponent<KnockdownState>();
	}

	public override void Enter()
	{
		_playerAnimator.HurtAir(true);
		base.Enter();
		_playerMovement.Knockback(new Vector2(
			_player.OtherPlayer.transform.localScale.x, _hurtAttack.knockbackDirection.y), _hurtAttack.knockback, _hurtAttack.knockbackDuration);
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
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
	}

	private new void ToKnockdownState()
	{
		if (_playerMovement._isGrounded && _canCheckGround)
		{
			Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
			_audio.Sound("Landed").Play();
			_stateMachine.ChangeState(_knockdownState);
		}
	}

	public override void Exit()
	{
		base.Exit();
		if (_canCheckGroundCoroutine != null)
		{
			_player.OtherPlayerUI.ResetCombo();
			_canCheckGround = false;
			StopCoroutine(_canCheckGroundCoroutine);
		}
	}

}