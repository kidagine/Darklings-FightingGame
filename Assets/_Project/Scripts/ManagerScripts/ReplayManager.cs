using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharedGame;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReplayManager : MonoBehaviour
{
    [SerializeField] private ReplaySO _replaySO = default;
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
    private readonly string _versionSplit = "Version:";
    private readonly string _patchNotesSplit = "Patch Notes:";


    public string VersionNumber { get; private set; }
    public int Skip { get; set; }
    public int ReplayAmount { get { return _replaySO.replays.Count; } private set { } }
    public static ReplayManager Instance { get; private set; }

    void Awake()
    {
        if (!SceneSettings.SceneSettingsDecide)
        {
            SceneSettings.ReplayMode = _isReplayMode;
            SceneSettings.ReplayIndex = _replayIndex;
        }

        if (SceneSettings.ReplayMode)
        {
            SetReplay();
        }
        CheckInstance();
    }


    public void SetReplay()
    {
        ReplayData replayCardData = GetReplayData(SceneSettings.ReplayIndex);
        SceneSettings.PlayerOne = replayCardData.playerOneCharacter;
        SceneSettings.ColorOne = replayCardData.playerOneColor;
        SceneSettings.AssistOne = replayCardData.playerOneAssist;
        SceneSettings.PlayerTwo = replayCardData.playerTwoCharacter;
        SceneSettings.ColorTwo = replayCardData.playerTwoColor;
        SceneSettings.AssistTwo = replayCardData.playerTwoAssist;
        SceneSettings.StageIndex = replayCardData.stage;
        SceneSettings.MusicName = replayCardData.music;
        SceneSettings.Bit1 = replayCardData.theme;
        SceneSettings.ControllerOne = InputSystem.devices[0];
        SceneSettings.ControllerTwo = InputSystem.devices[0];
        SceneSettings.ReplayMode = true;
    }

    private void CheckInstance()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        string versionText = _versionTextAsset.text;
        int versionTextPosition = versionText.IndexOf(_versionSplit) + _versionSplit.Length;
        VersionNumber = versionText[versionTextPosition..versionText.LastIndexOf(_patchNotesSplit)].Trim();
    }

    public void StartReplay()
    {
        if (SceneSettings.ReplayMode)
            LoadReplay();
    }

    public void SaveReplay()
    {
        if (GameplayManager.Instance.PlayerOneController.ControllerInputName == "CPU" || GameplayManager.Instance.PlayerTwoController.ControllerInputName == "CPU")
            return;
        if (NetworkInput.IS_LOCAL)
        {
            if (!SceneSettings.IsTrainingMode && !SceneSettings.ReplayMode && _replayNotificationAnimator != null)
            {
                ReplayData replayData = new()
                {
                    version = VersionNumber,
                    date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                    playerOneCharacter = SceneSettings.PlayerOne,
                    playerOneAssist = SceneSettings.AssistOne,
                    playerOneColor = SceneSettings.ColorOne,
                    playerTwoCharacter = SceneSettings.PlayerTwo,
                    playerTwoAssist = SceneSettings.AssistTwo,
                    playerTwoColor = SceneSettings.ColorTwo,
                    stage = SceneSettings.StageIndex,
                    music = GameplayManager.Instance.CurrentMusic.name,
                    theme = SceneSettings.Bit1,
                    playerOneInputs = new List<ReplayInput>(),
                    playerTwoInputs = new List<ReplayInput>()
                };

                for (int i = 0; i < _playerOneInputHistory.Inputs.Count; i++)
                {
                    InputItemNetwork inputItem = _playerOneInputHistory.Inputs[i];
                    if (inputItem.time > 0)
                    {
                        ReplayInput replayInput = new()
                        {
                            input = inputItem.inputEnum,
                            time = inputItem.time
                        };
                        replayData.playerOneInputs.Add(replayInput);
                    }
                }
                replayData.playerOneInputs = replayData.playerOneInputs.OrderBy(d => d.time).ToList();
                for (int i = 0; i < _playerTwoInputHistory.Inputs.Count; i++)
                {
                    InputItemNetwork inputItem = _playerTwoInputHistory.Inputs[i];
                    if (inputItem.time > 0)
                    {
                        ReplayInput replayInput = new()
                        {
                            input = inputItem.inputEnum,
                            time = inputItem.time
                        };
                        replayData.playerTwoInputs.Add(replayInput);
                    }
                }
                replayData.playerTwoInputs = replayData.playerTwoInputs.OrderBy(d => d.time).ToList();
                replayData.skipTime = Skip;
                _replayNotificationAnimator.SetTrigger("Save");
                _replaySO.replays.Add(replayData);
            }
        }
    }

    public void LoadReplay()
    {
        SceneSettings.ReplayMode = true;
        GameplayManager.Instance.PlayerOne.GetComponent<PlayerInput>().enabled = false;
        GameplayManager.Instance.PlayerTwo.GetComponent<PlayerInput>().enabled = false;
        replayCardData = GetReplayData(SceneSettings.ReplayIndex);
        initialReplayStart = true;
    }

    private ReplayData replayCardData;
    bool runReplay;
    bool initialReplayStart;
    private int playerOneReplayIndex;
    private int playerTwoReplayIndex;

    public ReplayData GetReplayData(int index) => _replaySO.replays[index];

    public void StartLoadReplay()
    {
        replayCardData = GetReplayData(SceneSettings.ReplayIndex);
        runReplay = true;
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.CapsLock) && !SceneSettings.IsTrainingMode)
            SaveReplay();
#endif
        if (initialReplayStart)
        {
            if (DemonicsWorld.Frame == replayCardData.skipTime && replayCardData.skipTime > 0)
            {
                GameSimulation.Skip = true;
                initialReplayStart = false;
                runReplay = true;
            }
        }
        if (runReplay)
        {
            NetworkInput.ONE_NEUTRAL_INPUT = false;
            NetworkInput.ONE_UP_INPUT = false;
            NetworkInput.ONE_DOWN_INPUT = false;
            NetworkInput.ONE_LEFT_INPUT = false;
            NetworkInput.ONE_RIGHT_INPUT = false;
            NetworkInput.ONE_UP_RIGHT_INPUT = false;
            NetworkInput.ONE_UP_LEFT_INPUT = false;
            NetworkInput.ONE_DOWN_RIGHT_INPUT = false;
            NetworkInput.ONE_DOWN_LEFT_INPUT = false;

            NetworkInput.ONE_LIGHT_INPUT = false;
            NetworkInput.ONE_MEDIUM_INPUT = false;
            NetworkInput.ONE_HEAVY_INPUT = false;
            NetworkInput.ONE_SHADOW_INPUT = false;
            NetworkInput.ONE_ARCANA_INPUT = false;
            NetworkInput.ONE_DASH_FORWARD_INPUT = false;
            NetworkInput.ONE_DASH_BACKWARD_INPUT = false;
            NetworkInput.ONE_GRAB_INPUT = false;
            NetworkInput.ONE_BLUE_FRENZY_INPUT = false;
            NetworkInput.ONE_RED_FRENZY_INPUT = false;

            NetworkInput.TWO_NEUTRAL_INPUT = false;
            NetworkInput.TWO_UP_INPUT = false;
            NetworkInput.TWO_DOWN_INPUT = false;
            NetworkInput.TWO_LEFT_INPUT = false;
            NetworkInput.TWO_RIGHT_INPUT = false;
            NetworkInput.TWO_UP_RIGHT_INPUT = false;
            NetworkInput.TWO_UP_LEFT_INPUT = false;
            NetworkInput.TWO_DOWN_RIGHT_INPUT = false;
            NetworkInput.TWO_DOWN_LEFT_INPUT = false;

            NetworkInput.TWO_LIGHT_INPUT = false;
            NetworkInput.TWO_MEDIUM_INPUT = false;
            NetworkInput.TWO_HEAVY_INPUT = false;
            NetworkInput.TWO_SHADOW_INPUT = false;
            NetworkInput.TWO_ARCANA_INPUT = false;
            NetworkInput.TWO_DASH_FORWARD_INPUT = false;
            NetworkInput.TWO_DASH_BACKWARD_INPUT = false;
            NetworkInput.TWO_GRAB_INPUT = false;
            NetworkInput.TWO_BLUE_FRENZY_INPUT = false;
            NetworkInput.TWO_RED_FRENZY_INPUT = false;
            NextReplayAction();
            NextReplayAction2();
        }
    }
    private void NextReplayAction()
    {
        if (playerOneReplayIndex < replayCardData.playerOneInputs.Count)
        {
            if (DemonicsWorld.Frame >= replayCardData.playerOneInputs[playerOneReplayIndex].time)
            {
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Neutral)
                    NetworkInput.ONE_NEUTRAL_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Up)
                    NetworkInput.ONE_UP_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Down)
                    NetworkInput.ONE_DOWN_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Left)
                    NetworkInput.ONE_LEFT_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Right)
                    NetworkInput.ONE_RIGHT_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.UpRight)
                    NetworkInput.ONE_UP_RIGHT_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.UpLeft)
                    NetworkInput.ONE_UP_LEFT_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.DownRight)
                    NetworkInput.ONE_DOWN_RIGHT_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.DownLeft)
                    NetworkInput.ONE_DOWN_LEFT_INPUT = true;

                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Light)
                    NetworkInput.ONE_LIGHT_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Medium)
                    NetworkInput.ONE_MEDIUM_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Heavy)
                    NetworkInput.ONE_HEAVY_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Assist)
                    NetworkInput.ONE_SHADOW_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Special)
                    NetworkInput.ONE_ARCANA_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.ForwardDash)
                    NetworkInput.ONE_DASH_FORWARD_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.BackDash)
                    NetworkInput.ONE_DASH_BACKWARD_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Throw)
                    NetworkInput.ONE_GRAB_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.Parry)
                    NetworkInput.ONE_BLUE_FRENZY_INPUT = true;
                if (replayCardData.playerOneInputs[playerOneReplayIndex].input == InputEnum.RedFrenzy)
                    NetworkInput.ONE_RED_FRENZY_INPUT = true;
                playerOneReplayIndex++;
            }
        }
    }
    private void NextReplayAction2()
    {
        if (playerTwoReplayIndex < replayCardData.playerTwoInputs.Count)
        {
            if (DemonicsWorld.Frame >= replayCardData.playerTwoInputs[playerTwoReplayIndex].time)
            {
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Neutral)
                    NetworkInput.TWO_NEUTRAL_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Up)
                    NetworkInput.TWO_UP_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Down)
                    NetworkInput.TWO_DOWN_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Left)
                    NetworkInput.TWO_LEFT_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Right)
                    NetworkInput.TWO_RIGHT_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.UpRight)
                    NetworkInput.TWO_UP_RIGHT_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.UpLeft)
                    NetworkInput.TWO_UP_LEFT_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.DownRight)
                    NetworkInput.TWO_DOWN_RIGHT_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.DownLeft)
                    NetworkInput.TWO_DOWN_LEFT_INPUT = true;

                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Light)
                    NetworkInput.TWO_LIGHT_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Medium)
                    NetworkInput.TWO_MEDIUM_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Heavy)
                    NetworkInput.TWO_HEAVY_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Assist)
                    NetworkInput.TWO_SHADOW_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Special)
                    NetworkInput.TWO_ARCANA_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.ForwardDash)
                    NetworkInput.TWO_DASH_FORWARD_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.BackDash)
                    NetworkInput.TWO_DASH_BACKWARD_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Throw)
                    NetworkInput.TWO_GRAB_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.Parry)
                    NetworkInput.TWO_BLUE_FRENZY_INPUT = true;
                if (replayCardData.playerTwoInputs[playerTwoReplayIndex].input == InputEnum.RedFrenzy)
                    NetworkInput.TWO_RED_FRENZY_INPUT = true;
                playerTwoReplayIndex++;
            }
        }
    }

    private void DeleteReplay()
    {
        if (_replaySO.replays.Count > 0)
            _replaySO.replays.Remove(_replaySO.replays[0]);
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
}

