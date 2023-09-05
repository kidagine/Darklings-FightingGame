using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopBarMenu : MonoBehaviour
{
    [SerializeField] private OptionsMenu _optionsMenu = default;
    [SerializeField] private PromptsInput _hotBarPrompts = default;
    [SerializeField] private ControlsMenu _controlsMenu = default;
    [SerializeField] private Sprite _musicOnSprite = default;
    [SerializeField] private Sprite _musicOffSprite = default;
    [SerializeField] private Sprite _fullScreenOnSprite = default;
    [SerializeField] private Sprite _fullScreenOffSprite = default;
    [SerializeField] private Image _musicImage = default;
    [SerializeField] private Image _fullScreenImage = default;
    [SerializeField] private BaseMenu _previousMenu = default;
    [SerializeField] private GameObject _backgroundImage = default;
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private Animator _animator;
    [SerializeField] private Canvas _canvas;
    private AudioSource _mainMenuAudio;
    private GameObject _previousSelectable;
    public bool Active { get { return _backgroundImage.activeSelf; } private set { } }

    private void Start() => _mainMenuAudio = Camera.main.GetComponent<AudioSource>();

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

    public void HideHotBar(GameObject hotBarToggle)
    {
        _canvas.sortingOrder = 0;
        _backgroundImage.SetActive(false);
        _previousSelectable = hotBarToggle;
        _animator.SetBool("Appear", false);
        _animator.SetBool("Disappear", true);
    }

    public void ShowHotBar()
    {
        _canvas.sortingOrder = 1;
        _inputManager.SetPrompts(_hotBarPrompts);
        _backgroundImage.SetActive(true);
        _animator.SetBool("Appear", true);
        _animator.SetBool("Disappear", false);
        EventSystem.current.SetSelectedGameObject(_previousSelectable);
    }
}
