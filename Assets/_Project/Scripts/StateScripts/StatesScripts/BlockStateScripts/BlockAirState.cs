using System.Collections;
using UnityEngine;

public class BlockAirState : BlockParentState
{
	[SerializeField] protected GameObject _groundedPrefab = default;


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

	private void ToFallState()
	{
		_playerAnimator.Jump();
		_stateMachine.ChangeState(_fallState);
	}

	//public void ToBlockState()
	//{
	//	if (_playerMovement.IsGrounded && _rigidbody.velocity.y <= 0.0f)
	//	{
	//		Instantiate(_groundedPrefab, transform.position, Quaternion.identity);
	//		_audio.Sound("Landed").Play();
	//		_blockState.Initialize(_blockAttack);
	//		_stateMachine.ChangeState(_blockState);
	//	}
	//}

	public override void UpdatePhysics()
	{
		_rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y / 1.05f);
	}
}
