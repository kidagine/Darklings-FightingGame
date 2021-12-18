using UnityEngine;

public class AssistAnimationEvents : MonoBehaviour
{
	[SerializeField] private Assist _assist = default;
	[SerializeField] private PlayerAnimationEvents _playerAnimationEvents = default;


	public void ProjectileAnimationEvent(AnimationEvent animationEvent)
	{
		_assist.Projectile();
		_playerAnimationEvents.StartFrameCount(animationEvent);
	}
}
