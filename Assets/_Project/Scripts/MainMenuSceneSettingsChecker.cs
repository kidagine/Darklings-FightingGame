using TMPro;
using UnityEngine;

public class MainMenuSceneSettingsChecker : MonoBehaviour
{
    [SerializeField] private BaseMenu _mainMenu = default;
    [SerializeField] private BaseMenu _characterSelectMenu = default;

    void Awake()
    {
        Time.timeScale = 1;
        if (SceneSettings.ChangeCharacter)
        {
            _mainMenu.Hide();
            _characterSelectMenu.Show();
        }
    }

}
