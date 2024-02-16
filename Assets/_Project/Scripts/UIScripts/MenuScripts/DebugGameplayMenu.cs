using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugGameplayMenu : MonoBehaviour
{
    [SerializeField] private TextAsset _versionTextAsset = default;
    [SerializeField] private TextMeshProUGUI _fpsText = default;
    [SerializeField] private TextMeshProUGUI _frameText = default;
    [SerializeField] private TextMeshProUGUI _p1PositionText = default;
    [SerializeField] private TextMeshProUGUI _p2PositionText = default;
    [SerializeField] private TextMeshProUGUI _p1ConnectionText = default;
    [SerializeField] private TextMeshProUGUI _p2ConnectionText = default;
    [SerializeField] private Slider _p1ConnectionSlider = default;
    [SerializeField] private Slider _p2ConnectionSlider = default;
    [SerializeField] private Canvas _debugLocalCanvas = default;
    [SerializeField] private Canvas _debugOnlineCanvas = default;
    private readonly string _versionSplit = "Version:";
    private readonly string _patchNotesSplit = "Patch Notes:";
    private int _fpsFrame;
    void Awake()
    {
        transform.parent.gameObject.SetActive(false);
#if UNITY_EDITOR
        transform.parent.gameObject.SetActive(true);
        string versionText = _versionTextAsset.text;
        int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
        string versionNumber = " " + versionText[versionTextPosition..versionText.LastIndexOf(_patchNotesSplit)].Trim();
#endif
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _debugLocalCanvas.enabled = !_debugLocalCanvas.enabled;
            _debugOnlineCanvas.enabled = !_debugOnlineCanvas.enabled;
        }
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
            _p1ConnectionSlider.value = GameplayManager.Instance.PlayerOne.ConnectionProgress;
            _p2ConnectionSlider.value = GameplayManager.Instance.PlayerTwo.ConnectionProgress;
            _p1ConnectionText.text = GameSimulation._players[0].health.ToString();
            _p2ConnectionText.text = GameSimulation._players[1].health.ToString();

            string p1X = (GameplayManager.Instance.PlayerTwo.OtherPlayerMovement.Physics.Position.x).ToString();
            string p1Y = (GameplayManager.Instance.PlayerTwo.OtherPlayerMovement.Physics.Position.y).ToString();
            string p2X = (GameplayManager.Instance.PlayerOne.OtherPlayerMovement.Physics.Position.x).ToString();
            string p2Y = (GameplayManager.Instance.PlayerOne.OtherPlayerMovement.Physics.Position.y).ToString();
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
