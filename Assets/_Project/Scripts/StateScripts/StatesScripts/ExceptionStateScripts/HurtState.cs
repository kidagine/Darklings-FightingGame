using System.Collections;
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
        GameObject effect = Instantiate(HurtAttack.hurtEffect);
        effect.transform.localPosition = HurtAttack.hurtEffectPosition;
        _playerMovement.Knockback(new Vector2(transform.root.localScale.x * -1.0f, 0.0f), HurtAttack.knockback, HurtAttack.knockbackDuration);
        if (HurtAttack.cameraShaker != null)
        {
            CameraShake.Instance.Shake(HurtAttack.cameraShaker.intensity, HurtAttack.cameraShaker.timer);
        }
        GameManager.Instance.HitStop(HurtAttack.hitstop);
        StartCoroutine(StunCoroutine(HurtAttack.hitStun));
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