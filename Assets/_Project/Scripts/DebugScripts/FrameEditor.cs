using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FrameEditor : MonoBehaviour
{
    [SerializeField] private Image _frameImage = default;
    [SerializeField] private TMP_InputField _durationInputField = default;


    public void SetDuration(int value)
    {
        _durationInputField.text = value.ToString();
    }

    public void SetImage(Color color)
    {
        _frameImage.color = color;
    }
}
