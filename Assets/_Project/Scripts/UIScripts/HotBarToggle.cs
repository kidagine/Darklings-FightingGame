using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotBarToggle : BaseToggle
{
    [SerializeField] private TopBarMenu _topBarMenu;
    [SerializeField] private GameObject _activationBar;
    [SerializeField] private GameObject _options;
    [SerializeField] private TextMeshProUGUI _explanationText;
    [SerializeField] private string _explanation;
    public static HotBarToggle PreviousSelected;

    protected override void Awake()
    {
        _audio = GetComponent<Audio>();
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        _rect = GetComponent<RectTransform>();
        _defaultPosition = _rect.anchoredPosition;
        _baseTogglesGroup.AddToggle(this);
        if (_selectOnStart)
        {
            _isPressed = true;
            _animator.SetBool("IsHover", true);
            _activationBar.SetActive(true);
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        PreviousSelected = this;
        _options.SetActive(true);
        _explanationText.text = _explanation;
        _baseTogglesGroup.CheckHover(this);
        base.OnSelect(eventData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        //_options.SetActive(false);
        //base.OnDeselect(eventData);
    }

    public override void Activate()
    {
        _topBarMenu.HideHotBar(gameObject);
        _baseTogglesGroup.ActiveToggle = this;
        _isPressed = true;
        _baseTogglesGroup.CheckToggles();
        _activationBar.SetActive(true);
    }

    public override void ResetHover()
    {
        _isPressed = false;
        _options.SetActive(false);
        _animator.SetBool("IsHover", false);
        _animator.SetBool("IsPress", false);
    }

    public override void ResetToggle()
    {
        base.ResetToggle();
        ResetHover();
        _activationBar.SetActive(false);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (_isPressed)
            return;
        _button.Select();
    }

    public override void OnPointerDown(PointerEventData eventData) { }

    public override void OnPointerExit(PointerEventData eventData) { }
}
