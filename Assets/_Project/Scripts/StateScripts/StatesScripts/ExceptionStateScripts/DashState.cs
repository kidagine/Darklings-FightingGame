using Demonics.Manager;
using System.Collections;
using UnityEngine;

public class DashState : State
{
    [SerializeField] private GameObject _playerGhostPrefab = default;
    [SerializeField] private GameObject _dashPrefab = default;
    private IdleState _idleState;
    private RunState _runState;
    private HurtState _hurtState;
    private AirborneHurtState _airborneHurtState;
    private RedFrenzyState _redFrenzyState;
    private Coroutine _dashCoroutine;

    public int DashDirection { get; set; }

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _runState = GetComponent<RunState>();
        _hurtState = GetComponent<HurtState>();
        _airborneHurtState = GetComponent<AirborneHurtState>();
        _redFrenzyState = GetComponent<RedFrenzyState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Dash();
        _audio.Sound("Dash").Play();
        Transform dashEffect = ObjectPoolingManager.Instance.Spawn(_dashPrefab, transform.root.position).transform;
        if (DashDirection > 0)
        {
            dashEffect.localScale = new Vector2(1, transform.localScale.y);
            dashEffect.position = new Vector2(dashEffect.position.x - 1, dashEffect.position.y);
        }
        else
        {
            dashEffect.localScale = new Vector2(-1, transform.localScale.y);
            dashEffect.position = new Vector2(dashEffect.position.x + 1, dashEffect.position.y);
        }
        _rigidbody.velocity = new Vector2(DashDirection, 0) * _player.playerStats.dashForce;
        _playerMovement.ZeroGravity();
        _dashCoroutine = StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject playerGhost = ObjectPoolingManager.Instance.Spawn(_playerGhostPrefab, transform.position);
            playerGhost.GetComponent<PlayerGhost>().SetSprite(_playerAnimator.GetCurrentSprite(), transform.root.localScale.x, Color.white);
            yield return new WaitForSeconds(0.07f);
        }
        _rigidbody.velocity = Vector2.zero;
        _playerMovement.ResetGravity();
        ToIdleState();
        _inputBuffer.CheckInputBuffer();
        if (_baseController.InputDirection.x * transform.root.localScale.x > 0)
        {
            ToRunState();
        }
        yield return null;
    }

    private void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    private void ToRunState()
    {
        _stateMachine.ChangeState(_runState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _player.CheckFlip();
    }

    public override bool AssistCall()
    {
        _player.AssistAction();
        return true;
    }

    public override bool ToRedFrenzyState()
    {
        _stateMachine.ChangeState(_redFrenzyState);
        return true;
    }

    public override bool ToHurtState(AttackSO attack)
    {
        _hurtState.Initialize(attack);
        _stateMachine.ChangeState(_hurtState);
        return true;
    }

    public override bool ToAirborneHurtState(AttackSO attack)
    {
        _airborneHurtState.Initialize(attack);
        _stateMachine.ChangeState(_airborneHurtState);
        return true;
    }

    public override void Exit()
    {
        base.Exit();
        if (_dashCoroutine != null)
        {
            _playerMovement.ResetGravity();
            StopCoroutine(_dashCoroutine);
        }
    }
}
