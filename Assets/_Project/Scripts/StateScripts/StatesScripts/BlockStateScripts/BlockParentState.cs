using Demonics.Manager;
using UnityEngine;

public class BlockParentState : State
{
    [SerializeField] private GameObject _chipEffectPrefab = default;
    [SerializeField] private GameObject _blockEffectPrefab = default;
    protected IdleState _idleState;
    protected CrouchState _crouchState;
    protected FallState _fallState;
    protected AttackState _attackState;
    protected GrabbedState _grabbedState;
    protected ShadowbreakState _shadowbreakState;
    protected HurtState _hurtState;
    protected AttackSO _blockAttack;
    private readonly int _chipDamage = 250;
    private bool _skip;
    protected int _blockFrame;

    public void Initialize(AttackSO attack, bool skip = false)
    {
        _skip = skip;
        _blockAttack = attack;
    }

    protected virtual void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _crouchState = GetComponent<CrouchState>();
        _fallState = GetComponent<FallState>();
        _attackState = GetComponent<AttackState>();
        _grabbedState = GetComponent<GrabbedState>();
        _hurtState = GetComponent<HurtState>();
        _shadowbreakState = GetComponent<ShadowbreakState>();
    }

    public override void Enter()
    {
        base.Enter();
        _audio.Sound("Block").Play();
        _blockFrame = _blockAttack.hitStun;
        _playerMovement.Knockback(new Vector2(_blockAttack.knockbackForce.x, 0), _blockAttack.knockbackDuration, (int)(_player.OtherPlayer.transform.localScale.x));
        if (!_skip)
        {
            GameObject effect;
            if (_blockAttack.isArcana)
            {
                effect = ObjectPoolingManager.Instance.Spawn(_chipEffectPrefab);
            }
            else
            {
                effect = ObjectPoolingManager.Instance.Spawn(_blockEffectPrefab);
            }
            effect.transform.localPosition = new Vector2(_player.transform.position.x + (_player.transform.localScale.x * 0.25f), _blockAttack.hurtEffectPosition.y);
            GameManager.Instance.HitStop(_blockAttack.hitstop);
        }
        if (_blockAttack.isArcana)
        {
            _player.SetHealth(_chipDamage);
            _playerUI.Damaged();
            _playerUI.UpdateHealthDamaged();
        }
    }


    public override bool ToBlockState(AttackSO attack)
    {
        this.Initialize(attack);
        _stateMachine.ChangeState(this);
        return true;
    }

    public override bool AssistCall()
    {
        if (_player.AssistGauge >= (DemonicsFloat)1)
        {
            _stateMachine.ChangeState(_shadowbreakState);
            return true;
        }
        return false;
    }

    public override bool ToHurtState(AttackSO attack)
    {
        if (attack.attackTypeEnum == AttackTypeEnum.Break)
        {
            _hurtState.Initialize(attack);
            _stateMachine.ChangeState(_hurtState);
            return true;
        }
        return false;
    }

    public override bool ToGrabbedState()
    {
        _grabbedState.Initialize(true);
        _stateMachine.ChangeState(_grabbedState);
        return true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
