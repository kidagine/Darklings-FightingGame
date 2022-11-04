using UnityEngine;

public class IntroState : State
{
    private TauntState _tauntState;


    private void Awake()
    {
        _tauntState = GetComponent<TauntState>();
    }

    public override void Enter()
    {
        base.Enter();
        _playerAnimator.Idle();
        _player.CheckFlip();
    }

    public override bool ToTauntState()
    {
        _stateMachine.ChangeState(_tauntState);
        return true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
