using System.Collections;
using UnityEngine;

public class CpuController : BaseController
{
    private Transform _otherPlayer;
    private Coroutine _movementCoroutine;
    private Coroutine _attackCoroutine;
    private float _movementInputX;
    private float _distance;
    private float _arcanaTimer;
    private float _attackTimer;
    private float _movementTimer;

    public void SetOtherPlayer(Transform otherPlayer)
    {
        _otherPlayer = otherPlayer;
    }

    void Update()
    {
        if (!GameManager.Instance.IsCpuOff && !_playerMovement.FullyLockMovement && !_player.IsAttacking)
        {
            _distance = Mathf.Abs(_otherPlayer.transform.position.x - transform.position.x);
            _playerMovement.MovementInput = new Vector2(_movementInputX, 0.0f);
            Arcana();
            Movement();
            Attack();
        }
        else
        {
            if (TrainingSettings.Stance == 0)
            {
                _playerMovement.StandUpAction();
            }
            if (TrainingSettings.Stance == 1)
            {
                _playerMovement.CrouchAction();
            }
            if (TrainingSettings.Stance == 2)
            {
                _playerMovement.JumpAction();
            }
            _playerMovement.MovementInput = Vector2.zero;
        }
    }

    private void Movement()
    {
        if (IsControllerEnabled)
        {
            _movementTimer -= Time.deltaTime;
            if (_movementTimer < 0)
            {
                int movementRandom;
                if (_distance <= 6.5f)
                {
                    movementRandom = Random.Range(0, 6);
                }
                else
                {
                    movementRandom = Random.Range(0, 9);
                }
                int jumpRandom = Random.Range(0, 8);
                int crouchRandom = Random.Range(0, 12);
                int standingRandom = Random.Range(0, 4);
                _movementInputX = movementRandom switch
                {
                    1 => 0.0f,
                    2 => transform.localScale.x * -1.0f,
                    _ => transform.localScale.x * 1.0f,
                };
                if (jumpRandom == 2)
                {
                    if (GameManager.Instance.HasGameStarted)
                    {
                        _playerMovement.JumpAction();
                    }
                }
                if (crouchRandom == 2)
                {
                    _playerMovement.CrouchAction();
                }
                if (_playerMovement.IsCrouching)
                {
                    if (standingRandom == 2)
                    {
                        _playerMovement.StandUpAction();
                    }
                }
                _movementTimer = Random.Range(0.2f, 0.35f);
            }
        }
    }

    private void Attack()
    {
        if (IsControllerEnabled)
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer < 0)
            {
                int attackRandom = Random.Range(0, 8);
                if (attackRandom <= 2)
                {
                    _player.LightAction();
                }
                else if (attackRandom <= 4)
                {
                    _player.MediumAction();
                }
                else if (attackRandom <= 6)
                {
                    _player.HeavyAction();
                }
                else
                {
                    _player.ThrowAction();
                }
                _attackTimer = Random.Range(0.15f, 0.35f);
            }
        }
    }

    private void Arcana()
    {
        if (IsControllerEnabled)
        {
            _arcanaTimer -= Time.deltaTime;
            if (_arcanaTimer < 0)
            {
                int arcanaRandom = Random.Range(0, 2);
                if (arcanaRandom == 0)
                {
                    _player.AssistAction();
                }
                else if (arcanaRandom == 1)
                {
                    _player.ArcaneAction();
                }
                _attackTimer = Random.Range(0.15f, 0.35f);
                _arcanaTimer = Random.Range(0.4f, 0.85f);
            }
        }
    }

    public override void ActivateInput()
    {
        if (!GameManager.Instance.IsCpuOff)
        {
            base.ActivateInput();
        }
    }


    public override void DeactivateInput()
    {
        if (!GameManager.Instance.IsCpuOff)
        {
            base.DeactivateInput();
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
                _movementCoroutine = null;
            }
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
        }
    }
}