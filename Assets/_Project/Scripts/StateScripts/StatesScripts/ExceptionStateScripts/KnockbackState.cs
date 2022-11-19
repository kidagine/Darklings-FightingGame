using UnityEngine;

public class KnockbackState : State
{
    [SerializeField] private GameObject _groundedPrefab = default;
    [SerializeField] private CameraShakerSO _cameraShaker = default;
    private KnockdownState _knockdownState;
    private bool _canCheckGround;
    private readonly int _knockbackDuration = 25;
    private readonly float _knockbackForce = 2.5f;
    private int _groundCheckFrames;

    protected void Awake()
    {
        _knockdownState = GetComponent<KnockdownState>();
    }

    public override void Enter()
    {
        _playerAnimator.HurtAir();
        base.Enter();
        _physics.Velocity = DemonicsVector2.Zero;
        _playerMovement.StopKnockback();
        _playerMovement.Knockback(new Vector2(_knockbackForce, _knockbackForce), _knockbackDuration, (int)(_player.OtherPlayer.transform.localScale.x), 5, true);
        CameraShake.Instance.Shake(_cameraShaker);
        _groundCheckFrames = 8;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        ToKnockdownState();
        if (DemonicsWorld.WaitFramesOnce(ref _groundCheckFrames))
        {
            _canCheckGround = true;
        }
    }

    private new void ToKnockdownState()
    {
        if (_playerMovement.IsGrounded && _canCheckGround)
        {
            Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
            _stateMachine.ChangeState(_knockdownState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _player.OtherPlayer.StopComboTimer();
        _canCheckGround = false;
    }
}