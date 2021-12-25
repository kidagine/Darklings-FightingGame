using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionUpdater : MonoBehaviour
{
	[SerializeField] private TextAsset _versionTextAsset = default;
	[Header("Texts")]
	[SerializeField] private TextMeshProUGUI _menuVersionText = default;
	[SerializeField] private TextMeshProUGUI _patchNotesVersionText = default;
	[SerializeField] private Transform _patchNotesGroup = default;
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

		foreach (Transform patchNote in _patchNotesGroup)
		{
			_patchNotes.Add(patchNote.GetChild(1).GetComponent<TextMeshProUGUI>());
		}
		string versionText = _versionTextAsset.text;
		int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
		string versionNumber = " " + versionText.Substring(versionTextPosition, versionText.LastIndexOf(_patchNotesSplit) - versionTextPosition).Trim();
		string patchNotesWhole = versionText.Substring(versionText.IndexOf(_patchNotesSplit) + _patchNotesSplit.Length).Trim();
		string[] patchNotes = patchNotesWhole.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

		_menuVersionText.text += versionNumber;
		_patchNotesVersionText.text += versionNumber;
		for (int i = 0; i < _patchNotes.Count; i++)
		{
			if (i < patchNotes.Length)
			{
				_patchNotes[i].text = patchNotes[i].Trim();
			}
			else
			{
				_patchNotes[i].transform.parent.gameObject.SetActive(false);
			}
		}
	}
}
