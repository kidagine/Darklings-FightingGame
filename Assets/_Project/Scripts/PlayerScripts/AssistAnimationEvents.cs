using UnityEngine;

public class AssistAnimationEvents : MonoBehaviour
{
	[SerializeField] private Assist _assist = default;


	public void ProjectileAnimationEvent()
	{
		_assist.Projectile();
	}
}
