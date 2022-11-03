using TMPro;
using UnityEngine;

public class DebugGameplayMenu : MonoBehaviour
{
	[SerializeField] private TextAsset _versionTextAsset = default;
	[SerializeField] private TextMeshProUGUI _versionText = default;
	[SerializeField] private TextMeshProUGUI _fpsText = default;
	[SerializeField] private TextMeshProUGUI _frameText = default;
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
		_fpsFrame++;
		if (_fpsFrame == 6)
		{
			_fpsFrame = 0;
		}
	}
#endif
}
