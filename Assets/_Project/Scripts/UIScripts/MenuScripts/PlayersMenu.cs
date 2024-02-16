using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayersMenu : BaseMenu
{
    [SerializeField] private HomeMenu _homeMenu = default;
    [SerializeField] private CharacterMenu _characterMenu = default;
    [SerializeField] private PlayerIcon[] _playerIcons = default;
    [SerializeField] private RectTransform[] _playerGroups = default;
    [SerializeField] private GameObject _cpuTextRight = default;
    [SerializeField] private GameObject _cpuTextLeft = default;
    [SerializeField] private PromptsInput _prompts = default;
    [SerializeField] private Selectable _firstSelectable = default;
    private Audio _audio;
    public GameObject CpuTextRight { get { return _cpuTextRight; } private set { } }
    public GameObject CpuTextLeft { get { return _cpuTextLeft; } private set { } }
    public RectTransform[] PlayerGroups { get { return _playerGroups; } set { } }


    void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    private void UpdateVisiblePlayers(InputDevice inputDevice, InputDeviceChange inputDeviceChange)
    {
        for (int i = 0; i < _playerIcons.Length; i++)
            _playerIcons[i].SetController();
    }

    public void OpenOtherMenu()
    {
        _audio.Sound("Pressed").Play();
        if (PlayerGroups[0].childCount == 0 && PlayerGroups[2].childCount == 0)
            _playerIcons[0].ConfirmQuickAssign();
        else if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            Hide();
            _homeMenu.Hide();
            _characterMenu.Show();
        }
    }

    public void OpenNextMenu(int intToCheck)
    {
        if (PlayerGroups[intToCheck].childCount == 0)
            return;
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            Hide();
            _homeMenu.Hide();
            _characterMenu.Show();
        }
    }

    public void OpenKeyboardCoOp()
    {
        _audio.Sound("Pressed").Play();
        SceneSettings.ControllerOne = _playerIcons[0].PlayerInput.devices[0];
        SceneSettings.ControllerTwo = _playerIcons[0].PlayerInput.devices[0];
        gameObject.SetActive(false);
        Hide();
        _homeMenu.Hide();
        _characterMenu.Show();
    }

    public void CheckCPU()
    {
        _cpuTextLeft.gameObject.SetActive(_playerGroups[0].childCount == 0 ? true : false);
        _cpuTextRight.gameObject.SetActive(_playerGroups[2].childCount == 0 ? true : false);
    }

    void OnDisable()
    {
        _firstSelectable.Select();
        if (_playerGroups[0].childCount > 0)
            SceneSettings.ControllerOne = _playerGroups[0].GetChild(0).GetComponent<PlayerIcon>().PlayerInput.devices[0];
        if (_playerGroups[2].childCount > 0)
            SceneSettings.ControllerTwo = _playerGroups[2].GetChild(0).GetComponent<PlayerIcon>().PlayerInput.devices[0];
        _cpuTextLeft.SetActive(true);
        _cpuTextRight.SetActive(true);
        InputSystem.onDeviceChange -= UpdateVisiblePlayers;
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        _prompts.gameObject.SetActive(true);
        InputSystem.onDeviceChange += UpdateVisiblePlayers;
        UpdateVisiblePlayers(null, default);
    }
}
