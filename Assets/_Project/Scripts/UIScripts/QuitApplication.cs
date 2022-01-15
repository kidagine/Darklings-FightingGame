using UnityEngine;

public class QuitApplication : MonoBehaviour
{
	public void Quit()
	{
		Application.Quit();
#if UNITY_WEBGL
		Application.OpenURL("https://gamejolt.com/games/darklings/640842");
#endif
	}
}
