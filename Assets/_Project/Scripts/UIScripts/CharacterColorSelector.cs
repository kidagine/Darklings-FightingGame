using Demonics.Sounds;
using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterColorSelector : MonoBehaviour
{
    [SerializeField] private CharacterMenu _characterMenu = default;
    [SerializeField] private TextMeshProUGUI _playerOneColorNumber = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
    [SerializeField] private GameObject _arrows = default;
    [SerializeField] private CharacterColorSelector _otherCharacterColorSelector = default;
    [SerializeField] private bool _isPlayerOne = default;
    private Audio _audio;
    private Vector2 _directionInput;
	private string _controllerInputName;
	private bool _inputDeactivated;


	public int ColorNumber { get; private set; }
	public bool HasSelected { get; set; }


    void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    private void OnEnable()
	{
        if (_isPlayerOne)
        {
            if (SceneSettings.ControllerOne == "")
            {
                if (SceneSettings.ControllerTwo == "")
                {
                    _controllerInputName = "Keyboard";
                }
                else
                {
                    _controllerInputName = SceneSettings.ControllerTwo;
                }
            }
            else
            {
                _controllerInputName = SceneSettings.ControllerOne;
            }
        }
        else
        {
            if (SceneSettings.ControllerTwo == "")
            {
                if (SceneSettings.ControllerOne == "")
                {
                    _controllerInputName = "Keyboard";
                }
                else
                {
                    _controllerInputName = SceneSettings.ControllerOne;
                }
            }
            else
            {
                _controllerInputName = SceneSettings.ControllerTwo;
            }
        }
	}

	private void Update()
	{
		if (!_inputDeactivated)
        {
            _directionInput = new Vector2(Input.GetAxisRaw(_controllerInputName + "Horizontal"), 0.0f);
			if (_directionInput.x == 1.0f)
            {
                _audio.Sound("Pressed").Play();
                ColorNumber++;
				StartCoroutine(ResetInput());
			}
			if (_directionInput.x == -1.0f)
            {
                _audio.Sound("Pressed").Play();
                ColorNumber--;
				StartCoroutine(ResetInput());
			}
            ColorNumber = _playerAnimator.SetSpriteLibraryAsset(ColorNumber);
            _playerOneColorNumber.text = $"Color {ColorNumber + 1}";

            if (Input.GetButtonDown(_controllerInputName + "Confirm"))
            {
                if (_isPlayerOne)
                {
                    SceneSettings.ColorOne = ColorNumber;
                }
                else
                {
                    SceneSettings.ColorTwo = ColorNumber;
                }
                _audio.Sound("Selected").Play();
                _inputDeactivated = true;
                _arrows.SetActive(false);
                _characterMenu.SetCharacter(_isPlayerOne);
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
        _playerAnimator.SetSpriteLibraryAsset(0);
        ColorNumber = 0;
        _inputDeactivated = false;
        _arrows.SetActive(true);
        gameObject.SetActive(false);
    }
}
