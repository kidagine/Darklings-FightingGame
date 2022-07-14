using Demonics.Sounds;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
	[SerializeField] private PlayersMenu _playersMenu = default;
	[SerializeField] private string _inputName = default;
	private RectTransform _rectTransform;
	private Audio _audio;
	private float _originalPositionY;
	private readonly float _left = -375.0f;
	private readonly float _right = 375.0f;
	private readonly float _center = 0.0f;
	private bool _isMovenentInUse;


	private void Awake()
	{
		_audio = GetComponent<Audio>();
		_rectTransform = GetComponent<RectTransform>();
		_originalPositionY = _rectTransform.anchoredPosition.y;
	}

	void Update()
	{
		Movement(_inputName);
	}

	private void Movement(string inputName)
	{
		float movement = Input.GetAxisRaw(inputName + "Horizontal");
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

	public void Center()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		transform.GetChild(1).gameObject.SetActive(true);
		_playersMenu.CpuTextLeft.SetActive(true);
		_playersMenu.CpuTextRight.SetActive(true);
		_rectTransform.anchoredPosition = new Vector2(_center, _originalPositionY);
	}
}
