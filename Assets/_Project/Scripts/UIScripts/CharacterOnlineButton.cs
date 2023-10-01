using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterOnlineButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerStatsSO _playerStatsSO = default;
    [SerializeField] private OnlineSetupMenu _onlineSetupMenu = default;
    [SerializeField] private GameObject _playerSelector = default;
    [SerializeField] private GameObject _backgroundImage = default;
    private Audio _audio;
    private Button _button;

    public PlayerStatsSO PlayerStatsSO { get { return _playerStatsSO; } set { } }

    void Awake()
    {
        _audio = GetComponent<Audio>();
        _button = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _audio.Sound("Selected").Play();
        _playerSelector.SetActive(true);
    }

    public void Activate()
    {
        _onlineSetupMenu.SetCharacterImage(PlayerStatsSO);
        _backgroundImage.SetActive(true);
    }

    public void Deactivate() => _backgroundImage.SetActive(false);

    public void OnDeselect(BaseEventData eventData) => _playerSelector.SetActive(false);

    public void Click()
    {
        _audio.Sound("Pressed").Play();
        _onlineSetupMenu.SelectCharacterImage(PlayerStatsSO);
        _onlineSetupMenu.SetCharacterImage(PlayerStatsSO);
        Select();
    }

    public void Select() => _backgroundImage.SetActive(true);

    private void OnDisable() => _playerSelector.SetActive(false);

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(MouseSetup.Instance.HoverCursor, Vector2.zero, CursorMode.Auto);
        _button.Select();
    }

    public void OnPointerExit(PointerEventData eventData) => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
}
