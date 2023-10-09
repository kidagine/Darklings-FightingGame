using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DemonSlider : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _titleText = default;
    [SerializeField] private TextMeshProUGUI _valueText = default;
    [SerializeField] private Color _selectedColor = default;
    [SerializeField] private Color _deselectedColor = default;
    [SerializeField] private bool _showAsPercentage = default;
    [SerializeField] private PlayerPreferences _playerPreferences = default;
    [SerializeField] private UnityEventInt _eventInt = default;
    [SerializeField] private Slider _slider;


    private void Start() => _slider.onValueChanged.AddListener(UpdateValue);

    private void UpdateValue(float value)
    {
        int valueInt = (int)value;
        _slider.Select();
        SetValue(valueInt);
        if (_playerPreferences)
            _playerPreferences.SavePreference(gameObject.name.Substring(0, gameObject.name.IndexOf("_")), valueInt);
    }

    public void SetValue(int value)
    {
        string valueText = value.ToString();
        if (_showAsPercentage)
            valueText += "%";
        _valueText.text = valueText;
        _eventInt.Invoke(value);
        _slider.value = value;
    }

    public void SelectValue(BaseEventData eventData)
    {
        AxisEventData axisEventData = eventData as AxisEventData;
        if (axisEventData.moveDir == MoveDirection.Right)
            _slider.value++;
        if (axisEventData.moveDir == MoveDirection.Left)
            _slider.value--;
    }

    public void OnSelect(BaseEventData eventData) => _titleText.color = _selectedColor;

    public void OnDeselect(BaseEventData eventData) => _titleText.color = _deselectedColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        Cursor.SetCursor(MouseSetup.Instance.HoverCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
