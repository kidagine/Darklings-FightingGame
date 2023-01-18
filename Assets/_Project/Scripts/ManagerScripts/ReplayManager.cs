using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReplayManager : MonoBehaviour
{
    [SerializeField] private PauseMenu _replayPauseMenu = default;
    [SerializeField] private TextAsset _versionTextAsset = default;
    [SerializeField] private GameObject _replayPrompts = default;
    [SerializeField] private GameObject _replayInput = default;
    [SerializeField] private Animator _replayNotificationAnimator = default;
    [SerializeField] private InputHistory _playerOneInputHistory = default;
    [SerializeField] private InputHistory _playerTwoInputHistory = default;
    [Header("Debug")]
    [SerializeField] private bool _isReplayMode;
    [Range(0, 99)]
    [SerializeField] private int _replayIndex;
    private BrainController _playerOneController;
    private BrainController _playerTwoController;
    private InputBuffer _playerOneInputBuffer;
    private InputBuffer _playerTwoInputBuffer;
    private readonly int _replaysLimit = 100;
    private string[] _replays;
    private readonly string _versionSplit = "Version:";
    private readonly string _dateSplit = "Date:";
    private readonly string _patchNotesSplit = "Patch Notes:";
    private readonly string _playerOneSplit = "Player One:";
    private readonly string _playerTwoSplit = "Player Two:";
    private readonly string _stageSplit = "Stage:";
    private readonly string _skipSplit = "Skip:";
    private readonly string _playerOneInputsSplit = "Player One Inputs:";
    private readonly string _playerTwoInputsSplit = "Player Two Inputs:";

    public string VersionNumber { get; private set; }
    public int Skip { get; set; }
    public int ReplayAmount { get { return _replays.Length; } private set { } }
    public static ReplayManager Instance { get; private set; }

    void Awake()
    {
        if (!SceneSettings.SceneSettingsDecide)
        {
            SceneSettings.ReplayMode = _isReplayMode;
            SceneSettings.ReplayIndex = _replayIndex;
        }
        _replays = DemonicsSaver.Load("replay", "").Split('@');

        if (SceneSettings.ReplayMode)
        {
            SetReplay();
        }
        CheckInstance();
    }

    public void SetReplay()
    {
        ReplayCardData replayCardData = GetReplayData(SceneSettings.ReplayIndex);
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
        SceneSettings.ControllerOne = InputSystem.devices[0];
        SceneSettings.ControllerTwo = InputSystem.devices[0];
        SceneSettings.ReplayMode = true;
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
        string versionText = _versionTextAsset.text;
        int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
        VersionNumber = versionText[versionTextPosition..versionText.LastIndexOf(_patchNotesSplit)].Trim();
        if (SceneSettings.ReplayMode)
        {
            LoadReplay();
        }
    }

    public void Setup()
    {
        if (GameplayManager.Instance != null)
        {
            _playerOneInputBuffer = GameplayManager.Instance.PlayerOne.GetComponent<InputBuffer>();
            _playerTwoInputBuffer = GameplayManager.Instance.PlayerTwo.GetComponent<InputBuffer>();
        }
    }

    public void SaveReplay()
    {
        if (!SceneSettings.IsTrainingMode && !SceneSettings.ReplayMode && _replayNotificationAnimator != null)
        {
            string replayName = "replay";
            string replayData = "";
            replayData += $"Version:\n{VersionNumber}";
            replayData += $"\nDate:\n{DateTime.Now.ToString("MM/dd/yyyy HH:mm")}";
            replayData += $"\nPlayer One:\n{SceneSettings.PlayerOne}, {SceneSettings.ColorOne}, {SceneSettings.AssistOne}";
            replayData += $"\nPlayer Two:\n{SceneSettings.PlayerTwo}, {SceneSettings.ColorTwo}, {SceneSettings.AssistTwo}";
            replayData += $"\nStage:\n{SceneSettings.StageIndex}, {GameplayManager.Instance.CurrentMusic.name}, {SceneSettings.Bit1}";
            string playerOneInputsHistory = "";
            for (int i = 0; i < _playerOneInputHistory.Inputs.Count; i++)
            {
                playerOneInputsHistory += $"{_playerOneInputHistory.Inputs[i]},{_playerOneInputHistory.Directions[i]},{_playerOneInputHistory.InputTimes[i]}";
                if (i != _playerOneInputHistory.Inputs.Count - 1)
                {
                    playerOneInputsHistory += "|";
                }
            }
            replayData += $"\nPlayer One Inputs:\n{playerOneInputsHistory}";
            string playerTwoInputsHistory = "";
            for (int i = 0; i < _playerTwoInputHistory.Inputs.Count; i++)
            {
                playerTwoInputsHistory += $"{_playerTwoInputHistory.Inputs[i]},{_playerTwoInputHistory.Directions[i]},{_playerTwoInputHistory.InputTimes[i]}";
                if (i != _playerTwoInputHistory.Inputs.Count - 1)
                {
                    playerTwoInputsHistory += "|";
                }
            }
            replayData += $"\nPlayer Two Inputs:\n{playerTwoInputsHistory}";
            replayData += $"\nSkip:\n{Skip}";
            replayData += $"\n@";
            _replayNotificationAnimator.SetTrigger("Save");
            DemonicsSaver.Save(replayName, replayData, true);
        }
    }

    public void LoadReplay()
    {
        SceneSettings.ReplayMode = true;
        _playerOneController = GameplayManager.Instance.PlayerOne.GetComponent<BrainController>();
        _playerTwoController = GameplayManager.Instance.PlayerTwo.GetComponent<BrainController>();
        GameplayManager.Instance.PlayerOne.GetComponent<PlayerInput>().enabled = false;
        GameplayManager.Instance.PlayerTwo.GetComponent<PlayerInput>().enabled = false;
        replayCardData = GetReplayData(SceneSettings.ReplayIndex);
        initialReplayStart = true;
    }

    private ReplayCardData replayCardData;
    bool runReplay;
    bool initialReplayStart;
    private int i;
    private int j;

    public void StartLoadReplay()
    {
        replayCardData = GetReplayData(SceneSettings.ReplayIndex);
        runReplay = true;
    }
    private void FixedUpdate()
    {
        if (initialReplayStart)
        {
            if (DemonicsWorld.Frame == replayCardData.skip && replayCardData.skip > 0)
            {
                GameplayManager.Instance.SkipIntro();
                initialReplayStart = false;
                runReplay = true;
            }
        }
        if (runReplay)
        {
            NextReplayAction();
            NextReplayAction2();
        }
    }
    private void NextReplayAction()
    {
        if (i < replayCardData.playerOneInputs.Length)
        {
            if (DemonicsWorld.Frame >= replayCardData.playerOneInputs[i].time)
            {
                NetworkInput.ONE_LIGHT_INPUT = true;
                i++;
                NextReplayAction();
            }
        }
    }
    private void NextReplayAction2()
    {
        if (j < replayCardData.playerTwoInputs.Length)
        {
            if (DemonicsWorld.Frame >= replayCardData.playerTwoInputs[j].time)
            {
                _playerTwoInputBuffer.AddInputBufferItem(replayCardData.playerTwoInputs[j].input, replayCardData.playerTwoInputs[j].direction);
                j++;
                NextReplayAction2();
            }
        }
    }

    public ReplayCardData GetReplayData(int index)
    {
        string replayText = _replays[index];

        int versionTextPosition = replayText.IndexOf(_versionSplit) + _versionSplit.Length;
        string versionNumber = replayText[versionTextPosition..replayText.LastIndexOf(_dateSplit)].Trim();

        int dateTextPosition = replayText.IndexOf(_dateSplit) + _dateSplit.Length;
        string dateNumber = replayText[dateTextPosition..replayText.LastIndexOf(_playerOneSplit)].Trim();

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

        List<ReplayInput> replayOneInputs = new();
        if (playerOneInputInfo[0] != "")
        {
            for (int i = 0; i < playerOneInputInfo.Length; i++)
            {
                string[] playerInput = playerOneInputInfo[i].Split(',');
                replayOneInputs.Add(new ReplayInput() { input = Enum.Parse<InputEnum>(playerInput[0]), direction = Enum.Parse<InputDirectionEnum>(playerInput[1]), time = int.Parse(playerInput[2]) });
            }
        }
        List<ReplayInput> replayTwoInputs = new();
        if (playerTwoInputInfo[0] != "")
        {
            for (int i = 0; i < playerTwoInputInfo.Length; i++)
            {
                string[] playerInput = playerTwoInputInfo[i].Split(',');
                replayTwoInputs.Add(new ReplayInput() { input = Enum.Parse<InputEnum>(playerInput[0]), direction = Enum.Parse<InputDirectionEnum>(playerInput[1]), time = int.Parse(playerInput[2]) });
            }

        }

        ReplayCardData replayData = new()
        {
            versionNumber = versionNumber,
            date = dateNumber,
            characterOne = int.Parse(playerOneInfo[0]),
            colorOne = int.Parse(playerOneInfo[1]),
            assistOne = int.Parse(playerOneInfo[2]),
            characterTwo = int.Parse(playerTwoInfo[0]),
            colorTwo = int.Parse(playerTwoInfo[1]),
            assistTwo = int.Parse(playerTwoInfo[2]),
            stage = int.Parse(stageInfo[0]),
            musicName = stageInfo[1].Trim(),
            bit1 = bool.Parse(stageInfo[2]),
            playerOneInputs = replayOneInputs.ToArray(),
            playerTwoInputs = replayTwoInputs.ToArray(),
            skip = float.Parse(skipTextWhole)
        };
        return replayData;
    }

    private void DeleteReplay()
    {
        File.Delete(_replays[0]);
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        GameplayManager.Instance.DisableAllInput();
        GameplayManager.Instance.PauseMusic();
        _replayPauseMenu.Show();
    }

    public void ShowReplayPrompts()
    {
        _replayInput.SetActive(true);
        _replayPrompts.SetActive(true);
    }

    public void ToggleReplayInputHistory()
    {
        GameObject playerOneInputHistory = _playerOneInputHistory.transform.GetChild(0).gameObject;
        GameObject playerTwoInputHistory = _playerTwoInputHistory.transform.GetChild(0).gameObject;

        if (playerOneInputHistory.activeSelf)
        {
            playerOneInputHistory.SetActive(false);
            playerTwoInputHistory.SetActive(false);
        }
        else
        {
            playerOneInputHistory.SetActive(true);
            playerTwoInputHistory.SetActive(true);
        }
    }


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock) && !SceneSettings.IsTrainingMode)
        {
            SaveReplay();
        }
    }
#endif
}

