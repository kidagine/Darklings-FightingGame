using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseToggle : BaseButton
{
    [SerializeField] protected BaseTogglesGroup _baseTogglesGroup = default;
    [SerializeField] protected bool _selectOnStart = default;
    [SerializeField] private BaseMenu[] _childMenues = default;
    protected override void Awake()
    {
        base.Awake();
        _resetToDefault = false;
        if (_selectOnStart)
        {
            _button.onClick?.Invoke();
            _isPressed = true;
            _animator.SetBool("IsPress", true);
        }
    }

    public virtual void ResetToggle()
    {
        for (int i = 0; i < _childMenues.Length; i++)
            _childMenues[i].Hide();
        _isPressed = false;
        _animator.SetBool("IsHover", false);
        _animator.SetBool("IsPress", false);
    }

    public virtual void ResetHover() { }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _baseTogglesGroup.ActiveToggle = this;
        _isPressed = true;
        _baseTogglesGroup.CheckToggles();
        base.OnPointerDown(eventData);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        //_baseTogglesGroup.CheckToggles();
        _audio.Sound("Pressed").Play();
        _animator.SetBool("IsHover", true);
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        _isPressed = false;
        _animator.SetBool("IsHover", false);
        _animator.SetBool("IsPress", false);
    }

    public override void Activate()
    {
        _baseTogglesGroup.ActiveToggle = this;
        _isPressed = true;
        _baseTogglesGroup.CheckToggles();
        _audio.Sound("Pressed").Play();
        _animator.SetBool("IsHover", true);
        _animator.SetBool("IsPress", true);
        _button.onClick?.Invoke();
    }

    // public override void Deactivate()
    // {
    //     _button.enabled = false;
    //     _animator.SetBool("IsDeactivated", true);
    // }

    private void OnEnable()
    {
        if (_baseTogglesGroup.ActiveToggle == this)
        {
            _button.onClick?.Invoke();
            _isPressed = true;
            _animator.SetBool("IsPress", true);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _childMenues.Length; i++)
            _childMenues[i].Hide();
    }
}
