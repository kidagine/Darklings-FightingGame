using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE_WIN
        Application.Quit();
#endif
#if UNITY_WEBGL
		Application.OpenURL("https://gamejolt.com/games/darklings/640842");
#endif
    }
}
