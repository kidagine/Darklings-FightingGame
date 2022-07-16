using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
	[SerializeField] private PauseMenu _replayPauseMenu = default;
	[SerializeField] private TextAsset _versionTextAsset = default;
	[SerializeField] private GameObject _replayPrompts = default;
	[SerializeField] private Animator _replayNotificationAnimator = default;
	[SerializeField] private InputHistory _playerOneInputHistory = default;
	[SerializeField] private InputHistory _playerTwoInputHistory = default;
	[Header("Debug")]
	[SerializeField] private bool _isReplayMode;
	[Range(1, 100)]
	[SerializeField] private int _replayIndex;
	private readonly string _replayPath = "/_Project/Replays/";
	private readonly int _replaysLimit = 100;
	private string[] _replayFiles;
	private readonly string _versionSplit = "Version:";
	private readonly string _patchNotesSplit = "Patch Notes:";
	private readonly string _playerOneSplit = "Player One:";
	private readonly string _playerTwoSplit = "Player Two:";
	private readonly string _stageSplit = "Stage:";
	private readonly string _playerOneInputsSplit = "Player One Inputs:";
	private readonly string _playerTwoInputsSplit = "Player Two Inputs:";

	public string VersionNumber { get; private set; }
	public int ReplayFilesAmount { get { return _replayFiles.Length; } private set { } }
	public static ReplayManager Instance { get; private set; }



	void Awake()
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

	void Start()
	{
		if (_isReplayMode)
		{
			LoadReplay();
		}
	}


	private void Setup()
	{
		_replayFiles = Directory.GetFiles(Application.dataPath + $@"{_replayPath}", "*.txt", SearchOption.AllDirectories);
		string versionText = _versionTextAsset.text;
		int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
		VersionNumber = " " + versionText[versionTextPosition..versionText.LastIndexOf(_patchNotesSplit)].Trim();

	}

	public void SaveReplay()
	{
		if (!SceneSettings.IsTrainingMode && _replayNotificationAnimator != null)
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
					$"Version: \n {VersionNumber}");
				fileStream.Write(version, 0, version.Length);
				byte[] playerOne = new UTF8Encoding(true).GetBytes(
					$"\n Player One: \n {SceneSettings.PlayerOne}, {SceneSettings.ColorOne}, {SceneSettings.AssistOne}");
				fileStream.Write(playerOne, 0, playerOne.Length);
				byte[] playerTwo = new UTF8Encoding(true).GetBytes(
					$"\n Player Two: \n {SceneSettings.PlayerTwo}, {SceneSettings.ColorTwo}, {SceneSettings.AssistTwo}");
				fileStream.Write(playerTwo, 0, playerTwo.Length);
				byte[] stage = new UTF8Encoding(true).GetBytes(
					$"\n Stage: \n {SceneSettings.StageIndex}, {SceneSettings.MusicName}, {SceneSettings.Bit1}");
				fileStream.Write(stage, 0, stage.Length);
				string playerOneInputsHistory = "";
				for (int i = 0; i < _playerOneInputHistory.Inputs.Count; i++)
				{
					playerOneInputsHistory += _playerOneInputHistory.Inputs[i] + ", ";
				}
				byte[] playerOneInputs = new UTF8Encoding(true).GetBytes(
					$"\n Player One Inputs: \n {playerOneInputsHistory}");
				fileStream.Write(playerOneInputs, 0, playerOneInputs.Length);
				string playerTwoInputsHistory = "";
				for (int i = 0; i < _playerTwoInputHistory.Inputs.Count; i++)
				{
					playerTwoInputsHistory += _playerTwoInputHistory.Inputs[i].ToString();
				}
				byte[] playerTwoInputs = new UTF8Encoding(true).GetBytes(
					$"\n Player Two Inputs: \n {playerTwoInputsHistory}");
				fileStream.Write(playerTwoInputs, 0, playerTwoInputs.Length);
				_replayNotificationAnimator.SetTrigger("Save");
			}
			catch (Exception e)
			{
				Debug.LogError("Error saving replay: " + e);
			}
		}
	}

	public void LoadReplay()
	{
		SceneSettings.ReplayMode = true;
		//_playerOneController = GameManager.Instance.PlayerOne.GetComponent<BrainController>();
		//_playerTwoController = GameManager.Instance.PlayerTwo.GetComponent<BrainController>();
		//_playerOneController.SetControllerToCpu();
		//_playerTwoController.SetControllerToCpu();
		//ReplayCardData replayCardData = GetReplayData(_replayIndex - 1);
		//StartCoroutine(LoadReplayCoroutine(replayCardData));
	}
	public ReplayCardData GetReplayData(int index)
	{
		string replayText = File.ReadAllText(_replayFiles[index]);

		int versionTextPosition = replayText.IndexOf(_versionSplit) + _versionSplit.Length;
		string versionNumber = " " + replayText[versionTextPosition..replayText.LastIndexOf(_playerOneSplit)].Trim();

		int playerOneTextPosition = replayText.IndexOf(_playerOneSplit) + _playerOneSplit.Length;
		string playerOneTextWhole = " " + replayText[playerOneTextPosition..replayText.LastIndexOf(_playerTwoSplit)].Trim();
		string[] playerOneInfo = playerOneTextWhole.Split(',');

		int playerTwoTextPosition = replayText.IndexOf(_playerTwoSplit) + _playerTwoSplit.Length;
		string playerTwoTextWhole = " " + replayText[playerTwoTextPosition..replayText.LastIndexOf(_stageSplit)].Trim();
		string[] playerTwoInfo = playerTwoTextWhole.Split(',');

		int stageTextPosition = replayText.IndexOf(_stageSplit) + _stageSplit.Length;
		string stageTextWhole = " " + replayText[stageTextPosition..replayText.LastIndexOf(_playerOneInputsSplit)].Trim();
		string[] stageInfo = stageTextWhole.Split(',');

		int playerOneInputTextPosition = replayText.IndexOf(_playerOneInputsSplit) + _playerOneInputsSplit.Length;
		string playerOneInputTextWhole = " " + replayText[playerOneInputTextPosition..replayText.LastIndexOf(_playerTwoInputsSplit)].Trim();
		string[] playerOneInputInfo = playerOneInputTextWhole.Split(',');

		string playerTwoInputTextWhole = " " + replayText[(replayText.IndexOf(_playerTwoInputsSplit) + _playerTwoInputsSplit.Length)..].Trim();
		string[] playerTwoInputInfo = playerTwoInputTextWhole.Split(',');

		ReplayCardData replayData = new()
		{ versionNumber = versionNumber,
			characterOne = int.Parse(playerOneInfo[0]), colorOne = int.Parse(playerOneInfo[1]), assistOne = int.Parse(playerOneInfo[2]),
			characterTwo = int.Parse(playerTwoInfo[0]), colorTwo = int.Parse(playerTwoInfo[1]), assistTwo = int.Parse(playerTwoInfo[2]),
			stage = int.Parse(stageInfo[0]), musicName = stageInfo[1].Trim(), bit1 = bool.Parse(stageInfo[2])
		};
		return replayData;
	}

	private void DeleteReplay()
	{
		File.Delete(_replayFiles[0]);
	}

	public void Pause()
	{
		Time.timeScale = 0.0f;
		GameManager.Instance.DisableAllInput();
		GameManager.Instance.PauseMusic();
		_replayPauseMenu.Show();
	}

	public void ShowReplayPrompts()
	{
		_replayPrompts.SetActive(true);
	}
}
