using Demonics.UI;
using UnityEngine;

public class MainMenuSceneSettingsChecker : MonoBehaviour
{
    [SerializeField] private BaseMenu _mainMenuOverlay = default;
    [SerializeField] private BaseMenu _mainMenu = default;
    [SerializeField] private BaseMenu _characterSelectMenu = default;


    void Awake()
    {
        if (SceneSettings.ChangeCharacter && SceneSettings.ControllerOne != "" && SceneSettings.ControllerTwo != "")
        {
            _mainMenuOverlay.Hide();
            _mainMenu.Hide();
            _characterSelectMenu.Show();
        }
    }
}
