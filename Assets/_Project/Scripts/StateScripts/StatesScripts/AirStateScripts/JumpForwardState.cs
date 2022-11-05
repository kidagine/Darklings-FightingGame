using FixMath.NET;
using UnityEngine;

public class JumpForwardState : AirParentState
{
    [SerializeField] protected GameObject _jumpPrefab = default;
    private Fix64 _jumpX;
    private Fix64 _jumpY;
    private int _jumpFrame = 5;
    private bool _jumpCancel;
    private readonly Fix64 _jumpForwardX = (Fix64)0.14;

    public void Initialize(bool jumpCancel = false)
    {
        _jumpCancel = jumpCancel;
    }

    public override void Enter()
    {
        base.Enter();
        Instantiate(_jumpPrefab, transform.position, Quaternion.identity);
        _audio.Sound("Jump").Play();
        _jumpFrame = 5;
        _playerAnimator.JumpForward(true);
        _playerMovement.ResetToWalkSpeed();
        if (_jumpCancel)
        {
            Debug.Log("A");
            _physics.Velocity = new FixVector2(_jumpForwardX * (Fix64)_baseController.InputDirection.x, _jumpCancelForce);
        }
        else
        {
            _physics.Velocity = new FixVector2(_jumpForwardX * (Fix64)_baseController.InputDirection.x, _player.playerStats.JumpForce + 0.01);
        }
        //_jumpY = _player.playerStats.JumpForce;
    }


    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // if (!DemonicsWorld.WaitFrames(ref _jumpFrame))
        // {
        //     _physics.Velocity = new FixVector2(_physics.Velocity.x, (Fix64)_jumpY);
        // }
        // _physics.Velocity = new FixVector2((Fix64)_jumpX, _physics.Velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        _jumpCancel = false;
    }
}
