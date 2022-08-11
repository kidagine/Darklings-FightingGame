using Demonics.Sounds;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerIcon : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager = default;
	[SerializeField] private PlayersMenu _playersMenu = default;
	private RectTransform _rectTransform;
	private Audio _audio;
	private readonly float _left = -375.0f;
	private readonly float _right = 375.0f;
	private readonly float _center = 0.0f;
	private bool _isMovenentInUse;
	private float _originalPositionY;
	private PlayerInput _playerInput;

	private void Awake()
	{
		_audio = GetComponent<Audio>();
		_rectTransform = GetComponent<RectTransform>();
		_playerInput = GetComponent<PlayerInput>();	
		_originalPositionY = _rectTransform.anchoredPosition.y;
		_playerInput.onActionTriggered += (e) => Debug.Log("AA");
	}

	public void Movement(CallbackContext callbackContext)
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
		}
		if (movement == 0.0f)
		{
			_isMovenentInUse = false;
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
			_playersMenu.CpuTextLeft.SetActive(true);
			_playersMenu.CpuTextRight.SetActive(true);
			_rectTransform.anchoredPosition = new Vector2(_center, _originalPositionY);
		}
	}
}
