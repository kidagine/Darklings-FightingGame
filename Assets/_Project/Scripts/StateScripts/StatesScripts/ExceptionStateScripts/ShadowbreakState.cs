using UnityEngine;

public class ShadowbreakState : State
{
	[SerializeField] private GameObject _shadowbreakPrefab = default;
	private FallState _fallState;

	private void Awake()
	{
		_fallState = GetComponent<FallState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.OnCurrentAnimationFinished.AddListener(ToFallState);
		_playerAnimator.Shadowbreak();
		_audio.Sound("Shadowbreak").Play();
		CameraShake.Instance.Shake(0.5f, 0.1f);
		Transform shadowbreak = Instantiate(_shadowbreakPrefab, _playerAnimator.transform).transform;
		shadowbreak.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
	}

	private void ToFallState()
	{
		_stateMachine.ChangeState(_fallState);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
	}
}