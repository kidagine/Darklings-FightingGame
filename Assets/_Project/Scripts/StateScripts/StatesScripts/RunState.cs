using Demonics.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    [SerializeField] private GameObject _playerGhostPrefab = default;
    private IdleState _idleState;
    private JumpForwardState _jumpForwardState;
    private Coroutine _runCoroutine;
    private ArcanaState _arcanaState;

    void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
        _arcanaState = GetComponent<ArcanaState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Run();
        _audio.Sound("Run").Play();
        _playerMovement.MovementSpeed = _playerStats.PlayerStatsSO.runSpeed;
        _runCoroutine = StartCoroutine(RunCoroutine());
    }

    IEnumerator RunCoroutine()
    {
        while (_playerController.InputDirection.x != 0.0f)
        {
            GameObject playerGhost = ObjectPoolingManager.Instance.Spawn(_playerGhostPrefab, transform.position);
            playerGhost.GetComponent<PlayerGhost>().SetSprite(_playerAnimator.GetCurrentSprite(), transform.root.localScale.x, Color.white);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToIdleState();
        ToJumpForwardState();
    }

    private void ToIdleState()
    {
        if (_playerController.InputDirection.x == 0.0f)
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    private void ToJumpForwardState()
    {
        if (_playerController.InputDirection.y > 0.0f && !_playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = true;
            _stateMachine.ChangeState(_jumpForwardState);
        }
        else if (_playerController.InputDirection.y <= 0.0f && _playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = false;
        }
    }

    public override bool ToArcanaState()
    {
        if (_player.Arcana >= 1.0f)
        {
            _stateMachine.ChangeState(_arcanaState);
            return true;
        }
        return false;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = new Vector2(1.0f * _playerMovement.MovementSpeed, _rigidbody.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        if (_runCoroutine != null)
        {
            StopCoroutine(_runCoroutine);
            _audio.Sound("Run").Stop();
        }
    }
}
