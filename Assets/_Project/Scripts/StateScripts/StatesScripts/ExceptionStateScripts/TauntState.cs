using System.Collections;
using UnityEngine;

public class TauntState : State
{
    private IdleState _idleState;
    private int _tauntFrame;


    private void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.CheckFlip();
        _playerAnimator.Taunt();
        _tauntFrame = 160;
    }

    private new void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (DemonicsWorld.WaitFramesOnce(ref _tauntFrame))
        {
            ToIdleState();
        }
    }
}
