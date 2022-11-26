using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FrameEditor : MonoBehaviour
{
    [SerializeField] private CharacterEditor _characterEditor = default;
    [SerializeField] private Image _frameImage = default;
    [SerializeField] private Image _frameSelectedImage = default;
    [SerializeField] private Button _frameButton = default;
    [SerializeField] private TMP_InputField _durationInputField = default;
    private static FrameEditor previousFrameEditor;


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

    public void EnableFrameSelected()
    {
        if (previousFrameEditor != null)
        {
            previousFrameEditor.DisableFrameSelected();
        }
        previousFrameEditor = this;
        _frameSelectedImage.enabled = true;
    }

    public void DisableFrameSelected()
    {
        _frameSelectedImage.enabled = false;
    }

    private void ClickFrame()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _characterEditor.DeleteFrame(transform.GetSiblingIndex());
        }
        else
        {
            _characterEditor.GoToFrame(transform.GetSiblingIndex());
        }
    }
}
