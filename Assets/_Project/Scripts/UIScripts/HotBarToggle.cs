using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotBarToggle : BaseToggle
{
    [SerializeField] private TopBarMenu _topBarMenu;
    [SerializeField] private GameObject _activationBar;
    [SerializeField] private TextMeshProUGUI _explanationText;
    [SerializeField] private string _explanation;
    public static HotBarToggle PreviousSelected;

    protected override void Awake()
    {
        base.Awake();
        if (_selectOnStart)
        {
            _button.onClick?.Invoke();
            _isPressed = true;
            _animator.SetBool("IsHover", true);
            _activationBar.SetActive(true);
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        PreviousSelected = this;
        _explanationText.text = _explanation;
        base.OnSelect(eventData);
    }

    public override void Activate()
    {
        _topBarMenu.HideHotBar(gameObject);
        _baseTogglesGroup.ActiveToggle = this;
        _isPressed = true;
        _baseTogglesGroup.CheckToggles();
        _activationBar.SetActive(true);
    }

    public override void ResetToggle()
    {
        base.ResetToggle();
        _activationBar.SetActive(false);
    }


    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (_isPressed)
            return;
        _animator.SetBool("IsHover", true);
        _audio.Sound("Selected").Play();
        Cursor.SetCursor(MouseSetup.Instance.HoverCursor, Vector2.zero, CursorMode.Auto);
        _button.Select();
    }


    public override void OnPointerExit(PointerEventData eventData)
    {
        if (_isPressed)
            return;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
