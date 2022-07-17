using System;
using System.Collections;
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
	private BrainController _playerOneController;
	private BrainController _playerTwoController;
	private InputBuffer _playerOneInputBuffer;
	private InputBuffer _playerTwoInputBuffer;
	private readonly string _replayPath = "/_Project/Replays/";
	private readonly int _replaysLimit = 100;
	private string[] _replayFiles;
	private readonly string _versionSplit = "Version:";
	private readonly string _patchNotesSplit = "Patch Notes:";
	private readonly string _playerOneSplit = "Player One:";
	private readonly string _playerTwoSplit = "Player Two:";
	private readonly string _stageSplit = "Stage:";
	private readonly string _skipSplit = "Skip:";
	private readonly string _playerOneInputsSplit = "Player One Inputs:";
	private readonly string _playerTwoInputsSplit = "Player Two Inputs:";

	public string VersionNumber { get; private set; }
	public int ReplayFilesAmount { get { return _replayFiles.Length; } private set { } }
	public static ReplayManager Instance { get; private set; }



	void Awake()
	{
		_replayFiles = Directory.GetFiles(Application.dataPath + $@"{_replayPath}", "*.txt", SearchOption.AllDirectories);
		if (_isReplayMode)
		{
			ReplayCardData replayCardData = GetReplayData(_replayIndex - 1);
			SceneSettings.SceneSettingsDecide = true;
			SceneSettings.PlayerOne = replayCardData.characterOne;
			SceneSettings.ColorOne = replayCardData.colorOne;
			SceneSettings.AssistOne = replayCardData.assistOne;
			SceneSettings.PlayerTwo = replayCardData.characterTwo;
			SceneSettings.ColorTwo = replayCardData.colorTwo;
			SceneSettings.AssistTwo = replayCardData.assistTwo;
			SceneSettings.StageIndex = replayCardData.stage;
			SceneSettings.MusicName = replayCardData.musicName;
			SceneSettings.Bit1 = replayCardData.bit1;
			SceneSettings.ControllerOne = "KeyboardOne";
			SceneSettings.ControllerTwo = "KeyboardTwo";
			SceneSettings.ReplayMode = true;
		}
		CheckInstance();
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
		Setup();
		if (_isReplayMode)
		{
			LoadReplay();
		}
	}


	private void Setup()
	{
		string versionText = _versionTextAsset.text;
		int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
		VersionNumber = " " + versionText[versionTextPosition..versionText.LastIndexOf(_patchNotesSplit)].Trim();
		_playerOneInputBuffer = GameManager.Instance.PlayerOne.GetComponent<InputBuffer>();
		_playerTwoInputBuffer = GameManager.Instance.PlayerTwo.GetComponent<InputBuffer>();
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
		_playerOneController = GameManager.Instance.PlayerOne.GetComponent<BrainController>();
		_playerTwoController = GameManager.Instance.PlayerTwo.GetComponent<BrainController>();
		//_playerOneController.SetControllerToCpu();
		//_playerTwoController.SetControllerToCpu();
		ReplayCardData replayCardData = GetReplayData(_replayIndex - 1);
		StartCoroutine(LoadReplayCoroutine(replayCardData));
		StartCoroutine(LoadReplayCoroutine2(replayCardData));
	}

	IEnumerator LoadReplayCoroutine(ReplayCardData replayCardData)
	{
		yield return new WaitForSeconds(replayCardData.skip);
		GameManager.Instance.SkipIntro();
		for (int i = 0; i < replayCardData.playerOneInputs.Length; i++)
		{
			string[] playerOneInputInfo = replayCardData.playerOneInputs[i].Split(',');
			yield return new WaitForSeconds(int.Parse(playerOneInputInfo[2]));
			_playerOneInputBuffer.AddInputBufferItem((InputEnum)int.Parse(playerOneInputInfo[0]), (InputDirectionEnum)int.Parse(playerOneInputInfo[1]));
			switch ((InputDirectionEnum)int.Parse(playerOneInputInfo[1]))
			{
				case InputDirectionEnum.None:
					_playerOneController.ActiveController.InputDirection = new Vector2(0, 0);
					break;
				case InputDirectionEnum.Up:
					_playerOneController.ActiveController.InputDirection = new Vector2(0, 1);
					break;
				case InputDirectionEnum.Down:
					_playerOneController.ActiveController.InputDirection = new Vector2(0, -1);
					break;
				case InputDirectionEnum.Left:
					_playerOneController.ActiveController.InputDirection = new Vector2(-1, 0);
					break;
				case InputDirectionEnum.Right:
					_playerOneController.ActiveController.InputDirection = new Vector2(1, 0);
					break;
			}
		}
	}

	IEnumerator LoadReplayCoroutine2(ReplayCardData replayCardData)
	{
		yield return new WaitForSeconds(replayCardData.skip);
		GameManager.Instance.SkipIntro();
		for (int i = 0; i < replayCardData.playerOneInputs.Length; i++)
		{
			string[] playerTwoInputInfo = replayCardData.playerTwoInputs[i].Split(',');
			yield return new WaitForSeconds(int.Parse(playerTwoInputInfo[2]));
			_playerTwoInputBuffer.AddInputBufferItem((InputEnum)int.Parse(playerTwoInputInfo[0]), (InputDirectionEnum)int.Parse(playerTwoInputInfo[1]));
			switch ((InputDirectionEnum)int.Parse(playerTwoInputInfo[1]))
			{
				case InputDirectionEnum.None:
					_playerTwoController.ActiveController.InputDirection = new Vector2(0, 0);
					break;
				case InputDirectionEnum.Up:
					_playerTwoController.ActiveController.InputDirection = new Vector2(0, 1);
					break;
				case InputDirectionEnum.Down:
					_playerTwoController.ActiveController.InputDirection = new Vector2(0, -1);
					break;
				case InputDirectionEnum.Left:
					_playerTwoController.ActiveController.InputDirection = new Vector2(-1, 0);
					break;
				case InputDirectionEnum.Right:
					_playerTwoController.ActiveController.InputDirection = new Vector2(1, 0);
					break;
			}
		}
	}

	public ReplayCardData GetReplayData(int index)
	{
		string replayText = File.ReadAllText(_replayFiles[index]);

		int versionTextPosition = replayText.IndexOf(_versionSplit) + _versionSplit.Length;
		string versionNumber = replayText[versionTextPosition..replayText.LastIndexOf(_playerOneSplit)].Trim();

		int playerOneTextPosition = replayText.IndexOf(_playerOneSplit) + _playerOneSplit.Length;
		string playerOneTextWhole = replayText[playerOneTextPosition..replayText.LastIndexOf(_playerTwoSplit)].Trim();
		string[] playerOneInfo = playerOneTextWhole.Split(',');

		int playerTwoTextPosition = replayText.IndexOf(_playerTwoSplit) + _playerTwoSplit.Length;
		string playerTwoTextWhole = replayText[playerTwoTextPosition..replayText.LastIndexOf(_stageSplit)].Trim();
		string[] playerTwoInfo = playerTwoTextWhole.Split(',');

		int stageTextPosition = replayText.IndexOf(_stageSplit) + _stageSplit.Length;
		string stageTextWhole = replayText[stageTextPosition..replayText.LastIndexOf(_playerOneInputsSplit)].Trim();
		string[] stageInfo = stageTextWhole.Split(',');

		int playerOneInputTextPosition = replayText.IndexOf(_playerOneInputsSplit) + _playerOneInputsSplit.Length;
		string playerOneInputTextWhole = replayText[playerOneInputTextPosition..replayText.LastIndexOf(_playerTwoInputsSplit)].Trim();
		string[] playerOneInputInfo = playerOneInputTextWhole.Split('|');


		int playerTwoInputTextPosition = replayText.IndexOf(_playerTwoInputsSplit) + _playerTwoInputsSplit.Length;
		string playerTwoInputTextWhole = replayText[playerTwoInputTextPosition..replayText.LastIndexOf(_skipSplit)].Trim();
		string[] playerTwoInputInfo = playerTwoInputTextWhole.Split('|');

		string skipTextWhole = replayText[(replayText.IndexOf(_skipSplit) + _skipSplit.Length)..].Trim();

		ReplayCardData replayData = new()
		{
			versionNumber = versionNumber,
			characterOne = int.Parse(playerOneInfo[0]),
			colorOne = int.Parse(playerOneInfo[1]),
			assistOne = int.Parse(playerOneInfo[2]),
			characterTwo = int.Parse(playerTwoInfo[0]),
			colorTwo = int.Parse(playerTwoInfo[1]),
			assistTwo = int.Parse(playerTwoInfo[2]),
			stage = int.Parse(stageInfo[0]),
			musicName = stageInfo[1].Trim(),
			bit1 = bool.Parse(stageInfo[2]),
			playerOneInputs = playerOneInputInfo,
			playerTwoInputs = playerTwoInputInfo,
			skip = int.Parse(skipTextWhole)
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
