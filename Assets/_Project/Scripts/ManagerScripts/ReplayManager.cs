using System;
using System.Collections.Generic;
using System.IO;
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
        if (NetworkInput.IS_LOCAL)
        {
            if (!SceneSettings.IsTrainingMode && !SceneSettings.ReplayMode && _replayNotificationAnimator != null)
            {

                ReplayData replayData = new();
                replayData.version = VersionNumber;
                replayData.date = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
                replayData.playerOneCharacter = SceneSettings.PlayerOne;
                replayData.playerOneAssist = SceneSettings.ColorOne;
                replayData.playerOneColor = SceneSettings.AssistOne;
                replayData.playerTwoCharacter = SceneSettings.PlayerTwo;
                replayData.playerTwoAssist = SceneSettings.ColorTwo;
                replayData.playerTwoColor = SceneSettings.AssistTwo;
                replayData.stage = SceneSettings.StageIndex;
                replayData.music = GameplayManager.Instance.CurrentMusic.name;
                replayData.theme = SceneSettings.Bit1;
                Debug.Log(_playerOneInputHistory.Inputs.Count);
                for (int i = 0; i < _playerOneInputHistory.Inputs.Count; i++)
                {
                    ReplayInput replayInput = new();
                    replayInput.input = _playerOneInputHistory.Inputs[i];
                    replayInput.direction = _playerOneInputHistory.Directions[i];
                    replayInput.time = _playerOneInputHistory.InputTimes[i];
                    replayData.playerOneInputs.Add(replayInput);
                }
                for (int i = 0; i < _playerTwoInputHistory.Inputs.Count; i++)
                {
                    ReplayInput replayInput = new();
                    replayInput.input = _playerOneInputHistory.Inputs[i];
                    replayInput.direction = _playerOneInputHistory.Directions[i];
                    replayInput.time = _playerOneInputHistory.InputTimes[i];
                    replayData.playerTwoInputs.Add(replayInput);
                }
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
    private int i;
    private int j;

    public ReplayData GetReplayData(int index) => _replaySO.replays[index];

    public void StartLoadReplay()
    {
        replayCardData = GetReplayData(SceneSettings.ReplayIndex);
        runReplay = true;
    }
    private void FixedUpdate()
    {
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
            NextReplayAction();
            NextReplayAction2();
        }
    }
    private void NextReplayAction()
    {
        if (i < replayCardData.playerOneInputs.Count)
        {
            Debug.Log(replayCardData.playerOneInputs[i].time);
            if (DemonicsWorld.Frame >= replayCardData.playerOneInputs[i].time)
            {
                if (replayCardData.playerOneInputs[i].input == InputEnum.Direction)
                {
                    if (replayCardData.playerOneInputs[i].direction == InputDirectionEnum.Up)
                        NetworkInput.ONE_UP_INPUT = true;
                    if (replayCardData.playerOneInputs[i].direction == InputDirectionEnum.Down)
                        NetworkInput.ONE_DOWN_INPUT = true;
                    if (replayCardData.playerOneInputs[i].direction == InputDirectionEnum.Left)
                        NetworkInput.ONE_LEFT_INPUT = true;
                    if (replayCardData.playerOneInputs[i].direction == InputDirectionEnum.Right)
                        NetworkInput.ONE_RIGHT_INPUT = true;
                    if (replayCardData.playerOneInputs[i].direction == InputDirectionEnum.Neutral)
                    {
                        NetworkInput.ONE_LEFT_INPUT = false;
                        NetworkInput.ONE_RIGHT_INPUT = false;
                    }
                }
                if (replayCardData.playerOneInputs[i].input == InputEnum.Light)
                    NetworkInput.ONE_LIGHT_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.Medium)
                    NetworkInput.ONE_MEDIUM_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.Heavy)
                    NetworkInput.ONE_HEAVY_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.Assist)
                    NetworkInput.ONE_SHADOW_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.Special)
                    NetworkInput.ONE_ARCANA_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.ForwardDash)
                    NetworkInput.ONE_DASH_FORWARD_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.BackDash)
                    NetworkInput.ONE_DASH_BACKWARD_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.Throw)
                    NetworkInput.ONE_GRAB_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.Parry)
                    NetworkInput.ONE_BLUE_FRENZY_INPUT = true;
                if (replayCardData.playerOneInputs[i].input == InputEnum.RedFrenzy)
                    NetworkInput.ONE_RED_FRENZY_INPUT = true;
                i++;
                NextReplayAction();
            }
        }
    }
    private void NextReplayAction2()
    {
        // if (j < replayCardData.playerTwoInputs.Count)
        // {
        //     if (DemonicsWorld.Frame >= replayCardData.playerTwoInputs[j].time)
        //     {
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.Direction)
        //         {
        //             if (replayCardData.playerTwoInputs[i].direction == InputDirectionEnum.Up)
        //             {
        //                 NetworkInput.TWO_UP_INPUT = true;
        //             }
        //             if (replayCardData.playerTwoInputs[i].direction == InputDirectionEnum.Down)
        //             {
        //                 NetworkInput.TWO_DOWN_INPUT = true;
        //             }
        //             if (replayCardData.playerTwoInputs[i].direction == InputDirectionEnum.Left)
        //             {
        //                 NetworkInput.TWO_LEFT_INPUT = true;
        //             }
        //             if (replayCardData.playerTwoInputs[i].direction == InputDirectionEnum.Right)
        //             {
        //                 NetworkInput.TWO_RIGHT_INPUT = true;
        //             }
        //             if (replayCardData.playerTwoInputs[i].direction == InputDirectionEnum.Neutral)
        //             {
        //                 NetworkInput.TWO_LEFT_INPUT = false;
        //                 NetworkInput.TWO_RIGHT_INPUT = false;
        //             }
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.Light)
        //         {
        //             NetworkInput.TWO_LIGHT_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.Medium)
        //         {
        //             NetworkInput.TWO_MEDIUM_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.Heavy)
        //         {
        //             NetworkInput.TWO_HEAVY_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.Assist)
        //         {
        //             NetworkInput.TWO_SHADOW_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.Special)
        //         {
        //             NetworkInput.TWO_ARCANA_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.ForwardDash)
        //         {
        //             NetworkInput.TWO_DASH_FORWARD_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.BackDash)
        //         {
        //             NetworkInput.TWO_DASH_BACKWARD_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.Throw)
        //         {
        //             NetworkInput.TWO_GRAB_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.Parry)
        //         {
        //             NetworkInput.TWO_BLUE_FRENZY_INPUT = true;
        //         }
        //         if (replayCardData.playerTwoInputs[i].input == InputEnum.RedFrenzy)
        //         {
        //             NetworkInput.TWO_RED_FRENZY_INPUT = true;
        //         }
        //         j++;
        //         NextReplayAction2();
        //     }
        // }
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
        // _replayInput.SetActive(true);
        // _replayPrompts.SetActive(true);
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
            SaveReplay();
    }
#endif
}

