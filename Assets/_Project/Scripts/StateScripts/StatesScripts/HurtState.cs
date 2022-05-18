using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    private IdleState _idleState;
    public AttackSO HurtAttack { get; set; }

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        _audio.Sound(HurtAttack.impactSound).Play();
        _playerAnimator.Hurt(true);
        _playerMovement.StopGhosts();
        GameObject effect = Instantiate(HurtAttack.hurtEffect);
        effect.transform.localPosition = HurtAttack.hurtEffectPosition;
        StartCoroutine(StunCoroutine(HurtAttack.hitStun));
    }

    IEnumerator StunCoroutine(float hitStun)
    {
        yield return new WaitForSeconds(hitStun);
        // _playerUI.UpdateHealthDamaged();
        // _otherPlayerUI.ResetCombo();
        ToIdleState();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
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