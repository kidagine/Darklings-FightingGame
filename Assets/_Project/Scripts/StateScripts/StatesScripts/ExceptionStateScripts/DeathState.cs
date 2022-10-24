using UnityEngine;

public class DeathState : State
{
    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Knockdown();
        _player.SetHurtbox(false);
        _player.SetPushboxTrigger(true);
        _playerUI.SetRecoverableHealth(0);
        _playerUI.SetHealthDamaged(0);
        GameManager.Instance.RoundOver(false);
        GameManager.Instance.SuperFreeze();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_playerMovement.IsGrounded)
        {
            _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
        }
    }

    public override bool ToHurtState(AttackSO attack)
    {
        return false;
    }
}