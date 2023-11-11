using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterAssistSelector : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private GameObject _colors = default;
    [SerializeField] private TextMeshProUGUI _playerOneColorNumber = default;
    [SerializeField] private TextMeshProUGUI _assistIndicatorText = default;
    [SerializeField] private Animator _assistAnimator = default;
    [SerializeField] private ChangeStageMenu _changeStageMenu = default;
    [SerializeField] private RebindMenu _rebindMenu = default;
    [SerializeField] private PlayerUIRender _playerUIRender = default;
    [SerializeField] private GameObject _arrows = default;
    [SerializeField] private AssistStatsSO[] assistStatsSO = default;
    [SerializeField] private bool _isPlayerOne = default;
    private Audio _audio;
    private Vector2 _directionInput;
    private int _assistCount;
    private bool _inputDeactivated;
    private bool _pressed;


    public char AssistLetter { get; private set; } = 'A';
    public bool HasSelected { get; set; }


    void Awake() => _audio = GetComponent<Audio>();

    private void Update() => Movement();

    public void Movement()
    {
        if (!_inputDeactivated && !_changeStageMenu.IsOpen && !_rebindMenu.gameObject.activeSelf)
        {
            _directionInput = _inputManager.NavigationInput;
            if (_directionInput.x == -1.0f)
            {
                _audio.Sound("Pressed").Play();
                MoveLeft();
            }
            if (_directionInput.x == 1.0f)
            {
                _audio.Sound("Pressed").Play();
                MoveRight();
            }
        }
    }

    public void MoveLeft()
    {
        if (_assistCount > 0)
        {
            AssistLetter--;
            _assistCount--;
            StartCoroutine(ResetInput());
        }
        else if (_assistCount <= 0)
        {
            AssistLetter = 'C';
            _assistCount = assistStatsSO.Length - 1;
            StartCoroutine(ResetInput());
        }
        _playerOneColorNumber.text = $"Shadow {AssistLetter}";
    }

    public void MoveRight()
    {
        if (_assistCount < assistStatsSO.Length - 1)
        {
            AssistLetter++;
            _assistCount++;
            StartCoroutine(ResetInput());
        }
        else if (_assistCount >= assistStatsSO.Length - 1)
        {
            AssistLetter = 'A';
            _assistCount = 0;
            StartCoroutine(ResetInput());
        }
        _playerOneColorNumber.text = $"Shadow {AssistLetter}";
    }

    public void Confirm()
    {
        if (!_pressed && !_rebindMenu.gameObject.activeSelf)
        {
            _pressed = true;
            if (_isPlayerOne)
                SceneSettings.AssistOne = _assistCount;
            else
                SceneSettings.AssistTwo = _assistCount;
            _audio.Sound("Selected").Play();
            _assistAnimator.Play("AssistSelectorTaunt");
            _colors.SetActive(true);
            _assistIndicatorText.text = AssistLetter.ToString();
            _inputDeactivated = true;
            _arrows.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
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
        _playerUIRender.SetSpriteLibraryAsset(0);
        AssistLetter = 'A';
        _assistIndicatorText.text = "";
        _inputDeactivated = false;
        _arrows.SetActive(true);
        _assistCount = 0;
        _pressed = false;
        transform.GetChild(0).gameObject.SetActive(true);
        if (_assistAnimator != null)
            _assistAnimator.Rebind();
    }
}
