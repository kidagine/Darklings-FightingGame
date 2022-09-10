using Demonics.Manager;
using System.Collections;
using UnityEngine;

public class RunState : GroundParentState
{
    [SerializeField] private GameObject _playerGhostPrefab = default;
    private Coroutine _runCoroutine;

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Run();
        _audio.Sound("Run").Play();
        _playerMovement.MovementSpeed = _player.playerStats.SpeedRun;
        _runCoroutine = StartCoroutine(RunCoroutine());
    }

    IEnumerator RunCoroutine()
    {
        while (_baseController.InputDirection.x != 0.0f)
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
        if (_baseController.InputDirection.x == 0)
        {
            _stateMachine.ChangeState(_idleState);
        }
    }

    private void ToJumpForwardState()
    {
        if (_baseController.InputDirection.y > 0 && !_playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = true;
            if (_baseController.InputDirection.x != 0)
            {
                _stateMachine.ChangeState(_jumpForwardState);
            }
            else
            {
                _stateMachine.ChangeState(_jumpState);
            }
        }
        else if (_baseController.InputDirection.y <= 0 && _playerMovement.HasJumped)
        {
            _playerMovement.HasJumped = false;
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _rigidbody.velocity = new Vector2(transform.root.localScale.x * _playerMovement.MovementSpeed, _rigidbody.velocity.y);
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
