using UnityEngine;

public class CpuController : BaseController
{
    [SerializeField] private PlayerStateManager _playerStateMachine = default;
    private Transform _otherPlayer;
    private DemonicsFloat _distance;
    private bool _crouch;
    private bool _jump;
    private bool _reset;
    private int _attackFrames = 3;
    private int _specialFrames = 4;
    private int _movementFrames = 2;
    public void SetOtherPlayer(Transform otherPlayer)
    {
        _otherPlayer = otherPlayer;
    }


    void FixedUpdate()
    {
        if (GameManager.Instance.HasGameStarted)
        {
            if (!TrainingSettings.CpuOff || !SceneSettings.IsTrainingMode)
            {
                _reset = false;
                _distance = DemonicsFloat.Abs(_playerMovement.Physics.Position.x - _player.OtherPlayerMovement.Physics.Position.x);
                Movement();
                if (_distance <= (DemonicsFloat)3.5)
                {
                    Attack();
                }
                Specials();
            }
            else
            {
                if (!_reset)
                {
                    _reset = true;
                    InputDirection = Vector2Int.zero;
                    _playerStateMachine.ResetToInitialState();
                }
            }
        }
        else
        {
            InputDirection = Vector2Int.zero;
        }
    }

    private void Movement()
    {
        if (IsControllerEnabled)
        {
            if (DemonicsWorld.WaitFramesOnce(ref _movementFrames))
            {
                int jumpRandom = Random.Range(0, 12);
                int crouchRandom = Random.Range(0, 12);
                int standingRandom = Random.Range(0, 4);
                int movementRandom;
                if (_distance <= (DemonicsFloat)6.5)
                {
                    movementRandom = Random.Range(0, 6);
                }
                else
                {
                    movementRandom = Random.Range(0, 9);
                }
                switch (movementRandom)
                {
                    case 0:
                        _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.NoneVertical);
                        break;
                    case > 0 and <= 4:
                        float _movementInputLeft = (int)(transform.localScale.x * 1);
                        if (_movementInputLeft == 1)
                        {
                            _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Right);
                        }
                        else if (_movementInputLeft == -1)
                        {
                            _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Left);
                        }
                        break;
                    case > 5:
                        float _movementInputRight = (int)(transform.localScale.x * -1);
                        if (_movementInputRight == 1)
                        {
                            _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Right);
                        }
                        else if (_movementInputRight == -1)
                        {
                            _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Left);
                        }
                        break;
                }
                if (jumpRandom == 2)
                {
                    _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Up);
                }
                if (crouchRandom == 2)
                {
                    _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.Down);
                }
                _movementFrames = Random.Range(10, 15);
            }
        }
    }

    private void Attack()
    {
        if (IsControllerEnabled)
        {
            if (DemonicsWorld.WaitFramesOnce(ref _attackFrames))
            {
                int attackRandom = Random.Range(0, 8);
                if (attackRandom <= 2)
                {
                    _inputBuffer.AddInputBufferItem(InputEnum.Light);
                }
                else if (attackRandom <= 4)
                {
                    _inputBuffer.AddInputBufferItem(InputEnum.Medium);
                }
                else if (attackRandom <= 6)
                {
                    _inputBuffer.AddInputBufferItem(InputEnum.Heavy);
                }
                else
                {
                    _inputBuffer.AddInputBufferItem(InputEnum.Throw);
                }
                _attackFrames = Random.Range(2, 5);
            }
        }
    }

    private void Specials()
    {
        if (IsControllerEnabled)
        {
            if (DemonicsWorld.WaitFramesOnce(ref _specialFrames))
            {
                int arcanaRandom = Random.Range(0, 2);
                if (arcanaRandom == 0)
                {
                    _inputBuffer.AddInputBufferItem(InputEnum.Assist);
                }
                else if (arcanaRandom == 1)
                {
                    _inputBuffer.AddInputBufferItem(InputEnum.Special);
                }
                _specialFrames = Random.Range(5, 9);
            }
        }
    }

    public override bool Jump()
    {
        if (InputDirection.y > 0)
        {
            return true;
        }
        return false;
    }

    public override bool Crouch()
    {
        if (InputDirection.y < 0)
        {
            return true;
        }
        return false;
    }

    public override bool StandUp()
    {
        if (InputDirection.y == 0)
        {
            _inputBuffer.AddInputBufferItem(InputEnum.Direction, InputDirectionEnum.NoneVertical);
            return true;
        }
        return false;
    }

    public override void ActivateInput()
    {
        if (!TrainingSettings.CpuOff)
        {
            base.ActivateInput();
        }
    }

    public override void DeactivateInput()
    {
        if (!TrainingSettings.CpuOff)
        {
            base.DeactivateInput();
        }
    }
}