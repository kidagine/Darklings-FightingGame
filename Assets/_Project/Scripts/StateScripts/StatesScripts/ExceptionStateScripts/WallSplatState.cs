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
		_playerAnimator.WallSplat();
		_player.SetHurtbox(false);
		_playerMovement.ZeroGravity();
		_playerMovement.StopAllCoroutines();
		_player.transform.position = _playerMovement.OnWall();
		_playerUI.DisplayNotification(NotificationTypeEnum.WallSplat);
		GameObject effect = Instantiate(_wallSplatPrefab);
		effect.transform.localPosition = transform.position;
		_wallSplatCoroutine = StartCoroutine(WallSplatStateCoroutine());
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

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		_rigidbody.velocity = Vector2.zero;
	}

	public override bool ToKnockdownState()
	{
		return false;
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