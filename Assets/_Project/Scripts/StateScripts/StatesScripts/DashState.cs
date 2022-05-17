using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    [SerializeField] private GameObject _playerGhostPrefab = default;
    [SerializeField] private GameObject _dashPrefab = default;
    private IdleState _idleState;
    private RunState _runState;
    private Coroutine _dashCoroutine;

    public int DashDirection { get; set; }

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _runState = GetComponent<RunState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Dash();
        _audio.Sound("Dash").Play();
        Transform dashEffect = Instantiate(_dashPrefab, transform.position, Quaternion.identity).transform;
        if (DashDirection > 0.0f)
        {
            dashEffect.position = new Vector2(dashEffect.position.x - 1.0f, dashEffect.position.y);
        }
        else
        {
            dashEffect.localScale = new Vector2(-1.0f, transform.localScale.y);
            dashEffect.position = new Vector2(dashEffect.position.x + 1.0f, dashEffect.position.y);
        }
        _rigidbody.velocity = new Vector2(DashDirection, 0.0f) * _playerStats.PlayerStatsSO.dashForce;
        _playerMovement.ZeroGravity();
        _dashCoroutine = StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject playerGhost = Instantiate(_playerGhostPrefab, transform.position, Quaternion.identity);
            playerGhost.GetComponent<PlayerGhost>().SetSprite(_playerAnimator.GetCurrentSprite(), transform.localScale.x, Color.white);
            yield return new WaitForSeconds(0.07f);
        }
        _rigidbody.velocity = Vector2.zero;
        _playerMovement.ResetGravity();
        ToIdleState();
        if (_playerController.InputDirection.x * transform.localScale.x > 0.0f)
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
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        if (_dashCoroutine != null)
        {
            StopCoroutine(_dashCoroutine);
        }
    }
}
