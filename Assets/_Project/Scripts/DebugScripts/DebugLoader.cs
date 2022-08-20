using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugLoader : MonoBehaviour
{
    [SerializeField] private bool _loadDebug = default;


    void Awake()
    {
        if (_loadDebug && !SceneManager.GetSceneByName("DebugScene").isLoaded)
        {
            SceneManager.LoadScene("DebugScene", LoadSceneMode.Additive);
        }
    }
}
