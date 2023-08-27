using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotBarToggle : BaseToggle
{
    [SerializeField] private TopBarMenu _topBarMenu;
    [SerializeField] private GameObject _activationBar;
    [SerializeField] private TextMeshProUGUI _explanationText;
    [SerializeField] private string _explanation;

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
}
