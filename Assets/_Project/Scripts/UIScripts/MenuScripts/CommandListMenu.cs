using System.Text.RegularExpressions;
using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class CommandListMenu : BaseMenu
{
    [SerializeField] private TextMeshProUGUI[] _characterText = default;
    [SerializeField] private TextMeshProUGUI _descriptionText = default;
    [SerializeField] private VideoPlayer _showcaseVideo = default;
    [SerializeField] private PauseMenu _pauseMenu = default;
    [SerializeField] private PauseMenu _pauseTrainingMenu = default;
    [SerializeField] private RectTransform _characterContent = default;
    [SerializeField] private RectTransform _commonContent = default;
    [SerializeField] private GameObject _characterMovesPage = default;
    [SerializeField] private GameObject _commonMovesPage = default;
    [SerializeField] private Button _characterMovesButton = default;
    [SerializeField] private Button _commonMovesButton = default;
    [SerializeField] private GameObject[] _slides = default;
    [SerializeField] private GameObject[] _subSlides = default;
    [SerializeField] private CommandFramedata _commandFramedata = default;
    [SerializeField] private CommandListButton[] _commandListButtons = default;
    [SerializeField] private GameObject _toggleFramedataPrompt = default;
    [SerializeField] private GameObject _knockdownImage = default;
    [SerializeField] private GameObject _reversalImage = default;
    [SerializeField] private GameObject _projectileImage = default;
    [SerializeField] private GameObject _videoMenu = default;
    [SerializeField] private GameObject _framedataMenu = default;
    private int _currentPage;
    private readonly string _baseUrl = "https://kidagine.github.io/Darklings-CommandListVideos/";
    private Player _playerOne;
    private Player _playerTwo;
    private Player _currentlyDisplayedPlayer;
    private Player _otherDisplayedPlayer;


    public PauseMenu CurrentPauseMenu { get; private set; }

    void Awake()
    {
        _playerOne = GameManager.Instance.PlayerOne;
        _playerTwo = GameManager.Instance.PlayerTwo;
    }

    public void NextSubPage()
    {
        if (_subSlides[0].activeSelf)
        {
            _subSlides[0].SetActive(false);
            _subSlides[1].SetActive(true);
        }
        else
        {
            _subSlides[0].SetActive(true);
            _subSlides[1].SetActive(false);
        }
    }

    public void NextPage()
    {
        _currentPage++;
        if (_currentPage > _slides.Length - 1)
        {
            _currentPage = 0;
        }
        if (_currentPage == 0)
        {
            SetCommandListData(_playerOne.playerStats);
        }
        else
        {
            SetCommandListData(_playerTwo.playerStats);
        }
        EventSystem.current.SetSelectedGameObject(null);
        SetPageInfo();
    }

    public void PreviousPage()
    {
        _currentPage--;
        if (_currentPage < 0)
        {
            _currentPage = _slides.Length - 1;
        }
        if (_currentPage == 0)
        {
            SetCommandListData(_playerOne.playerStats);
        }
        else
        {
            SetCommandListData(_playerTwo.playerStats);
        }
        SetPageInfo();
    }

    private void SetPageInfo()
    {
        for (int i = 0; i < _slides.Length; i++)
        {
            _slides[i].SetActive(false);
        }
        string playerOne = Regex.Replace(_playerOne.PlayerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
        string playerTwo = Regex.Replace(_playerTwo.PlayerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
        if (_currentPage == 0)
        {
            _characterContent.anchoredPosition = Vector2.zero;
            _characterMovesButton.Select();
            _characterMovesPage.SetActive(true);
            _commonMovesPage.SetActive(false);
            _characterText[0].text = playerOne;
            _characterText[1].text = playerTwo;
            _characterText[2].text = "Common Moves";
        }
        else if (_currentPage == 1)
        {
            _characterContent.anchoredPosition = Vector2.zero;
            _characterMovesButton.Select();
            _characterMovesPage.SetActive(true);
            _commonMovesPage.SetActive(false);
            _characterText[0].text = playerTwo;
            _characterText[1].text = "Common Moves";
            _characterText[2].text = playerOne;
        }
        else
        {
            _commonContent.anchoredPosition = Vector2.zero;
            _commonMovesButton.Select();
            _characterMovesPage.SetActive(false);
            _commonMovesPage.SetActive(true);
            _characterText[0].text = "Common Moves";
            _characterText[1].text = playerOne;
            _characterText[2].text = playerTwo;
        }
        _slides[_currentPage].SetActive(true);
    }

    public void ChangePage()
    {
        _currentPage++;
        if (_playerOne == _currentlyDisplayedPlayer)
        {
            _currentlyDisplayedPlayer = _playerTwo;
        }
        else
        {
            _currentlyDisplayedPlayer = _playerOne;
        }
        SetCommandListData(_currentlyDisplayedPlayer.PlayerStats);
    }

    private void SetCommandListData(PlayerStatsSO playerStats)
    {
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
        // _commandListButtons[11].SetData(playerStats.jH);
        _commandListButtons[12].SetData(playerStats.mThrow);
        _commandListButtons[13].SetData(playerStats.mParry);
        _commandListButtons[14].SetData(playerStats.jL);
        SetPageInfo();
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
        _characterContent.anchoredPosition = Vector2.zero;
        _commonContent.anchoredPosition = Vector2.zero;
        if (GameManager.Instance.IsTrainingMode)
        {
            CurrentPauseMenu = _pauseTrainingMenu;
        }
        else
        {
            CurrentPauseMenu = _pauseMenu;
        }
        if (CurrentPauseMenu.PlayerOnePaused)
        {
            _currentlyDisplayedPlayer = _playerOne;
            _otherDisplayedPlayer = _playerTwo;
        }
        else
        {
            _currentlyDisplayedPlayer = _playerTwo;
            _otherDisplayedPlayer = _playerOne;
        }
        SetCommandListData(_currentlyDisplayedPlayer.PlayerStats);
    }
}
