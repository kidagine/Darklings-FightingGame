using Demonics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameMeterSquare : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _frameNumberText = default;
    [SerializeField] private Image _border = default;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _border.transform.SetAsFirstSibling();
    }

    public void SetFrame(FramedataTypesEnum framedataEnum, bool isExtraSquare = false)
    {
        switch (framedataEnum)
        {
            case FramedataTypesEnum.None:
                _image.color = Color.black;
                _frameNumberText.text = "";
                break;
            case FramedataTypesEnum.StartUp:
                _image.color = Color.green;
                break;
            case FramedataTypesEnum.Active:
                _image.color = Color.red;
                break;
            case FramedataTypesEnum.Recovery:
                _image.color = Color.blue;
                break;
            case FramedataTypesEnum.Hurt:
                _image.color = Color.yellow;
                break;
            case FramedataTypesEnum.Knockdown:
                _image.color = Color.magenta;
                break;
            case FramedataTypesEnum.Block:
                _image.color = Color.cyan;
                break;
        }
        _border.gameObject.SetActive(isExtraSquare);
        _border.color = _image.color;
    }

    public void FadeFrame()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.3f);
        _frameNumberText.text = "";
    }

    public void DisplayFrameNumber(int number)
    {
        _frameNumberText.text = number.ToString();
    }
}
