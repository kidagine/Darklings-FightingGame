using System.Collections;
using UnityEngine;

public class TauntState : State
{
    private IdleState _idleState;
    private Coroutine _tauntCoroutine;
    private readonly float _tauntTime = 2.75f;


    private void Awake()
    {
        _idleState = GetComponent<IdleState>();
    }

    public override void Enter()
    {
        base.Enter();
        _player.CheckFlip();
        _playerAnimator.Taunt();
        _tauntCoroutine = StartCoroutine(TauntCoroutine());
    }

    IEnumerator TauntCoroutine()
    {
        yield return new WaitForSeconds(_tauntTime);
        ToIdleState();
    }

    private void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void Exit()
    {
        base.Exit();
        if (_tauntCoroutine != null)
        {
            StopCoroutine(_tauntCoroutine);
        }
    }
}
