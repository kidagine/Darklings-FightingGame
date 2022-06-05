using System.Collections;
using UnityEngine;

public class KnockbackState : State
{
	[SerializeField] private GameObject _groundedPrefab = default;
	private KnockdownState _knockdownState;
	private Coroutine _canCheckGroundCoroutine;
	private bool _canCheckGround;
	private readonly float _knockbackDirectionY = 0.5f;
	private readonly float _knockbackDuration = 0.2f;
	private readonly float _knockbackForce = 2.5f;

	protected void Awake()
	{
		_knockdownState = GetComponent<KnockdownState>();
	}

	public override void Enter()
	{
		_playerAnimator.HurtAir(true);
		base.Enter();
		_playerMovement.Knockback(new Vector2(
			_player.OtherPlayer.transform.localScale.x, _knockbackDirectionY), _knockbackForce, _knockbackDuration);
		CameraShake.Instance.Shake(1, 1);
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
		if (_playerMovement.IsGrounded && _canCheckGround)
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