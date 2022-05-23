using System.Collections;
using UnityEngine;

public class HurtState : State
{
    private IdleState _idleState;
    private AttackSO _hurtAttack;

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    public void Initialize(AttackSO hurtAttack)
    {
        _hurtAttack = hurtAttack;
    }

    public override void Enter()
    {
        base.Enter();
        _audio.Sound(_hurtAttack.impactSound).Play();
        _playerAnimator.Hurt(true);
        GameObject effect = Instantiate(_hurtAttack.hurtEffect);
        effect.transform.localPosition = _hurtAttack.hurtEffectPosition;
        _playerMovement.Knockback(new Vector2(transform.root.localScale.x * -1.0f, 0.0f), _hurtAttack.knockback, _hurtAttack.knockbackDuration);
        if (_hurtAttack.cameraShaker != null)
        {
            CameraShake.Instance.Shake(_hurtAttack.cameraShaker.intensity, _hurtAttack.cameraShaker.timer);
        }
        GameManager.Instance.HitStop(_hurtAttack.hitstop);
        StartCoroutine(StunCoroutine(_hurtAttack.hitStun));
    }

    IEnumerator StunCoroutine(float hitStun)
    {
        yield return new WaitForSeconds(hitStun);
        // _playerUI.UpdateHealthDamaged();
        // _otherPlayerUI.ResetCombo();
        ToIdleState();
    }

    private void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = Vector2.zero;
    }
}