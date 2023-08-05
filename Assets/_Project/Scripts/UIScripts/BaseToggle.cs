using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseToggle : BaseButton
{
    [SerializeField] private BaseTogglesGroup _baseTogglesGroup = default;
    [SerializeField] private bool _selectOnStart = default;
    [SerializeField] private BaseMenu[] _childMenues = default;
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
        for (int i = 0; i < _childMenues.Length; i++)
            _childMenues[i].Hide();
        _isPressed = false;
        _animator.SetBool("IsHover", false);
        _animator.SetBool("IsPress", false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        _baseTogglesGroup.ActiveToggle = this;
        _isPressed = true;
        _baseTogglesGroup.CheckToggles();
        base.OnPointerDown(eventData);
    }

    private void OnEnable()
    {
        if (_baseTogglesGroup.ActiveToggle == this)
        {
            GetComponent<Button>().onClick?.Invoke();
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
