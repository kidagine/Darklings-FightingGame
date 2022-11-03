
using UnityEngine;

public class ThrowState : State
{
    private IdleState _idleState;
    private KnockbackState _knockbackState;
    private bool _flip;


    private void Awake()
    {
        _idleState = GetComponent<IdleState>();
        _knockbackState = GetComponent<KnockbackState>();
    }

    public void Initialize(bool flip)
    {
        _flip = flip;
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetSpriteOrderPriority();
        if (_flip)
        {
            _player.Flip((int)transform.root.localScale.x * -1);
        }
        _playerAnimator.OnCurrentAnimationFinished.AddListener(ToIdleState);
        _playerAnimator.OnCurrentAnimationFinished.AddListener(() => { _player.OtherPlayerStateManager.TryToKnockdownState(); });
        _playerAnimator.Throw();
    }

    private void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    public override bool ToKnockbackState()
    {
        _playerAnimator.OnCurrentAnimationFinished.RemoveAllListeners();
        _stateMachine.ChangeState(_knockbackState);
        return true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}