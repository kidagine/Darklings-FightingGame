using Demonics.Manager;
using UnityEngine;

public class RedFrenzyState : State
{
    [SerializeField] private GameObject _teleportDisappearEffect = default;
    [SerializeField] private GameObject _teleportAppearEffect = default;
    private IdleState _idleState;
    private HurtState _hurtState;
    private AirHurtState _airHurtState;
    private int _startTeleportFrame;
    private int _midTeleportFrame;
    private int _endTeleportFrame;


    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _hurtState = GetComponent<HurtState>();
        _airHurtState = GetComponent<AirHurtState>();
    }

    public override void Enter()
    {
        base.Enter();
        _startTeleportFrame = 7;
        _playerUI.Damaged();
        _player.CurrentAttack = _player.playerStats.mRedFrenzy;
        _player.CheckFlip();
        _player.SetSpriteOrderPriority();
        _audio.Sound("Vanish").Play();
        _playerAnimator.RedFrenzy();
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
    }

    private new void ToIdleState()
    {
        if (_stateMachine.CurrentState == this)
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _rigidbody.velocity = Vector2.zero;
        _playerMovement.ZeroGravity();
        if (DemonicsPhysics.WaitFramesOnce(ref _startTeleportFrame))
        {
            StartTeleportToOpponent();
        }
        if (_midTeleportFrame > 0)
        {
            if (DemonicsPhysics.WaitFramesOnce(ref _midTeleportFrame))
            {
                MidTeleportToOpponent();
            }
        }
        if (_endTeleportFrame > 0)
        {
            if (DemonicsPhysics.WaitFramesOnce(ref _endTeleportFrame))
            {
                EndTeleportToOpponent();
            }
        }
    }

    private void StartTeleportToOpponent()
    {
        _player.SetInvinsible(true);
        ObjectPoolingManager.Instance.Spawn(_teleportDisappearEffect, transform.root.position);
        _midTeleportFrame = 15;
        GameManager.Instance.GlobalHitstop(22);
    }

    private void MidTeleportToOpponent()
    {
        if (_player.transform.localScale.x > 0)
        {
            _player.transform.position = new Vector2(_player.OtherPlayer.transform.position.x - 1.5f, _player.OtherPlayer.transform.position.y);
        }
        else
        {
            _player.transform.position = new Vector2(_player.OtherPlayer.transform.position.x + 1.5f, _player.OtherPlayer.transform.position.y);
        }
        ObjectPoolingManager.Instance.Spawn(_teleportAppearEffect, transform.root.position);
        _endTeleportFrame = 11;
    }

    private void EndTeleportToOpponent()
    {
        _audio.Sound(_player.CurrentAttack.attackSound).Play();
        _player.SetInvinsible(false);
    }

    public override bool ToHurtState(AttackSO attack)
    {
        if (_player.CanTakeSuperArmorHit(attack))
        {
            _audio.Sound(attack.impactSound).Play();
            _player.HurtOnSuperArmor(attack);
            return false;
        }
        _player.OtherPlayerUI.DisplayNotification(NotificationTypeEnum.Punish);
        if (_playerMovement.IsGrounded)
        {
            _hurtState.Initialize(attack);
            _stateMachine.ChangeState(_hurtState);
        }
        else
        {
            _airHurtState.Initialize(attack);
            _stateMachine.ChangeState(_airHurtState);
        }
        return true;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        _audio.Sound("Hit").Play();
        _player.HurtOnSuperArmor(attack);
        return false;
    }


    public override void Exit()
    {
        base.Exit();
        _player.CanSkipAttack = false;
        _playerUI.UpdateHealthDamaged();
        _playerMovement.ResetGravity();
    }
}
