using UnityEngine;

public class AirborneHurtState : State
{
	[SerializeField] private GameObject _groundedPrefab = default;
	private KnockdownState _knockdownState;
	private AttackSO _hurtAttack;

	void Awake()
	{
		_knockdownState = GetComponent<KnockdownState>();
	}

	public void Initialize(AttackSO hurtAttack)
	{
		_hurtAttack = hurtAttack;
	}

	public override void Enter()
	{
		base.Enter();
		_audio.Sound(_hurtAttack.impactSound).Play();
		_playerAnimator.HurtAir(true);
		GameObject effect = Instantiate(_hurtAttack.hurtEffect);
		effect.transform.localPosition = _hurtAttack.hurtEffectPosition;
		_playerMovement.Knockback(new Vector2(
			transform.root.localScale.x * -1.0f, _hurtAttack.knockbackDirection.y), _hurtAttack.knockback, _hurtAttack.knockbackDuration);
		if (_hurtAttack.cameraShaker != null)
		{
			CameraShake.Instance.Shake(_hurtAttack.cameraShaker.intensity, _hurtAttack.cameraShaker.timer);
		}
		GameManager.Instance.HitStop(_hurtAttack.hitstop);
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