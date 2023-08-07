using UnityEngine;
using UnityEngine.UI;

public class TopBarMenu : MonoBehaviour
{
    [SerializeField] private OptionsMenu _optionsMenu = default;
    [SerializeField] private ControlsMenu _controlsMenu = default;
    [SerializeField] private Sprite _musicOnSprite = default;
    [SerializeField] private Sprite _musicOffSprite = default;
    [SerializeField] private Sprite _fullScreenOnSprite = default;
    [SerializeField] private Sprite _fullScreenOffSprite = default;
    [SerializeField] private Image _musicImage = default;
    [SerializeField] private Image _fullScreenImage = default;
    [SerializeField] private BaseMenu _previousMenu = default;
    private AudioSource _mainMenuAudio;

    private void Start()
    {
        _mainMenuAudio = Camera.main.GetComponent<AudioSource>();
    }

    public void JoinDiscord() => Application.OpenURL("https://discord.gg/4k7PfbgN");

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        _fullScreenImage.sprite = Screen.fullScreen ? _fullScreenOffSprite : _fullScreenOnSprite;
    }
    public void ToggleMenuMusic()
    {
        _mainMenuAudio.enabled = !_mainMenuAudio.enabled;
        _musicImage.sprite = _mainMenuAudio.enabled ? _musicOffSprite : _musicOnSprite;
    }

    public void OpenControls() => _controlsMenu.Show();

    public void OpenSettings() => _optionsMenu.Show();

    public void ShowMenu(BaseMenu menu)
    {
        _previousMenu.Hide();
        menu.Show();
        _previousMenu = menu;
    }
}
