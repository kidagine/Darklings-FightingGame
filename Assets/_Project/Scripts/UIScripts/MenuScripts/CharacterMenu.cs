using Demonics.UI;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class CharacterMenu : BaseMenu
{
    [SerializeField] private PlayerInput _playerInput = default;
    [SerializeField] private FadeHandler _fadeHandler = default;
    [SerializeField] private GameObject _rebindOnePrompt = default;
    [SerializeField] private GameObject _assistOne = default;
    [SerializeField] private GameObject _assistTwo = default;
    [SerializeField] private GameObject _iconsOne = default;
    [SerializeField] private GameObject _iconsTwo = default;
    [SerializeField] private PlayerUIRender _assistOneUIRenderer = default;
    [SerializeField] private PlayerUIRender _assistTwoUIRenderer = default;
    [SerializeField] private ChangeStageMenu _changeStageMenu = default;
    [SerializeField] private AnimationSO _randomAnimation = default;
    [SerializeField] private Button _firstCharacterButton = default;
    [SerializeField] private PlayerUIRender _playerUIRenderOne = default;
    [SerializeField] private PlayerUIRender _playerUIRenderTwo = default;
    [SerializeField] private TextMeshProUGUI _playerOneName = default;
    [SerializeField] private TextMeshProUGUI _playerTwoName = default;
    [SerializeField] private TextMeshProUGUI _hpTextOne = default;
    [SerializeField] private TextMeshProUGUI _arcanaTextOne = default;
    [SerializeField] private TextMeshProUGUI _speedTextOne = default;
    [SerializeField] private TextMeshProUGUI _hpTextTwo = default;
    [SerializeField] private TextMeshProUGUI _arcanaTextTwo = default;
    [SerializeField] private TextMeshProUGUI _speedTextTwo = default;
    [SerializeField] private PlayerStatsSO[] _playerStatsArray = default;
    [SerializeField] private RebindMenu[] _rebindMenues = default;
    private PlayerStatsSO _playerStats;
    private EventSystem _currentEventSystem;
    public bool FirstCharacterSelected { get; private set; }


    void Awake()
    {
        _currentEventSystem = EventSystem.current;
    }

    public void SetCharacterImage(PlayerStatsSO playerStats, bool isRandomizer)
    {
        _playerStats = playerStats;
        if (!FirstCharacterSelected)
        {
            _playerOneName.enabled = true;
            if (!isRandomizer)
            {
                _playerUIRenderOne.gameObject.SetActive(true);
                _iconsOne.gameObject.SetActive(true);
                _playerOneName.text = Regex.Replace(playerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
                _playerUIRenderOne.PlayerStats = playerStats;
                _playerUIRenderOne.SetAnimator(_playerStats._animation);
                _hpTextOne.text = $"LV{_playerStats.defenseLevel}";
                _arcanaTextOne.text = $"LV{_playerStats.arcanaLevel}";
                _speedTextOne.text = $"LV{_playerStats.speedLevel}";
            }
            else
            {
                _playerUIRenderOne.SetAnimator(_randomAnimation);
                _hpTextOne.text = "?";
                _arcanaTextOne.text = "?";
                _speedTextOne.text = "?";
                _playerOneName.text = "Random";
            }
        }
        else
        {
            UsedController();
            _playerTwoName.enabled = true;
            if (!isRandomizer)
            {
                _playerUIRenderTwo.gameObject.SetActive(true);
                _iconsTwo.gameObject.SetActive(true);
                _playerTwoName.text = Regex.Replace(playerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
                _playerUIRenderTwo.PlayerStats = playerStats;
                _playerUIRenderTwo.SetAnimator(_playerStats._animation);
                _hpTextTwo.text = $"LV{_playerStats.defenseLevel}";
                _arcanaTextTwo.text = $"LV{_playerStats.arcanaLevel}";
                _speedTextTwo.text = $"LV{_playerStats.speedLevel}";
            }
            else
            {
                _playerUIRenderOne.SetAnimator(_randomAnimation);
                _hpTextTwo.text = "?";
                _arcanaTextTwo.text = "?";
                _speedTextTwo.text = "?";
                _playerTwoName.text = "Random";
            }
        }
    }

    public void SelectCharacterImage()
    {
        _currentEventSystem.sendNavigationEvents = false;
        _playerOneName.enabled = true;
        _playerTwoName.enabled = true;
        if (!FirstCharacterSelected)
        {
            _assistOneUIRenderer.gameObject.SetActive(true);
            _assistOne.SetActive(true);
            if (_playerStats == null)
            {
                int randomPlayer = UnityEngine.Random.Range(0, _playerStatsArray.Length);
                _playerStats = _playerStatsArray[randomPlayer];
                string characterName = Regex.Replace(_playerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
                _playerOneName.text = characterName;
                _playerUIRenderOne.PlayerStats = _playerStats;
                _playerUIRenderOne.SetAnimator(_playerStats._animation);
            }
            _hpTextOne.text = $"LV{_playerStats.defenseLevel}";
            _arcanaTextOne.text = $"LV{_playerStats.arcanaLevel}";
            _speedTextOne.text = $"LV{_playerStats.speedLevel}";
            SceneSettings.PlayerOne = _playerStats.characterIndex;
        }
        else
        {
            _assistTwoUIRenderer.gameObject.SetActive(true);
            _assistTwo.SetActive(true);
            if (_playerStats == null)
            {
                int randomPlayer = UnityEngine.Random.Range(0, _playerStatsArray.Length);
                _playerStats = _playerStatsArray[randomPlayer];
                string characterName = Regex.Replace(_playerStats.characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
                _playerOneName.text = characterName;
                _playerUIRenderTwo.PlayerStats = _playerStats;
                _playerUIRenderTwo.SetAnimator(_playerStats._animation);
            }
            _hpTextTwo.text = $"LV{_playerStats.defenseLevel}";
            _arcanaTextTwo.text = $"LV{_playerStats.arcanaLevel}";
            _speedTextTwo.text = $"LV{_playerStats.speedLevel}";
            SceneSettings.PlayerTwo = _playerStats.characterIndex;
        }
    }

    public void SetCharacter(bool isPlayerOne)
    {
        if (isPlayerOne)
        {
            _playerUIRenderOne.Taunt();
        }
        else
        {
            _playerUIRenderTwo.Taunt();
        }
        if (FirstCharacterSelected)
        {
            EventSystem.current.gameObject.SetActive(false);
        }
        StartCoroutine(TauntEndCoroutine());
    }

    IEnumerator TauntEndCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        _currentEventSystem.sendNavigationEvents = true;
        if (!FirstCharacterSelected)
        {
            FirstCharacterSelected = true;
            _currentEventSystem.SetSelectedGameObject(null);
            _firstCharacterButton.Select();
        }
        else
        {
            SceneSettings.SceneSettingsDecide = true;
            if (SceneSettings.RandomStage)
            {
                SceneSettings.StageIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(StageTypeEnum)).Length - 1);
            }
            _fadeHandler.onFadeEnd.AddListener(() => SceneManager.LoadScene(2));
            _fadeHandler.StartFadeTransition(true);

        }
    }

    public void GoBack(BaseMenu otherMenu)
    {
        if (_changeStageMenu.IsOpen)
        {
            _changeStageMenu.ChangeStageClose();
        }
        else
        {
            if (!_rebindMenues[0].gameObject.activeSelf && !_rebindMenues[1].gameObject.activeSelf)
            {
                OpenMenuHideCurrent(otherMenu);
                ResetControllerInput();
            }
        }
    }

    public void ResetControllerInput()
    {
        //SceneSettings.ControllerOne = "Cpu";
        //SceneSettings.ControllerTwo = "Cpu";
    }

    public void OpenRebind()
    {
        if (UsedController())
        {
            if (!_changeStageMenu.IsOpen)
            {
                if (!FirstCharacterSelected && SceneSettings.ControllerOne != null)
                {
                    _rebindMenues[0].Show();
                }
                else if (SceneSettings.ControllerTwo != null)
                {
                    _rebindMenues[1].Show();
                }
                _currentEventSystem.sendNavigationEvents = true;
            }
        }
    }

    private bool UsedController()
    {
        InputDevice device;
        if (!FirstCharacterSelected)
        {
            device = SceneSettings.ControllerOne;
        }
        else
        {
            device = SceneSettings.ControllerTwo;
        }
        if (device == _playerInput.devices[0])
        {
            _rebindOnePrompt.SetActive(true);
            return true;
        }
        _rebindOnePrompt.SetActive(false);
        return false;
    }

    private void OnDisable()
    {
        if (!SceneSettings.SceneSettingsDecide)
        {
            _iconsOne.gameObject.SetActive(false);
            _iconsTwo.gameObject.SetActive(false);
            _currentEventSystem.SetSelectedGameObject(null);
            _currentEventSystem.sendNavigationEvents = true;
            FirstCharacterSelected = false;
            _hpTextOne.text = "";
            _arcanaTextOne.text = "";
            _speedTextOne.text = "";
            _hpTextTwo.text = "";
            _arcanaTextTwo.text = "";
            _speedTextTwo.text = "";
            _playerOneName.text = "";
            _playerTwoName.text = "";
            _assistOneUIRenderer.gameObject.SetActive(false);
            _assistTwoUIRenderer.gameObject.SetActive(false);
        }
        _fadeHandler.onFadeEnd.RemoveAllListeners();
    }
}
