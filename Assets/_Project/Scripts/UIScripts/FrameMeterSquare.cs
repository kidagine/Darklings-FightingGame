using Demonics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameMeterSquare : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _frameNumberText = default;
    [SerializeField] private Image _border = default;
    [Header("Notification Colors")]
    [SerializeField] private Color _noneColor = Color.white;
    [SerializeField] private Color _startUpColor = Color.white;
    [SerializeField] private Color _activeColor = Color.white;
    [SerializeField] private Color _recoveryColor = Color.white;
    [SerializeField] private Color _hurtColor = Color.white;
    [SerializeField] private Color _knockdownColor = Color.white;
    [SerializeField] private Color _blockColor = Color.white;
    [SerializeField] private Color _parryColor = Color.white;
    [SerializeField] private Color _emptyColor = Color.white;
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
                _image.color = _noneColor;
                break;
            case FramedataTypesEnum.StartUp:
                _image.color = _startUpColor;
                break;
            case FramedataTypesEnum.Active:
                _image.color = _activeColor;
                break;
            case FramedataTypesEnum.Recovery:
                _image.color = _recoveryColor;
                break;
            case FramedataTypesEnum.Hurt:
                _image.color = _hurtColor;
                break;
            case FramedataTypesEnum.Knockdown:
                _image.color = _knockdownColor;
                break;
            case FramedataTypesEnum.Block:
                _image.color = _blockColor;
                break;
            case FramedataTypesEnum.Parry:
                _image.color = _parryColor;
                break;
            case FramedataTypesEnum.Empty:
                _image.color = _emptyColor;
                break;
        }
        _frameNumberText.text = "";
        _border.gameObject.SetActive(isExtraSquare);
        _border.color = _image.color;
    }

    public void FadeFrame()
    {
        _border.gameObject.SetActive(false);
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.3f);
    }

    public void DisplayFrameNumber(int number)
    {
        _frameNumberText.text = number.ToString();
    }
}
