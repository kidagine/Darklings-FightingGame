using UnityEngine;

namespace Demonics.Utility
{
	public class DestroyAnimationEvent : MonoBehaviour
	{
		public void Destroy()
		{
			Destroy(gameObject);
		}
	}
}