using System.Collections;
using UnityEngine;

public class BlockAirState : BlockParentState
{
	[SerializeField] protected GameObject _groundedPrefab = default;
	private BlockState _blockState;


	protected override void Awake()
	{
		base.Awake();
		_blockState = GetComponent<BlockState>();
	}

	public override void Enter()
	{
		base.Enter();
		_playerAnimator.BlockAir();
		_blockCoroutine = StartCoroutine(BlockCoroutine(_blockAttack.hitStun));
	}

	IEnumerator BlockCoroutine(float blockStun)
	{
		yield return new WaitForSeconds(blockStun);
		ToFallState();
	}

	public override void UpdateLogic()
	{
		base.UpdateLogic();
		ToBlockState();
	}

	private void ToFallState()
	{
		_playerAnimator.Jump();
		_stateMachine.ChangeState(_fallState);
	}

	private void ToBlockState()
	{
		if (_playerMovement.IsGrounded)
		{
			Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
			_audio.Sound("Landed").Play();
			_blockState.Initialize(_blockAttack);
			_stateMachine.ChangeState(_blockState);
		}
	}

	public override void UpdatePhysics()
	{
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y / 1.05f);
	}
}
