using Demonics.Manager;
using System.Collections;
using UnityEngine;

public class AirDashState : State
{
    [SerializeField] private GameObject _playerGhostPrefab = default;
    [SerializeField] private GameObject _dashPrefab = default;
    private FallState _fallState;
    private JumpState _jumpState;
    private JumpForwardState _jumpForwardState;

    private Coroutine _dashCoroutine;
    public int DashDirection { get; set; }
    void Awake()
    {
        _fallState = GetComponent<FallState>();
        _jumpState = GetComponent<JumpState>();
        _jumpForwardState = GetComponent<JumpForwardState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.AirDash();
        _audio.Sound("Dash").Play();
        _player.SetPushboxTrigger(false);
        Transform dashEffect = ObjectPoolingManager.Instance.Spawn(_dashPrefab, transform.position).transform;
        if (DashDirection > 0)
        {
            dashEffect.localScale = new Vector2(1, transform.localScale.y);
            dashEffect.position = new Vector2(dashEffect.position.x - 1, dashEffect.position.y);
        }
        else
        {
            dashEffect.localScale = new Vector2(-1, transform.root.localScale.y);
            dashEffect.position = new Vector2(dashEffect.position.x + 1, dashEffect.position.y);
        }
        _rigidbody.velocity = new Vector2(DashDirection, 0) * _player.playerStats.dashForce;
        _playerMovement.ZeroGravity();
        _dashCoroutine = StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        _playerMovement.HasAirDashed = true;
        for (int i = 0; i < 3; i++)
        {
            GameObject playerGhost = ObjectPoolingManager.Instance.Spawn(_playerGhostPrefab, transform.position);
            playerGhost.GetComponent<PlayerGhost>().SetSprite(_playerAnimator.GetCurrentSprite(), transform.root.localScale.x, Color.white);
            yield return new WaitForSeconds(0.07f);
        }
        _rigidbody.velocity = Vector2.zero;
        _playerMovement.ResetGravity();
        yield return null;
        _dashCoroutine = null;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToFallState();
        ToJumpState();
        ToJumpForwardState();
    }

    public void ToFallState()
    {
        if (_dashCoroutine == null)
        {
            _stateMachine.ChangeState(_fallState);
        }
    }

    public void ToJumpState()
    {
        if (_player.playerStats.canDoubleJump && !_playerMovement.HasDoubleJumped && _dashCoroutine == null)
        {
            if (_baseController.InputDirection.x == 0)
            {
                if (_baseController.InputDirection.y > 0 && !_playerMovement.HasJumped)
                {
                    _playerMovement.HasDoubleJumped = true;
                    _playerMovement.HasJumped = true;
                    _stateMachine.ChangeState(_jumpState);
                }
                else if (_baseController.InputDirection.y <= 0 && _playerMovement.HasJumped)
                {
                    _playerMovement.HasJumped = false;
                }
            }
        }
    }

    public void ToJumpForwardState()
    {
        if (_player.playerStats.canDoubleJump && !_playerMovement.HasDoubleJumped && _dashCoroutine == null)
        {
            if (_baseController.InputDirection.x != 0)
            {
                if (_baseController.InputDirection.y > 0 && !_playerMovement.HasJumped)
                {
                    _playerMovement.HasDoubleJumped = true;
                    _playerMovement.HasJumped = true;
                    _stateMachine.ChangeState(_jumpForwardState);
                }
                else if (_baseController.InputDirection.y <= 0 && _playerMovement.HasJumped)
                {
                    _playerMovement.HasJumped = false;
                }
            }
        }
    }

    public override bool AssistCall()
    {
        _player.AssistAction();
        return true;
    }

    public override void Exit()
    {
        base.Exit();
        if (_dashCoroutine != null)
        {
            _playerMovement.ResetGravity();
            StopCoroutine(_dashCoroutine);
            _rigidbody.velocity = Vector2.zero;
        }
    }
}

