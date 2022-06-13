using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainingMenu : BaseMenu
{
    [SerializeField] private GameObject _p1 = default;
    [SerializeField] private GameObject _p2 = default;
    [SerializeField] private InputHistory _inputHistoryOne = default;
    [SerializeField] private InputHistory _inputHistoryTwo = default;
    [SerializeField] private TextMeshProUGUI _startupOneText = default;
    [SerializeField] private TextMeshProUGUI _activeOneText = default;
    [SerializeField] private TextMeshProUGUI _recoveryOneText = default;
    [SerializeField] private TextMeshProUGUI _hitTypeOneText = default;
    [SerializeField] private TextMeshProUGUI _stateOneText = default;
    [SerializeField] private TextMeshProUGUI _startupTwoText = default;
    [SerializeField] private TextMeshProUGUI _activeTwoText = default;
    [SerializeField] private TextMeshProUGUI _recoveryTwoText = default;
    [SerializeField] private TextMeshProUGUI _hitTypeTwoText = default;
    [SerializeField] private TextMeshProUGUI _stateTwoText = default;
    [SerializeField] private Canvas[] _uiCanvases = default;
    [SerializeField] private BaseMenu _trainingPauseMenu = default;
    [SerializeField] private PauseMenu _pauseMenu = default;
    [SerializeField] private TrainingSubOption[] _trainingSubOptions = default;
    [Header("Selectors")]
    private int _currentTrainingSubOptionIndex;


    void Update()
    {
        if (Input.GetButtonDown(_pauseMenu.PauseControllerType + "UILeft"))
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
        if (Input.GetButtonDown(_pauseMenu.PauseControllerType + "UIRight"))
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
                GameManager.Instance.GameSpeed = 1.0f;
                break;
            case 1:
                GameManager.Instance.GameSpeed = 0.75f;
                break;
            case 2:
                GameManager.Instance.GameSpeed = 0.5f;
                break;
            case 3:
                GameManager.Instance.GameSpeed = 0.25f;
                break;
            case 4:
                GameManager.Instance.GameSpeed = 0.10f;
                break;
        }
    }


    public void SetCpu(int value)
    {
        switch (value)
        {
            case 0:
                TrainingSettings.CpuOff = true;
                GameManager.Instance.DeactivateCpus();
                break;
            case 1:
                TrainingSettings.CpuOff = false;
                GameManager.Instance.DeactivateCpus();
                GameManager.Instance.ActivateCpus();
                break;
        }
    }

    public void SetArcana(int value)
    {
        switch (value)
        {
            case 0:
                GameManager.Instance.InfiniteArcana = false;
                break;
            case 1:
                GameManager.Instance.InfiniteArcana = true;
                break;
        }
    }

    public void SetAssist(int value)
    {
        switch (value)
        {
            case 0:
                GameManager.Instance.InfiniteAssist = false;
                break;
            case 1:
                GameManager.Instance.InfiniteAssist = true;
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
                GameManager.Instance.InfiniteHealth = false;
                break;
            case 1:
                GameManager.Instance.MaxHealths();
                GameManager.Instance.InfiniteHealth = true;
                break;
        }
    }

    public void SetUI(int value)
    {
        switch (value)
        {
            case 0:
                for (int i = 0; i < _uiCanvases.Length; i++)
                {
                    _uiCanvases[i].enabled = true;
                }
                break;
            case 1:
                for (int i = 0; i < _uiCanvases.Length; i++)
                {
                    _uiCanvases[i].enabled = false;
                }
                break;
        }
    }

    public void SetFramedata(int value)
    {
        switch (value)
        {
            case 0:
                _p1.SetActive(false);
                _p2.SetActive(false);
                break;
            case 1:
                _p1.SetActive(true);
                _p2.SetActive(false);
                break;
            case 2:
                _p1.SetActive(false);
                _p2.SetActive(true);
                break;
            case 3:
                _p1.SetActive(true);
                _p2.SetActive(true);
                break;
        }
    }

    public void SetState(bool isPlayerOne, string state)
    {
        if (isPlayerOne)
        {
            _stateOneText.text = state;
        }
        else
        {
            _stateTwoText.text = state;
        }
    }

    public void FramedataValue(bool isPlayerOne, AttackSO attack)
    {
        if (isPlayerOne)
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
        }
    }

    public void SetInputHistory(int value)
    {
        switch (value)
        {
            case 0:
                _inputHistoryOne.gameObject.SetActive(false);
                _inputHistoryTwo.gameObject.SetActive(false);
                break;
            case 1:
                _inputHistoryOne.gameObject.SetActive(true);
                _inputHistoryTwo.gameObject.SetActive(true);
                break;
        }
    }

    public void SetBlock(int value)
    {
        switch (value)
        {
            case 0:
                TrainingSettings.BlockAlways = false;
                break;
            case 1:
                TrainingSettings.BlockAlways = true;
                break;
        }
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

    public void ResetTrainingOptions()
    {
        TrainingSettings.ShowHitboxes = false;
        GameManager.Instance.InfiniteHealth = false;
        GameManager.Instance.InfiniteArcana = false;
        GameManager.Instance.GameSpeed = 1.0f;
        GameManager.Instance.ActivateCpus();
    }

    public void HideMenu()
    {
        _startingOption = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        OpenMenuHideCurrent(_trainingPauseMenu);
    }
}
