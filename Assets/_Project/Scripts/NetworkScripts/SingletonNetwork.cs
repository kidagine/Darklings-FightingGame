using Unity.Netcode;
using UnityEngine;

public class SingletonNetwork<T> : NetworkBehaviour where T : MonoBehaviour
{
	private static T _instance;

	public static T Instance
	{
		get
		{
			if ((Object)_instance == (Object)null)
			{
				_instance = Object.FindObjectOfType<T>();
				if ((Object)_instance == (Object)null)
				{
					_instance = new GameObject
					{
						name = typeof(T).ToString()
					}.AddComponent<T>();
				}
			}

			return _instance;
		}
	}

	public virtual void Awake()
	{
		if ((Object)_instance != (Object)null)
		{
			Object.Destroy(base.gameObject);
			return;
		}

		_instance = GetComponent<T>();
		_ = ((Object)_instance == (Object)null);
	}
}