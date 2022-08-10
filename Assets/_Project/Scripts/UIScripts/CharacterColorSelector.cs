using Demonics.Sounds;
using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CharacterColorSelector : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager = default;
	[SerializeField] private CharacterMenu _characterMenu = default;
	[SerializeField] private TextMeshProUGUI _playerOneColorNumber = default;
	[SerializeField] private TextMeshProUGUI _assistIndicatorText = default;
	[SerializeField] private ChangeStageMenu _changeStageMenu = default;
	[SerializeField] private PlayerAnimator _playerAnimator = default;
	[SerializeField] private GameObject _arrows = default;
	[SerializeField] private bool _isPlayerOne = default;
	private Audio _audio;
	private Vector2 _directionInput;
	private string _controllerInputName;
	private bool _inputDeactivated;
	private bool _pressed;

	public int ColorNumber { get; private set; }
	public bool HasSelected { get; set; }


	void Awake()
	{
		_audio = GetComponent<Audio>();
	}

	private void OnEnable()
	{

	}

	private void Update()
	{
		Movement();
	}

	public void Movement()
	{
		if (!_inputDeactivated && !_changeStageMenu.IsOpen)
		{
			_directionInput = _inputManager.NavigationInput;
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
		}
	}

	public void Confirm()
	{
		if (!_pressed)
		{
			_pressed = true;
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
		_playerAnimator.SetSpriteLibraryAsset(0);
		ColorNumber = 0;
		_inputDeactivated = false;
		_arrows.SetActive(true);
		transform.GetChild(0).gameObject.SetActive(true);
		gameObject.SetActive(false);
		_assistIndicatorText.gameObject.SetActive(true);
	}
}
