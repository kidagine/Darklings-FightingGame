using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
	[SerializeField] private TextAsset _versionTextAsset = default;
	private readonly string _replayPath = "/_Project/Replays/";
	private readonly int _replaysLimit = 100;
	private string[] _replayFiles;
	private string _versionNumber;
	private readonly string _versionSplit = "Version:";
	private readonly string _patchNotesSplit = "Patch Notes:";
	private readonly string _playerOneSplit = "Player One:";
	private readonly string _playerTwoSplit = "Player Two:";
	private readonly string _stageSplit = "Stage:";

	public static ReplayManager Instance { get; private set; }
	public int ReplayFilesAmount { get { return _replayFiles.Length; } private set { } }

	void Start()
	{
		CheckInstance();
		Setup();
	}

	private void CheckInstance()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private void Setup()
	{
		_replayFiles = Directory.GetFiles(Application.dataPath + $@"{_replayPath}", "*.txt", SearchOption.AllDirectories);
		string versionText = _versionTextAsset.text;
		int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
		_versionNumber = " " + versionText[versionTextPosition..versionText.LastIndexOf(_patchNotesSplit)].Trim();

	}

	public void SaveReplay()
	{
		if (!SceneSettings.IsTrainingMode)
		{
			if (_replayFiles.Length == _replaysLimit)
			{
				DeleteReplay();
			}

			string fileName = Application.dataPath + $@"{_replayPath}{_replayFiles.Length + 1}_{GameManager.Instance.PlayerOne.PlayerStats.name}_{GameManager.Instance.PlayerTwo.PlayerStats.name}.txt";
			try
			{
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}

				using FileStream fileStream = File.Create(fileName);
				byte[] version = new UTF8Encoding(true).GetBytes(
					$"Version: \n {_versionNumber}");
				fileStream.Write(version, 0, version.Length);
				byte[] playerOne = new UTF8Encoding(true).GetBytes(
					$"\n Player One: \n {SceneSettings.PlayerOne}, {SceneSettings.ColorOne}, {SceneSettings.AssistOne}");
				fileStream.Write(playerOne, 0, playerOne.Length);
				byte[] playerTwo = new UTF8Encoding(true).GetBytes(
					$"\n Player Two: \n {SceneSettings.PlayerTwo}, {SceneSettings.ColorTwo}, {SceneSettings.AssistTwo}");
				fileStream.Write(playerTwo, 0, playerTwo.Length);
				byte[] stage = new UTF8Encoding(true).GetBytes(
					$"\n Stage: \n {SceneSettings.StageIndex}, {SceneSettings.MusicName} {SceneSettings.Bit1}");
				fileStream.Write(stage, 0, stage.Length);
			}
			catch (Exception e)
			{
				Debug.LogError("Error saving replay: " + e);
			}
		}
	}

	public void LoadReplay()
	{

	}

	public ReplayCardData GetReplayData(int index)
	{
		string replayText = File.ReadAllText(_replayFiles[index]);
		Debug.Log(replayText);
		int versionTextPosition = replayText.IndexOf(_versionSplit) + _versionSplit.Length;
		string versionWhole = " " + replayText[versionTextPosition..replayText.LastIndexOf(_playerOneSplit)].Trim();
		ReplayCardData replayData = new() { characterOne = index, characterTwo = index, versionNumber = versionWhole };
		return replayData;
	}

	private void DeleteReplay()
	{
		File.Delete(_replayFiles[0]);
	}
}