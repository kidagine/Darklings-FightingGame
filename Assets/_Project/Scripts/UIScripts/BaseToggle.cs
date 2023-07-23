using UnityEngine;
using UnityEngine.EventSystems;

public class BaseToggle : BaseButton
{
    [SerializeField] private BaseTogglesGroup _baseTogglesGroup = default;
    [SerializeField] private bool _selectOnStart;

    protected override void Awake()
    {
        base.Awake();
        _baseTogglesGroup.AddToggle(this);
        _resetToDefault = false;
        if (_selectOnStart)
        {
            _isPressed = true;
            _animator.SetBool("IsPress", true);
        }
    }

    public void ResetToggle()
    {
        _isPressed = false;
        _animator.SetBool("IsHover", false);
        _animator.SetBool("IsPress", false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        _baseTogglesGroup.CheckToggles();
        base.OnPointerDown(eventData);
    }
}
