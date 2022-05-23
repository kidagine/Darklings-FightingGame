using UnityEngine;

public class AirborneHurtState : HurtParentState
{
	[SerializeField] private GameObject _groundedPrefab = default;
	private KnockdownState _knockdownState;

	void Awake()
	{
		_knockdownState = GetComponent<KnockdownState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.HurtAir(true);
		_playerMovement.Knockback(new Vector2(
			transform.root.localScale.x * -1.0f, _hurtAttack.knockbackDirection.y), _hurtAttack.knockback, _hurtAttack.knockbackDuration);
		CameraShake.Instance.Shake(_hurtAttack.cameraShaker.intensity, _hurtAttack.cameraShaker.timer);
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		ToKnockdownState();
	}

	public void ToKnockdownState()
	{
		if (_playerMovement._isGrounded && _rigidbody.velocity.y <= 0.0f)
		{
			Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
			_audio.Sound("Landed").Play();
			_stateMachine.ChangeState(_knockdownState);
		}
	}
}