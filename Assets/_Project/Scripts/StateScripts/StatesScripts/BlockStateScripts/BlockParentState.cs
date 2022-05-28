using System.Collections;
using UnityEngine;

public class BlockParentState : State
{
	protected IdleState _idleState;
	protected AttackSO _blockAttack;
	private Coroutine _blockCoroutine;

	public void Initialize(AttackSO attack)
	{
		_blockAttack = attack;
	}

	void Awake()
	{
		_idleState = GetComponent<IdleState>();
	}

	public override void Enter()
	{
		base.Enter();
		_blockCoroutine = StartCoroutine(BlockCoroutine(_blockAttack.hitStun));
	}

	IEnumerator BlockCoroutine(float blockStun)
	{
		yield return new WaitForSeconds(blockStun);
		ToIdleState();
	}

	private void ToIdleState()
	{
		_stateMachine.ChangeState(_idleState);
	}

	public override void UpdatePhysics()
	{
		base.UpdatePhysics();
		_rigidbody.velocity = Vector2.zero;
	}

	public override void Exit()
	{
		base.Exit();
		if (_blockCoroutine != null)
		{
			StopCoroutine(_blockCoroutine);
		}
	}
}
