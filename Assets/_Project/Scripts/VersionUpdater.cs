using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VersionUpdater : MonoBehaviour
{
    [SerializeField] private TextAsset _versionTextAsset = default;
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _menuVersionText = default;
    [SerializeField] private TextMeshProUGUI _patchNotesVersionText = default;
    [SerializeField] private Transform _patchNotesGroup = default;
    [SerializeField] private Transform _patchNotesExtraGroup = default;
    [SerializeField] private GameObject _patchNotePrefab = default;
    [SerializeField] private GameObject _patchNoteExtraPrefab = default;
    private readonly string _versionSplit = "Version:";
    private readonly string _patchNotesSplit = "Patch Notes:";
    private readonly string _descriptionSplit = "Description:";


    void Awake()
    {
        SetVersionInformation();
        //        SceneManager.LoadScene("DebugScene", LoadSceneMode.Additive);
    }

    void SetVersionInformation()
    {
        _menuVersionText.text = "Ver";
        if (_versionTextAsset == null)
        {
            Debug.LogError("No version text asset found");
            return;
        }

        string versionText = _versionTextAsset.text;
        int descriptionTextPosition = versionText.IndexOf(_descriptionSplit) + _descriptionSplit.Length;
        int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
        string descriptionWhole = " " + versionText[descriptionTextPosition..versionText.LastIndexOf(_versionSplit)].Trim();
        string versionNumber = " " + versionText[versionTextPosition..versionText.LastIndexOf(_patchNotesSplit)].Trim();
        string patchNotesWhole = versionText[(versionText.IndexOf(_patchNotesSplit) + _patchNotesSplit.Length)..].Trim();
        string[] patchNotes = patchNotesWhole.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        string[] descriptions = descriptionWhole.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

        _menuVersionText.text += versionNumber;
        _patchNotesVersionText.text = "Patch Notes " + versionNumber;
        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI patchNote = Instantiate(_patchNotePrefab, _patchNotesGroup).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            patchNote.text = patchNotes[i].Trim();
        }

        for (int i = 0; i < patchNotes.Length; i++)
        {
            GameObject patchNote = Instantiate(_patchNoteExtraPrefab, _patchNotesExtraGroup);
            patchNote.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = patchNotes[i].Trim();
            patchNote.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = descriptions[i].Trim();
        }
    }
}
