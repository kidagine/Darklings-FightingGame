using Demonics.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerIcon : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager = default;
	[SerializeField] private PlayersMenu _playersMenu = default;
	[SerializeField] private PlayerInput _playerInput = default;
	[SerializeField] private PromptsImageChanger[] _promptsImageChangers = default;
	[SerializeField] private TextMeshProUGUI _controllerText = default;
	private RectTransform _rectTransform;
	private Audio _audio;
	private readonly float _left = -375.0f;
	private readonly float _right = 375.0f;
	private readonly float _center = 0.0f;
	private bool _isMovenentInUse;
	private float _originalPositionY;
	private int _deviceId;


	private void Awake()
	{
		_audio = GetComponent<Audio>();
		_rectTransform = GetComponent<RectTransform>();
		_originalPositionY = _rectTransform.anchoredPosition.y;
	}

	public void SetController()
	{
		if (_playerInput.devices.Count > 0)
		{
			gameObject.SetActive(true);
			_controllerText.text = _playerInput.devices[0].displayName;
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void Movement(CallbackContext callbackContext)
	{
		if (gameObject.activeInHierarchy)
		{
			float movement = callbackContext.ReadValue<Vector2>().x;
			if (movement != 0.0f)
			{
				if (!_isMovenentInUse)
				{
					_isMovenentInUse = true;
					if (movement > 0.0f)
					{
						if (_rectTransform.anchoredPosition.x == _left)
						{
							_audio.Sound("Selected").Play();
							Center();
						}
						else if (!_playersMenu.IsOnRight())
						{
							_audio.Sound("Selected").Play();
							transform.GetChild(1).gameObject.SetActive(false);
							_playersMenu.CpuTextRight.SetActive(false);
							_rectTransform.anchoredPosition = new Vector2(_right, 275.0f);
						}
					}
					else if (movement < 0.0f)
					{
						if (_rectTransform.anchoredPosition.x == _right)
						{
							_audio.Sound("Selected").Play();
							Center();
						}
						else if (!_playersMenu.IsOnLeft())
						{
							_audio.Sound("Selected").Play();
							transform.GetChild(0).gameObject.SetActive(false);
							_playersMenu.CpuTextLeft.SetActive(false);
							_rectTransform.anchoredPosition = new Vector2(_left, 275.0f);
						}
					}
				}
				_playersMenu.UpdateLeftRightCpu();
			}
			if (movement == 0.0f)
			{
				_isMovenentInUse = false;
			}
		}
	}

	public void OpenOtherMenu(CallbackContext callbackContext)
	{
		if (gameObject.activeInHierarchy)
		{
			if (callbackContext.performed)
			{
				if (_rectTransform.anchoredPosition.x == _left || _rectTransform.anchoredPosition.x == _right)
				{
					_playersMenu.OpenOtherMenu();
				}
			}
		}
	}

	public void ConfirmQuickAssign(CallbackContext callbackContext)
	{
		if (gameObject.activeInHierarchy && !_playersMenu.IsOnLeft())
		{
			if (callbackContext.performed)
			{
				_audio.Sound("Selected").Play();
				_rectTransform.anchoredPosition = new Vector2(_left, 275.0f);
				_playersMenu.UpdateLeftRightCpu();
			}
		}
	}

	void OnDisable()
	{
		_rectTransform.anchoredPosition = new Vector2(_center,_originalPositionY);
	}

	public void Center()
	{
		if (gameObject.activeSelf)
		{
			transform.GetChild(0).gameObject.SetActive(true);
			transform.GetChild(1).gameObject.SetActive(true);
			_rectTransform.anchoredPosition = new Vector2(_center, _originalPositionY);
		}
	}
}
