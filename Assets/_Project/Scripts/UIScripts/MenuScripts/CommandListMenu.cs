using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class CommandListMenu : BaseMenu
{
    [SerializeField] private TextMeshProUGUI _characterText = default;
    [SerializeField] private TextMeshProUGUI _descriptionText = default;
    [SerializeField] private VideoPlayer _showcaseVideo = default;
    [SerializeField] private PauseMenu _pauseMenu = default;
    [SerializeField] private PauseMenu _pauseTrainingMenu = default;
    [SerializeField] private CommandFramedata _commandFramedata = default;
    [SerializeField] private CommandListButton[] _commandListButtons = default;
    [SerializeField] private GameObject _toggleFramedataPrompt = default;
    [SerializeField] private GameObject _knockdownImage = default;
    [SerializeField] private GameObject _reversalImage = default;
    [SerializeField] private GameObject _projectileImage = default;
    [SerializeField] private GameObject _videoMenu = default;
    [SerializeField] private GameObject _framedataMenu = default;

    private readonly string _baseUrl = "https://kidagine.github.io/Darklings-CommandListVideos/";
    private Player _playerOne;
    private Player _playerTwo;
    private Player _currentlyDisplayedPlayer;

    public PauseMenu CurrentPauseMenu { get; private set; }

    void Awake()
    {
        _playerOne = GameManager.Instance.PlayerOne;
        _playerTwo = GameManager.Instance.PlayerTwo;
    }

    public void ChangePage()
    {
        if (_playerOne == _currentlyDisplayedPlayer)
        {
            _currentlyDisplayedPlayer = _playerTwo;
        }
        else
        {
            _currentlyDisplayedPlayer = _playerOne;
        }
        SetCommandListData(_currentlyDisplayedPlayer.PlayerStats);
        EventSystem.current.SetSelectedGameObject(null);
        _startingOption.Select();
    }

    private void SetCommandListData(PlayerStatsSO playerStats)
    {
        _characterText.text = playerStats.characterName.ToString();
        _commandListButtons[0].SetData(playerStats.m5Arcana);
        _commandListButtons[1].SetData(playerStats.m2Arcana);
        if (playerStats.jArcana != null)
        {
            _commandListButtons[2].SetData(playerStats.jArcana);
            _commandListButtons[2].gameObject.SetActive(true);
        }
        else
        {
            _commandListButtons[2].gameObject.SetActive(false);
        }
        _commandListButtons[3].SetData(playerStats.m5L);
        _commandListButtons[4].SetData(playerStats.m2L);
        _commandListButtons[5].SetData(playerStats.jL);
        _commandListButtons[6].SetData(playerStats.m5M);
        _commandListButtons[7].SetData(playerStats.m2M);
        _commandListButtons[8].SetData(playerStats.jM);
        _commandListButtons[9].SetData(playerStats.m5H);
        _commandListButtons[10].SetData(playerStats.m2H);
        _commandListButtons[11].SetData(playerStats.jL);
    }

    public void SetCommandListShowcase(ArcanaSO command)
    {
        _descriptionText.text = command.moveDescription;
#if UNITY_STANDALONE_WIN
        _showcaseVideo.clip = command.moveVideo;
#endif
#if UNITY_WEBGL
        _showcaseVideo.url = _baseUrl + command.moveVideo.name + ".mp4";
#endif
        _showcaseVideo.Stop();
        _showcaseVideo.Play();
        _reversalImage.SetActive(false);
        _knockdownImage.SetActive(false);
        _projectileImage.SetActive(false);
        if (command.reversal)
        {
            _reversalImage.SetActive(true);
        }
        if (command.causesKnockdown)
        {
            _knockdownImage.SetActive(true);
        }
        if (command.isProjectile)
        {
            _projectileImage.SetActive(true);
        }
        _commandFramedata.SetFramedata(command);
        _toggleFramedataPrompt.SetActive(true);
    }

    public void SetCommandListShowcase(AttackSO command)
    {
        _videoMenu.SetActive(false);
        _framedataMenu.SetActive(true);
        _commandFramedata.SetFramedata(command);
        _toggleFramedataPrompt.SetActive(false);
    }

    public void ToggleFramedata()
    {
        if (_toggleFramedataPrompt.activeSelf)
            if (_videoMenu.activeSelf)
            {
                _videoMenu.SetActive(false);
                _framedataMenu.SetActive(true);
            }
            else
            {
                _videoMenu.SetActive(true);
                _framedataMenu.SetActive(false);
                _showcaseVideo.Stop();
                _showcaseVideo.Play();
            }
    }

    public void Back()
    {
        CurrentPauseMenu.Show();
        Hide();
    }

    private void OnEnable()
    {
        if (GameManager.Instance.IsTrainingMode)
        {
            CurrentPauseMenu = _pauseTrainingMenu;
        }
        else
        {
            CurrentPauseMenu = _pauseMenu;
        }
        if (_pauseMenu.PlayerOnePaused)
        {
            _currentlyDisplayedPlayer = _playerOne;
        }
        else
        {
            _currentlyDisplayedPlayer = _playerTwo;
        }
        SetCommandListData(_currentlyDisplayedPlayer.PlayerStats);
    }
}
