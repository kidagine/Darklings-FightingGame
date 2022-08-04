using Demonics.Sounds;
using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterAssistSelector : MonoBehaviour
{
    [SerializeField] private GameObject _colors = default;
    [SerializeField] private TextMeshProUGUI _playerOneColorNumber = default;
    [SerializeField] private TextMeshProUGUI _assistIndicatorText = default;
    [SerializeField] private Animator _assistAnimator = default;
    [SerializeField] private ChangeStageMenu _changeStageMenu = default;
    [SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private GameObject _arrows = default;
    [SerializeField] private AssistStatsSO[] assistStatsSO = default;
    [SerializeField] private bool _isPlayerOne = default;
    private Audio _audio;
    private Vector2 _directionInput;
    private string _controllerInputName;
    private int _assistCount;
    private bool _inputDeactivated;


    public char AssistLetter { get; private set; } = 'A';
    public bool HasSelected { get; set; }


    void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    private void OnEnable()
    {
        if (_isPlayerOne)
        {
            if (SceneSettings.ControllerOne == "Cpu")
            {
                _controllerInputName = SceneSettings.ControllerTwo;
            }
            else
            {
                if (SceneSettings.ControllerOne == "KeyboardOne" || SceneSettings.ControllerOne == "KeyboardTwo")
                {
                    _controllerInputName = "Keyboard";
                }
                else
                {
                    _controllerInputName = SceneSettings.ControllerOne;
                }
            }
        }
        else
        {
            if (SceneSettings.ControllerTwo == "Cpu")
            {
                _controllerInputName = SceneSettings.ControllerOne;
            }
            else
            {
                if (SceneSettings.ControllerTwo == "KeyboardOne" || SceneSettings.ControllerTwo == "KeyboardTwo")
                {
                    _controllerInputName = "Keyboard";
                }
                else
                {
                    _controllerInputName = SceneSettings.ControllerTwo;
                }
            }
        }
    }

    private void Update()
    {
        if (!_inputDeactivated && !_changeStageMenu.IsOpen)
        {
            _directionInput = new Vector2(Input.GetAxisRaw(_controllerInputName + "Horizontal"), 0.0f);
            if (_directionInput.x == 1.0f && _assistCount < assistStatsSO.Length - 1)
            {
                _audio.Sound("Pressed").Play();
                AssistLetter++;
                _assistCount++;
                StartCoroutine(ResetInput());
            }
            else if (_directionInput.x == 1.0f && _assistCount >= assistStatsSO.Length - 1)
            {
                _audio.Sound("Pressed").Play();
                AssistLetter = 'A';
                _assistCount = 0;
                StartCoroutine(ResetInput());
            }
            if (_directionInput.x == -1.0f && _assistCount > 0)
            {
                _audio.Sound("Pressed").Play();
                AssistLetter--;
                _assistCount--;
                StartCoroutine(ResetInput());
            }
            else if (_directionInput.x == -1.0f && _assistCount <=  0)
            {
                _audio.Sound("Pressed").Play();
                AssistLetter = 'B';
                _assistCount = assistStatsSO.Length - 1;
                StartCoroutine(ResetInput());
            }
            _playerOneColorNumber.text = $"Shadow {AssistLetter}";

            if (Input.GetButtonDown(_controllerInputName + "Confirm"))
            {
                if (_isPlayerOne)
                {
                    SceneSettings.AssistOne = _assistCount;
                }
                else
                {
                    SceneSettings.AssistTwo = _assistCount;
                }
                _audio.Sound("Selected").Play();
                _assistAnimator.Play("AssistSelectorTaunt");
                _colors.SetActive(true);
                _assistIndicatorText.text = AssistLetter.ToString();
                _inputDeactivated = true;
                _arrows.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    IEnumerator ResetInput()
    {
        _inputDeactivated = true;
        _directionInput = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        _inputDeactivated = false;
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
        _playerAnimator.SetSpriteLibraryAsset(0);
        AssistLetter = 'A';
        _assistIndicatorText.text = "";
        _inputDeactivated = false;
        _arrows.SetActive(true);
        _assistCount = 0;
        transform.GetChild(0).gameObject.SetActive(true);
        if (_assistAnimator != null)
        {
            _assistAnimator.Rebind();
        }
    }
}
