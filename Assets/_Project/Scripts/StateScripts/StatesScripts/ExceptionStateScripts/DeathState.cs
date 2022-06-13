using UnityEngine;

public class DeathState : State
{
	public override void Enter()
	{
		base.Enter();
		_playerAnimator.Knockdown();
		_player.SetHurtbox(false);
		_player.SetPushboxTrigger(true);
		if (!GameManager.Instance.IsTrainingMode)
		{
			_player.Lives--;
		}
		if (_player.Lives <= 0)
		{
			GameManager.Instance.MatchOver();
		}
		else
		{
			GameManager.Instance.RoundOver();
		}
		GameManager.Instance.HitStop(0.35f);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
	}

	public override bool ToHurtState(AttackSO attack)
	{
		return false;
	}
}