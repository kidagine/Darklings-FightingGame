using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ReplayCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _playerOneImage = default;
    [SerializeField] private Image _playerTwoImage = default;
    [SerializeField] private TextMeshProUGUI _versionText = default;
    [SerializeField] private TextMeshProUGUI _dateText = default;
    [SerializeField] private Sprite[] _characterPortraits = default;
    public ReplayData ReplayData { get; private set; }
    private BaseButton _baseButton;
    private Button _button;

    void Awake()
    {
        _baseButton = GetComponent<BaseButton>();
        _button = GetComponent<Button>();
    }

    public void SetData(ReplayData replayData)
    {
        ReplayData = replayData;
        _playerOneImage.sprite = GetCharacterPortrait(replayData.playerOneCharacter);
        _playerTwoImage.sprite = GetCharacterPortrait(replayData.playerTwoCharacter);
        _versionText.text = $"Ver {replayData.version}";
        _dateText.text = replayData.date;
    }

    public void Initialize(int index, ReplaysMenu replaysMenu, RectTransform scrollView, Selectable previous = null, bool last = false)
    {
        _button.onClick.AddListener(() => replaysMenu.LoadReplayMatch(index));
        _baseButton._scrollView = scrollView;
        if (index == 0)
        {
            _baseButton._scrollUpAmount = 0.0f;
            _button.Select();
        }
        if (last)
        {
            Navigation customNav = new();
            customNav.mode = Navigation.Mode.Explicit;
            customNav.selectOnUp = previous;
            _button.navigation = customNav;
            _baseButton._scrollDownAmount = 0.0f;
        }

    }

    private Sprite GetCharacterPortrait(int index)
    {
        return _characterPortraits[index];
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(MouseSetup.Instance.HoverCursor, Vector2.zero, CursorMode.Auto);
        _button.Select();
    }

    public void OnPointerExit(PointerEventData eventData) => Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
}

public struct ReplayCardData
{
    public string versionNumber;
    public string date;
    public int characterOne;
    public int colorOne;
    public int assistOne;
    public int characterTwo;
    public int colorTwo;
    public int assistTwo;
    public int stage;
    public string musicName;
    public bool bit1;
    public int skip;
    public ReplayInput[] playerOneInputs;
    public ReplayInput[] playerTwoInputs;
}
[Serializable]
public struct ReplayInput
{
    public InputEnum input;
    public int time;
}