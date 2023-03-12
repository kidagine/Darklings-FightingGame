using UnityEngine;

namespace Demonics.Utility
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		void Awake()
		{
			DontDestroyOnLoad(this);
		}
	}
}