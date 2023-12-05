using System.Text.RegularExpressions;
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
    [SerializeField] private RectTransform _normalsContent = default;
    [SerializeField] private RectTransform _commonContent = default;
    [SerializeField] private GameObject _demonLimitPage = default;
    [SerializeField] private GameObject _characterMovesPage = default;
    [SerializeField] private GameObject _commonMovesPage = default;
    [SerializeField] private Button _characterMovesButton = default;
    [SerializeField] private Button _normalMovesButton = default;
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
    private Player _playerOne;
    private Player _playerTwo;
    private Player _currentlyDisplayedPlayer;
    private readonly string _baseUrl = "https://kidagine.github.io/Darklings-CommandListVideos/";

    public PauseMenu CurrentPauseMenu { get; private set; }

    void Awake()
    {
        _playerOne = GameplayManager.Instance.PlayerOne;
        _playerTwo = GameplayManager.Instance.PlayerTwo;
        string playerOne = Regex.Replace(_playerOne.PlayerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
        string playerTwo = Regex.Replace(_playerTwo.PlayerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
        _characterText[0].text = playerOne;
        _characterText[1].text = playerTwo;
    }

    public void NextSubPage()
    {
        if (_characterMovesPage.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            if (_subSlides[0].activeSelf)
            {
                _subSlides[0].SetActive(false);
                _subSlides[1].SetActive(true);
                _characterContent.anchoredPosition = Vector2.zero;
                _characterMovesButton.Select();
            }
            else
            {
                _subSlides[0].SetActive(true);
                _subSlides[1].SetActive(false);
                _normalsContent.anchoredPosition = Vector2.zero;
                _normalMovesButton.Select();
            }
        }
    }

    public void SetCharacterMenu(int index)
    {
        if (_playerOne == null)
            return;
        if (index == 0)
            SetCommandListData(_playerOne.playerStats);
        else
            SetCommandListData(_playerTwo.playerStats);
    }

    public void SetInfoMenu(int index)
    {
        for (int i = 0; i < _subSlides.Length; i++)
            _subSlides[i].SetActive(false);
        _subSlides[index].SetActive(true);
        _currentPage = index;
        if (index == 0)
            _startingOption.Select();
        else if (index == 1)
            _normalMovesButton.Select();
        else if (index == 2)
            _commonMovesButton.Select();
        _normalsContent.anchoredPosition = Vector2.zero;
        _characterContent.anchoredPosition = Vector2.zero;
    }

    private bool AreBothCharactersSame()
    {
        if (_playerOne.playerStats.characterIndex == _playerTwo.playerStats.characterIndex)
            return true;
        return false;
    }

    private void SetPageInfo()
    {
        _demonLimitPage.SetActive(false);
        for (int i = 0; i < _slides.Length; i++)
            _slides[i].SetActive(false);

        _characterContent.anchoredPosition = Vector2.zero;
        _characterMovesPage.SetActive(true);
        _commonMovesPage.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        if (_currentPage == 0)
            _startingOption.Select();
        else if (_currentPage == 1)
            _normalMovesButton.Select();
        else if (_currentPage == 2)
            _commonMovesButton.Select();
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
            _commandListButtons[2].gameObject.SetActive(false);
        _commandListButtons[3].SetData(playerStats.m5L);
        _commandListButtons[4].SetData(playerStats.m2L);
        _commandListButtons[5].SetData(playerStats.jL);
        _commandListButtons[6].SetData(playerStats.m5M);
        _commandListButtons[7].SetData(playerStats.m2M);
        _commandListButtons[8].SetData(playerStats.jM);
        _commandListButtons[9].SetData(playerStats.m5H);
        _commandListButtons[10].SetData(playerStats.m2H);
        _commandListButtons[11].SetData(playerStats.jH);
        _commandListButtons[12].SetData(playerStats.mThrow);
        _commandListButtons[13].SetData(playerStats.mRedFrenzy);
        _commandListButtons[14].SetData(playerStats.mParry);
        _commandListButtons[15].SetData(playerStats.jL);
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

        _reversalImage.SetActive(false);
        _knockdownImage.SetActive(false);
        _projectileImage.SetActive(false);
        if (command.reversal)
            _reversalImage.SetActive(true);
        if (command.causesKnockdown)
            _knockdownImage.SetActive(true);
        if (command.isProjectile)
            _projectileImage.SetActive(true);
        _commandFramedata.SetFramedata(command);
        _toggleFramedataPrompt.SetActive(true);
        if (!_toggleFramedata)
        {
            _videoMenu.SetActive(true);
            _framedataMenu.SetActive(false);
        }
        _showcaseVideo.Stop();
        _showcaseVideo.Play();
    }

    public void SetCommandListShowcase(AttackSO command)
    {
        _demonLimitPage.SetActive(false);
        _videoMenu.SetActive(false);
        _framedataMenu.SetActive(true);
        _commandFramedata.SetFramedata(command);
        _toggleFramedataPrompt.SetActive(false);
    }

    public void SetDemonLimitShowcase()
    {
        _demonLimitPage.SetActive(true);
        _videoMenu.SetActive(false);
        _framedataMenu.SetActive(false);
    }
    bool _toggleFramedata;
    public void ToggleFramedata()
    {
        if (_toggleFramedataPrompt.activeSelf)
        {
            if (!_toggleFramedata)
            {
                _demonLimitPage.SetActive(false);
                _videoMenu.SetActive(false);
                _framedataMenu.SetActive(true);
            }
            else
            {
                _demonLimitPage.SetActive(false);
                _videoMenu.SetActive(true);
                _framedataMenu.SetActive(false);
                _showcaseVideo.Stop();
                _showcaseVideo.Play();
            }
            _toggleFramedata = !_toggleFramedata;
        }
    }

    public void Back()
    {
        CurrentPauseMenu.Show();
        Hide();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _characterContent.anchoredPosition = Vector2.zero;
        _commonContent.anchoredPosition = Vector2.zero;
        if (GameplayManager.Instance.IsTrainingMode)
            CurrentPauseMenu = _pauseTrainingMenu;
        else
            CurrentPauseMenu = _pauseMenu;
        if (AreBothCharactersSame())
            _slides[1].transform.parent.gameObject.SetActive(false);
        if (CurrentPauseMenu.PlayerOnePaused)
            _currentlyDisplayedPlayer = _playerOne;
        else
            _currentlyDisplayedPlayer = _playerTwo;
        SetCommandListData(_currentlyDisplayedPlayer.PlayerStats);
    }
}
