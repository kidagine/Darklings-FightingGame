using System.Collections;
using UnityEngine;

public class WallSplatState : State
{
	[SerializeField] private GameObject _wallSplatPrefab = default;
	private AirborneHurtState _airborneHurtState;
	private Coroutine _wallSplatCoroutine;

	void Awake()
	{
		_airborneHurtState = GetComponent<AirborneHurtState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Knockdown();
		_player.SetHurtbox(false);
		_playerMovement.ZeroGravity();
		GameObject effect = Instantiate(_wallSplatPrefab);
		effect.transform.localPosition = transform.position;
		StartCoroutine(WallSplatStateCoroutine());
	}

	IEnumerator WallSplatStateCoroutine()
	{
		yield return new WaitForSeconds(0.2f);
		ToAirborneHurtState();
	}

	private void ToAirborneHurtState()
	{
		_airborneHurtState.WallSplat = true;
		_stateMachine.ChangeState(_airborneHurtState);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, 0.0f);
	}

	public override void Exit()
	{
		base.Exit();
		if (_wallSplatCoroutine != null)
		{
			StopCoroutine(_wallSplatCoroutine);
		}
	}
}