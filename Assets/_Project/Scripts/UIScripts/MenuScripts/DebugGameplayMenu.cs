using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugGameplayMenu : MonoBehaviour
{
    [SerializeField] private TextAsset _versionTextAsset = default;
    [SerializeField] private TextMeshProUGUI _versionText = default;
    [SerializeField] private TextMeshProUGUI _fpsText = default;
    [SerializeField] private TextMeshProUGUI _frameText = default;
    [SerializeField] private TextMeshProUGUI _p1PositionText = default;
    [SerializeField] private TextMeshProUGUI _p2PositionText = default;
    [SerializeField] private TextMeshProUGUI _p1ConnectionText = default;
    [SerializeField] private TextMeshProUGUI _p2ConnectionText = default;
    [SerializeField] private Slider _p1ConnectionSlider = default;
    [SerializeField] private Slider _p2ConnectionSlider = default;


    private readonly string _versionSplit = "Version:";
    private readonly string _patchNotesSplit = "Patch Notes:";
    private int _fpsFrame;
    void Awake()
    {
        gameObject.SetActive(false);
#if UNITY_EDITOR
        gameObject.SetActive(true);
        string versionText = _versionTextAsset.text;
        int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
        string versionNumber = " " + versionText[versionTextPosition..versionText.LastIndexOf(_patchNotesSplit)].Trim();

        _versionText.text = "Ver:" + versionNumber;
#endif
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (_fpsFrame == 4)
        {
            _fpsText.text = "FPS: " + Mathf.FloorToInt(1f / Time.deltaTime);
        }
        _frameText.text = "Frame: " + DemonicsWorld.Frame;
    }
    private void FixedUpdate()
    {
        if (GameplayManager.Instance.PlayerOne != null)
        {
            _p1ConnectionText.text = GameplayManager.Instance.PlayerOne.ConnectionStatus;
            _p2ConnectionText.text = GameplayManager.Instance.PlayerTwo.ConnectionStatus;
            if (string.IsNullOrEmpty(_p1ConnectionText.text))
            {
                _p1ConnectionText.transform.parent.gameObject.SetActive(false);
            }
            if (string.IsNullOrEmpty(_p2ConnectionText.text))
            {
                _p2ConnectionText.transform.parent.gameObject.SetActive(false);
            }

            string p1X = ((float)GameplayManager.Instance.PlayerTwo.OtherPlayerMovement.Physics.Position.x).ToString("0.00");
            string p1Y = ((float)GameplayManager.Instance.PlayerTwo.OtherPlayerMovement.Physics.Position.y).ToString("0.00");
            string p2X = ((float)GameplayManager.Instance.PlayerOne.OtherPlayerMovement.Physics.Position.x).ToString("0.00");
            string p2Y = ((float)GameplayManager.Instance.PlayerOne.OtherPlayerMovement.Physics.Position.y).ToString("0.00");
            _p1PositionText.text = $"P1 ({p1X}, {p1Y})";
            _p2PositionText.text = $"P2 ({p2X}, {p2Y})";

            _fpsFrame++;
            if (_fpsFrame == 6)
            {
                _fpsFrame = 0;
            }
            _p1PositionText.transform.parent.position = new Vector2(Camera.main.WorldToScreenPoint(GameplayManager.Instance.PlayerOne.transform.position).x, _p1PositionText.transform.parent.position.y);
            _p2PositionText.transform.parent.position = new Vector2(Camera.main.WorldToScreenPoint(GameplayManager.Instance.PlayerTwo.transform.position).x, _p2PositionText.transform.parent.position.y);
        }
    }
#endif
}
