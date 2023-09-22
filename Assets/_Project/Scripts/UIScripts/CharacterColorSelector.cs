using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterColorSelector : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private CharacterMenu _characterMenu = default;
    [SerializeField] private TextMeshProUGUI _playerOneColorNumber = default;
    [SerializeField] private TextMeshProUGUI _assistIndicatorText = default;
    [SerializeField] private ChangeStageMenu _changeStageMenu = default;
    [SerializeField] private RebindMenu _rebindMenu = default;
    [SerializeField] private PlayerUIRender _playerUIRender = default;
    [SerializeField] private GameObject _arrows = default;
    [SerializeField] private bool _isPlayerOne = default;
    private Audio _audio;
    private Vector2 _directionInput;
    private bool _inputDeactivated;
    private bool _pressed;

    public int ColorNumber { get; private set; }
    public bool HasSelected { get; set; }


    void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    private void Update()
    {
        Movement();
    }

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
            ColorNumber = _playerUIRender.SetSpriteLibraryAsset(ColorNumber);
            _playerOneColorNumber.text = $"Color {ColorNumber + 1}";
        }
    }

    public void MoveLeft()
    {
        ColorNumber--;
        StartCoroutine(ResetInput());
    }

    public void MoveRight()
    {
        ColorNumber++;
        StartCoroutine(ResetInput());
    }

    public void Confirm()
    {
        if (!_pressed && !_rebindMenu.gameObject.activeSelf)
        {
            _pressed = true;
            if (_isPlayerOne)
                SceneSettings.ColorOne = ColorNumber;
            else
                SceneSettings.ColorTwo = ColorNumber;
            _audio.Sound("Selected").Play();
            _inputDeactivated = true;
            _arrows.SetActive(false);
            _characterMenu.SetCharacter(_isPlayerOne);
            transform.GetChild(0).gameObject.SetActive(false);
            _assistIndicatorText.gameObject.SetActive(false);
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
        _playerUIRender.SetSpriteLibraryAsset(0);
        ColorNumber = 0;
        _inputDeactivated = false;
        _arrows.SetActive(true);
        _pressed = false;
        transform.GetChild(0).gameObject.SetActive(true);
        gameObject.SetActive(false);
        _assistIndicatorText.gameObject.SetActive(true);
    }
}
