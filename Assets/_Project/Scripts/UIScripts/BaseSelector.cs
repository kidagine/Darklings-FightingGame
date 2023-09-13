using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Audio))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Animator))]
public class BaseSelector : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    [SerializeField] private TextMeshProUGUI _titleText = default;
    [SerializeField] private TextMeshProUGUI _valueText = default;
    [SerializeField] private PlayerPreferences _playerPreferences = default;
    [SerializeField] private string[] _values = default;
    [SerializeField] private Color _selectedColor = default;
    [SerializeField] private Color _deselectedColor = default;
    [SerializeField] private UnityEventInt _eventInt = default;
    [SerializeField] private RectTransform _scrollView = default;
    [SerializeField] private float _scrollUpAmount = default;
    [SerializeField] private float _scrollDownAmount = default;
    protected Audio _audio;
    protected Button _button;

    public int Value { get; private set; }


    void Awake()
    {
        _audio = GetComponent<Audio>();
        _button = GetComponent<Button>();
    }

    public void SetValue(int value)
    {
        if (_values.Length == 0)
            return;
        if (value < 0)
            value = _values.Length - 1;
        if (value > _values.Length - 1)
            value = 0;

        _audio?.Sound("Selected").Play();
        Value = value;
        _valueText.text = _values[Value];
        _playerPreferences.SavePreference(gameObject.name.Substring(0, gameObject.name.IndexOf("_")), value);
        _eventInt.Invoke(Value);
    }

    public void OnSelect(BaseEventData eventData)
    {
        _titleText.color = _selectedColor;
        _valueText.color = _selectedColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _titleText.color = _deselectedColor;
        _valueText.color = _deselectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _button.Select();
    }

    public void SelectValue(BaseEventData eventData)
    {
        AxisEventData axisEventData = eventData as AxisEventData;
        if (axisEventData.moveDir == MoveDirection.Right)
            NextValue();
        if (axisEventData.moveDir == MoveDirection.Left)
            PreviousValue();
    }

    public void MoveScrollDown(BaseEventData eventData)
    {
        AxisEventData axisEventData = eventData as AxisEventData;
        if (axisEventData.moveDir == MoveDirection.Down)
            _scrollView.anchoredPosition += new Vector2(0.0f, _scrollDownAmount);
    }

    public void MoveScrollUp(BaseEventData eventData)
    {
        AxisEventData axisEventData = eventData as AxisEventData;
        if (axisEventData.moveDir == MoveDirection.Up)
            _scrollView.anchoredPosition += new Vector2(0.0f, _scrollUpAmount);
    }

    public void SetValues(string[] values) => _values = values;

    public void NextValue() => SetValue(Value + 1);

    public void PreviousValue() => SetValue(Value - 1);
}
