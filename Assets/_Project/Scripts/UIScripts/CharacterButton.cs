using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerStatsSO _playerStatsSO = default;
    [SerializeField] private CharacterMenu _characterMenu = default;
    [SerializeField] private GameObject _backgroundImage = default;
    [SerializeField] private GameObject _firstPlayerSelector = default;
    [SerializeField] private GameObject _secondPlayerSelector = default;
    [SerializeField] private bool _isRandomizer = default;
    private Audio _audio;
    private Button _button;

    public PlayerStatsSO PlayerStatsSO { get { return _playerStatsSO; } set { } }
    public bool IsRandomizer { get { return _isRandomizer; } private set { } }

    void Start()
    {
        _audio = GetComponent<Audio>();
        _button = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _audio.Sound("Selected").Play();
        if (!_characterMenu.FirstCharacterSelected)
            _firstPlayerSelector.SetActive(true);
        else
            _secondPlayerSelector.SetActive(true);
        _characterMenu.SetCharacterImage(PlayerStatsSO, IsRandomizer);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!_characterMenu.FirstCharacterSelected && !_characterMenu.FirstCharacterLocked)
            _firstPlayerSelector.SetActive(false);
        else if (!_characterMenu.SecondCharacterLocked)
            _secondPlayerSelector.SetActive(false);
    }

    public void Click()
    {
        _audio.Sound("Pressed").Play();
        _characterMenu.SelectCharacterImage();
        _backgroundImage.SetActive(true);
    }

    private void OnDisable()
    {
        _backgroundImage.SetActive(false);
        _firstPlayerSelector.SetActive(false);
        _secondPlayerSelector.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_characterMenu.FirstCharacterSelected && _characterMenu.FirstCharacterLocked)
            return;
        if (_characterMenu.SecondCharacterLocked)
            return;
        Cursor.SetCursor(MouseSetup.Instance.HoverCursor, Vector2.zero, CursorMode.Auto);
        _button.Select();
    }

    public void OnPointerExit(PointerEventData eventData) => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
}