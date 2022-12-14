using UnityEngine;
using UnityEngine.SceneManagement;


public class SplashScreen : MonoBehaviour
{
    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

