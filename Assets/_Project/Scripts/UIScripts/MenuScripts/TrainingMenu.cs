using Demonics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TrainingSettings;

public class TrainingMenu : BaseMenu
{
    [SerializeField] private GameObject _p1 = default;
    [SerializeField] private GameObject _framedataMeterGroup = default;
    [SerializeField] private GameObject _inputDisplayOne = default;
    [SerializeField] private GameObject _inputDisplayTwo = default;
    [SerializeField] private CanvasGroup _canvasGroup = default;
    [SerializeField] private FrameMeterSystem _frameMeterSystem = default;
    [SerializeField] private InputHistory _inputHistoryOne = default;
    [SerializeField] private InputHistory _inputHistoryTwo = default;
    [SerializeField] private TextMeshProUGUI _startupOneText = default;
    [SerializeField] private TextMeshProUGUI _activeOneText = default;
    [SerializeField] private TextMeshProUGUI _recoveryOneText = default;
    [SerializeField] private TextMeshProUGUI _hitTypeOneText = default;
    [SerializeField] private TextMeshProUGUI _damageOneText = default;
    [SerializeField] private TextMeshProUGUI _damageComboOneText = default;
    [SerializeField] private TextMeshProUGUI _stateOneText = default;
    [SerializeField] private TextMeshProUGUI _startupTwoText = default;
    [SerializeField] private TextMeshProUGUI _activeTwoText = default;
    [SerializeField] private TextMeshProUGUI _recoveryTwoText = default;
    [SerializeField] private TextMeshProUGUI _hitTypeTwoText = default;
    [SerializeField] private TextMeshProUGUI _damageTwoText = default;
    [SerializeField] private TextMeshProUGUI _damageComboTwoText = default;
    [SerializeField] private TextMeshProUGUI _stateTwoText = default;
    [SerializeField] private Canvas[] _uiCanvases = default;
    [SerializeField] private BaseMenu _trainingPauseMenu = default;
    [SerializeField] private TrainingSubOption[] _trainingSubOptions = default;
    [Header("Selectors")]
    private int _currentTrainingSubOptionIndex;
    public CanvasGroup CanvasGroup { get { return _canvasGroup; } private set { } }

    public void ConfigurePlayers(Player playerOne, Player playerTwo)
    {

    }

    public void ChangePage(bool left)
    {
        if (left)
        {
            if (_currentTrainingSubOptionIndex == 0)
            {
                _currentTrainingSubOptionIndex = _trainingSubOptions.Length - 1;
            }
            else
            {
                _currentTrainingSubOptionIndex--;
            }
            for (int i = 0; i < _trainingSubOptions.Length; i++)
            {
                if (i != _currentTrainingSubOptionIndex)
                {
                    _trainingSubOptions[i].Disable();
                }
            }
            _trainingSubOptions[_currentTrainingSubOptionIndex].Activate();
        }
        else
        {

            if (_currentTrainingSubOptionIndex == _trainingSubOptions.Length - 1)
            {
                _currentTrainingSubOptionIndex = 0;
            }
            else
            {
                _currentTrainingSubOptionIndex++;
            }
            for (int i = 0; i < _trainingSubOptions.Length; i++)
            {
                if (i != _currentTrainingSubOptionIndex)
                {
                    _trainingSubOptions[i].Disable();
                }
            }
            _trainingSubOptions[_currentTrainingSubOptionIndex].Activate();
        }
    }
    public void SetInputHistory(int value)
    {
        switch (value)
        {
            case 0:
                _inputHistoryOne.transform.GetChild(0).gameObject.SetActive(false);
                _inputHistoryTwo.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 1:
                _inputHistoryOne.transform.GetChild(0).gameObject.SetActive(true);
                _inputHistoryTwo.transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
    }
    public void SetHitboxes(int value)
    {
        if (value == 1)
        {
            TrainingSettings.ShowHitboxes = true;
        }
        else
        {
            TrainingSettings.ShowHitboxes = false;
        }
    }

    public void SetSlowdown(int value)
    {
        switch (value)
        {
            case 0:
                GameplayManager.Instance.GameSpeed = 1.0f;
                break;
            case 1:
                GameplayManager.Instance.GameSpeed = 0.75f;
                break;
            case 2:
                GameplayManager.Instance.GameSpeed = 0.5f;
                break;
            case 3:
                GameplayManager.Instance.GameSpeed = 0.25f;
                break;
            case 4:
                GameplayManager.Instance.GameSpeed = 0.10f;
                break;
        }
    }


    public void SetCpu(int value)
    {
        switch (value)
        {
            case 0:
                TrainingSettings.CpuOff = true;
                GameplayManager.Instance.DeactivateCpus();
                break;
            case 1:
                TrainingSettings.CpuOff = false;
                GameplayManager.Instance.DeactivateCpus();
                GameplayManager.Instance.ActivateCpus();
                break;
        }
    }

    public void SetArcana(int value)
    {
        switch (value)
        {
            case 0:
                GameplayManager.Instance.InfiniteArcana = false;
                break;
            case 1:
                GameplayManager.Instance.InfiniteArcana = true;
                CheckTrainingGauges();
                break;
        }
    }

    public void SetAssist(int value)
    {
        switch (value)
        {
            case 0:
                GameplayManager.Instance.InfiniteAssist = false;
                break;
            case 1:
                GameplayManager.Instance.InfiniteAssist = true;
                CheckTrainingGauges();
                break;
        }
    }

    public void SetStance(int value)
    {
        switch (value)
        {
            case 0:
                TrainingSettings.Stance = 0;
                break;
            case 1:
                TrainingSettings.Stance = 1;
                break;
            case 2:
                TrainingSettings.Stance = 2;
                break;
        }
    }

    public void SetHealth(int value)
    {
        switch (value)
        {
            case 0:
                GameplayManager.Instance.InfiniteHealth = false;
                break;
            case 1:
                GameplayManager.Instance.InfiniteHealth = true;
                CheckTrainingGauges();
                break;
        }
    }

    private void CheckTrainingGauges()
    {
        if (GameSimulation.Run)
        {
            GameSimulation._players[0].CurrentState.CheckTrainingComboEnd(GameSimulation._players[0], true);
            GameSimulation._players[1].CurrentState.CheckTrainingComboEnd(GameSimulation._players[1], true);
        }
    }

    public void SetUI(int value)
    {
        switch (value)
        {
            case 0:
                for (int i = 0; i < _uiCanvases.Length; i++)
                    _uiCanvases[i].enabled = false;
                break;
            case 1:
                for (int i = 0; i < _uiCanvases.Length; i++)
                    _uiCanvases[i].enabled = true;
                break;
        }
    }

    public void SetInputDisplay(int value)
    {
        switch (value)
        {
            case 0:
                _inputDisplayOne.SetActive(false);
                _inputDisplayTwo.SetActive(false);
                break;
            case 1:
                _inputDisplayOne.SetActive(true);
                _inputDisplayTwo.SetActive(true);
                break;
        }
    }

    public void SetFramedataMeter(int value)
    {
        switch (value)
        {
            case 0:
                _framedataMeterGroup.SetActive(false);
                break;
            case 1:
                _framedataMeterGroup.SetActive(true);
                break;
        }
    }

    public void SetFramedata(int value)
    {
        switch (value)
        {
            case 0:
                _p1.SetActive(false);
                break;
            case 1:
                _p1.SetActive(true);
                break;
        }
    }

    public void SetState(int isPlayerOne, string state)
    {
        if (isPlayerOne == 0)
        {
            _stateOneText.text = state;
        }
        else
        {
            _stateTwoText.text = state;
        }
    }

    public void FramedataValue(int isPlayerOne, ResultAttack attack)
    {
        if (attack == null)
        {
            return;
        }
        if (isPlayerOne == 0)
        {
            if (_startupOneText.gameObject.activeSelf)
            {
                _startupOneText.text = attack.startUpFrames.ToString();
            }
            if (_activeOneText.gameObject.activeSelf)
            {
                _activeOneText.text = attack.activeFrames.ToString();
            }
            if (_recoveryOneText.gameObject.activeSelf)
            {
                _recoveryOneText.text = attack.recoveryFrames.ToString();
            }
            if (_hitTypeOneText.gameObject.activeSelf)
            {
                _hitTypeOneText.text = attack.attackTypeEnum.ToString();
            }
            if (_damageOneText.gameObject.activeSelf)
            {
                _damageOneText.text = attack.damage.ToString();
            }
            if (_damageComboOneText.gameObject.activeSelf)
            {
                if (attack.comboDamage > 0)
                {
                    _damageComboOneText.text = attack.comboDamage.ToString();
                }
            }
        }
        else
        {
            if (_startupTwoText.gameObject.activeSelf)
            {
                _startupTwoText.text = attack.startUpFrames.ToString();
            }
            if (_activeTwoText.gameObject.activeSelf)
            {
                _activeTwoText.text = attack.activeFrames.ToString();
            }
            if (_recoveryTwoText.gameObject.activeSelf)
            {
                _recoveryTwoText.text = attack.recoveryFrames.ToString();
            }
            if (_hitTypeTwoText.gameObject.activeSelf)
            {
                _hitTypeTwoText.text = attack.attackTypeEnum.ToString();
            }
            if (_damageTwoText.gameObject.activeSelf)
            {
                _damageTwoText.text = attack.damage.ToString();
            }
            if (_damageComboTwoText.gameObject.activeSelf)
            {
                if (attack.comboDamage > 0)
                {
                    _damageComboTwoText.text = attack.comboDamage.ToString();
                }
            }
        }
    }

    public void FramedataMeterValue(int isPlayerOne, FramedataTypesEnum framedataEnum)
    {
        _frameMeterSystem.AddFrame(isPlayerOne, framedataEnum);
    }
    public void FramedataMeterRun()
    {
        if (!_frameMeterSystem.gameObject.activeInHierarchy)
            return;
        if (Time.timeScale == 0)
            return;
        _frameMeterSystem.RunFrame();
    }

    public void SetBlock(int value)
    {
        TrainingSettings.Block = (BlockType)value;
    }

    public void SetOnHit(int value)
    {
        switch (value)
        {
            case 0:
                TrainingSettings.OnHit = false;
                break;
            case 1:
                TrainingSettings.OnHit = true;
                break;
        }
    }

    public void SetBlockCount(int value)
    {
        TrainingSettings.BlockCount = value + 1;
        TrainingSettings.BlockCountCurrent = TrainingSettings.BlockCount;
    }

    public void ResetTrainingOptions()
    {
        TrainingSettings.ShowHitboxes = false;
        GameplayManager.Instance.InfiniteHealth = false;
        GameplayManager.Instance.InfiniteArcana = false;
        GameplayManager.Instance.GameSpeed = 1.0f;
        GameplayManager.Instance.ActivateCpus();
    }

    public void HideMenu()
    {
        _startingOption = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        OpenMenuHideCurrent(_trainingPauseMenu);
    }
}
