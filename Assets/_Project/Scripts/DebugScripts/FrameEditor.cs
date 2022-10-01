using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FrameEditor : MonoBehaviour
{
    [SerializeField] private CharacterEditor _characterEditor = default;
    [SerializeField] private Image _frameImage = default;
    [SerializeField] private Button _frameButton = default;
    [SerializeField] private TMP_InputField _durationInputField = default;


    void Awake()
    {
        _durationInputField.onEndEdit.AddListener(UpdateFrameDuration);
        _frameButton.onClick.AddListener(ClickFrame);
    }

    public void SetDuration(int value)
    {
        _durationInputField.text = value.ToString();
    }

    public void SetImage(Color color)
    {
        _frameImage.color = color;
    }

    private void UpdateFrameDuration(string value)
    {
        int duration;
        if (!int.TryParse(value, out duration))
        {
            duration = 1;
        }
        _characterEditor.SetFrameDuration(transform.GetSiblingIndex(), duration);
    }

    private void ClickFrame()
    {
        _characterEditor.GoToFrame(transform.GetSiblingIndex());
    }
}
