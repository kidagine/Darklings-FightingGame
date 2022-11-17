using UnityEngine;

public class CpuController : BaseController
{
    [SerializeField] private PlayerStateManager _playerStateMachine = default;
    private Transform _otherPlayer;
    private int _movementInputX;
    private DemonicsFloat _distance;
    private float _arcanaTimer;
    private float _movementTimer;
    private bool _crouch;
    private bool _jump;
    private bool _dash;
    private bool _reset;
    private int _attackFrames = 3;
    private int _specialFrames = 3;
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
                // Movement();
                if (_distance <= (DemonicsFloat)4)
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
                // _attackFrames = Random.Range(2, 5);
                _specialFrames = Random.Range(5, 9);
            }
        }
    }

    public override bool Crouch()
    {
        return _crouch;
    }

    public override bool StandUp()
    {
        return !_crouch;
    }

    public override bool Jump()
    {
        return _jump;
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