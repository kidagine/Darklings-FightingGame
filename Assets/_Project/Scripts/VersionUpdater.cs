using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionUpdater : MonoBehaviour
{
	[SerializeField] private TextAsset _versionTextAsset = default;
	[Header("Texts")]
	[SerializeField] private TextMeshProUGUI _menuVersionText = default;
	[SerializeField] private Transform _patchNotesGroup = default;
	[SerializeField] private GameObject _patchNotePrefab = default;
	private readonly List<TextMeshProUGUI> _patchNotes = new List<TextMeshProUGUI>();
	private readonly string _versionSplit = "Version:";
	private readonly string _patchNotesSplit = "Patch Notes:";


	void Awake()
	{
		SetVersionInformation();
	}

	void SetVersionInformation()
	{
		if (_versionTextAsset == null)
		{
			Debug.LogError("No version text asset found");
			return;
		}

		string versionText = _versionTextAsset.text;
		int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
		string versionNumber = " " + versionText.Substring(versionTextPosition, versionText.LastIndexOf(_patchNotesSplit) - versionTextPosition).Trim();
		string patchNotesWhole = versionText.Substring(versionText.IndexOf(_patchNotesSplit) + _patchNotesSplit.Length).Trim();
		string[] patchNotes = patchNotesWhole.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

		_menuVersionText.text += versionNumber;
		for (int i = 0; i < patchNotes.Length; i++)
		{
			TextMeshProUGUI patchNote = Instantiate(_patchNotePrefab, _patchNotesGroup).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
			patchNote.text = patchNotes[i].Trim();
		}
	}
}
