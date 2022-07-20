using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHistory : MonoBehaviour
{
	[SerializeField] private Sprite _down = default;
	[SerializeField] private Sprite _up = default;
	[SerializeField] private Sprite _left = default;
	[SerializeField] private Sprite _right = default;
	[SerializeField] private Sprite _light = default;
	[SerializeField] private Sprite _medium = default;
	[SerializeField] private Sprite _heavy = default;
	[SerializeField] private Sprite _special = default;
	[SerializeField] private Sprite _assist = default;
	private readonly List<InputHistoryImage> _inputHistoryImages = new();
	private readonly List<InputEnum> _inputEnums = new();
	private Coroutine _inputBreakCoroutine;
	private float _startInputTime;
	private int _currentInputImageIndex;
	private int _previousInputImageIndex;
	private bool _isNextInputBreak;
	private bool _isNextInputSubItem;

	public List<InputEnum> Inputs { get; private set; } = new();
	public List<InputDirectionEnum> Directions { get; private set; } = new();
	public List<float> InputTimes { get; private set; } = new();
	public PlayerController PlayerController { get; set; }


	void Awake()
	{
		foreach (Transform child in transform.GetChild(0))
		{
			_inputHistoryImages.Add(child.GetComponent<InputHistoryImage>());
		}
	}

	public void AddInput(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum = InputDirectionEnum.None)
	{
		if (inputDirectionEnum == InputDirectionEnum.None && inputEnum == InputEnum.Direction)
		{
			InputTimes.Add(Time.time - _startInputTime);
 			Inputs.Add(inputEnum);
			Directions.Add(inputDirectionEnum);
			return;
		}
		if (_inputHistoryImages.Count > 0)
		{
			if (_inputBreakCoroutine != null)
			{
				StopCoroutine(_inputBreakCoroutine);
			}

			if (inputEnum != InputEnum.Direction)
			{
				if (PlayerController.InputDirection.y == -1.0f)
				{
					InputTimes.Add(Time.time - _startInputTime);
					AddMainInput(InputEnum.Direction, InputDirectionEnum.Down);
				}
			}

			if (_isNextInputSubItem && !_inputEnums.Contains(inputEnum))
			{
				InputTimes.Add(Time.time -_startInputTime);
				AddSubInput(inputEnum, inputDirectionEnum);
			}
			else
			{
				InputTimes.Add(Time.time - _startInputTime);
				AddMainInput(inputEnum, inputDirectionEnum);
			}
		}
	}

	private void AddSubInput(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum = InputDirectionEnum.None)
	{
		InputHistoryImage inputHistoryImage = _inputHistoryImages[_previousInputImageIndex];
		if (inputEnum == InputEnum.Parry)
		{
			Image image = inputHistoryImage.ActivateHistoryImage(InputEnum.Medium, false);
			SetInputImageSprite(image, InputEnum.Medium, inputDirectionEnum);
			_inputEnums.Add(InputEnum.Medium);

			Image image1 = inputHistoryImage.ActivateHistoryImage(InputEnum.Heavy, false);
			SetInputImageSprite(image1, InputEnum.Heavy, inputDirectionEnum);
			_inputEnums.Add(InputEnum.Heavy);
		}
		else if (inputEnum == InputEnum.Throw)
		{
			Image image = inputHistoryImage.ActivateHistoryImage(InputEnum.Light, false);
			SetInputImageSprite(image, InputEnum.Light, inputDirectionEnum);
			_inputEnums.Add(InputEnum.Light);

			Image image1 = inputHistoryImage.ActivateHistoryImage(InputEnum.Medium, false);
			SetInputImageSprite(image1, InputEnum.Medium, inputDirectionEnum);
			_inputEnums.Add(InputEnum.Medium);
		}
		else
		{
			Image image = inputHistoryImage.ActivateHistoryImage(inputEnum, false);
			SetInputImageSprite(image, inputEnum, inputDirectionEnum);
			_inputEnums.Add(inputEnum);
		}
		Inputs.Add(inputEnum);
		Directions.Add(inputDirectionEnum);
		_startInputTime = Time.time;
	}

	private void AddMainInput(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum = InputDirectionEnum.None)
	{
		InputHistoryImage inputHistoryImage = _inputHistoryImages[_currentInputImageIndex];
		if (_isNextInputBreak)
		{
			_isNextInputBreak = false;
			inputHistoryImage.ActivateEmptyHistoryImage();
			IncreaseCurrentInputImageIndex();
			inputHistoryImage = _inputHistoryImages[_currentInputImageIndex];
		}
		if (inputEnum == InputEnum.Parry)
		{
			Image image = inputHistoryImage.ActivateHistoryImage(InputEnum.Medium, true);
			SetInputImageSprite(image, InputEnum.Medium, inputDirectionEnum);
			_inputEnums.Add(InputEnum.Medium);

			Image image1 = inputHistoryImage.ActivateHistoryImage(InputEnum.Heavy, false);
			SetInputImageSprite(image1, InputEnum.Heavy, inputDirectionEnum);
			_inputEnums.Add(InputEnum.Heavy);
		}
		else if (inputEnum == InputEnum.Throw)
		{
			Image image = inputHistoryImage.ActivateHistoryImage(InputEnum.Light, true);
			SetInputImageSprite(image, InputEnum.Light, inputDirectionEnum);
			_inputEnums.Add(InputEnum.Light);

			Image image1 = inputHistoryImage.ActivateHistoryImage(InputEnum.Medium, false);
			SetInputImageSprite(image1, InputEnum.Medium, inputDirectionEnum);
			_inputEnums.Add(InputEnum.Medium);
		}
		else
		{
			Image image = inputHistoryImage.ActivateHistoryImage(inputEnum, true);
			SetInputImageSprite(image, inputEnum, inputDirectionEnum);
			_inputEnums.Add(inputEnum);
		}
		Inputs.Add(inputEnum);
		Directions.Add(inputDirectionEnum);
		_startInputTime = Time.time;
		IncreaseCurrentInputImageIndex();
		_inputBreakCoroutine = StartCoroutine(InputBreakCoroutine());
		StartCoroutine(InputSubItemCoroutine());
	}


	private IEnumerator InputBreakCoroutine()
	{
		yield return new WaitForSecondsRealtime(1.0f);
		_isNextInputBreak = true;
	}

	private IEnumerator InputSubItemCoroutine()
	{
		_isNextInputSubItem = true;
		yield return new WaitForSecondsRealtime(0.15f);
		_isNextInputSubItem = false;
		_inputEnums.Clear();
	}

	private void IncreaseCurrentInputImageIndex()
	{
		if (_currentInputImageIndex < _inputHistoryImages.Count - 1)
		{
			_previousInputImageIndex = _currentInputImageIndex;
			_currentInputImageIndex++;
		}
		else
		{
			_previousInputImageIndex = _inputHistoryImages.Count - 1;
			_currentInputImageIndex = 0;
		}
	}

	private void SetInputImageSprite(Image inputImage, InputEnum inputEnum, InputDirectionEnum inputDirectionEnum = InputDirectionEnum.None)
	{
		switch (inputEnum)
		{
			case InputEnum.Direction:
				switch (inputDirectionEnum)
				{
					case InputDirectionEnum.Up:
						inputImage.sprite = _up;
						break;
					case InputDirectionEnum.Down:
						inputImage.sprite = _down;
						break;
					case InputDirectionEnum.Left:
						inputImage.sprite = _left;
						break;
					case InputDirectionEnum.Right:
						inputImage.sprite = _right;
						break;
				}
				break;
			case InputEnum.Light:
				inputImage.sprite = _light;
				break;
			case InputEnum.Medium:
				inputImage.sprite = _medium;
				break;
			case InputEnum.Heavy:
				inputImage.sprite = _heavy;
				break;
			case InputEnum.Special:
				inputImage.sprite = _special;
				break;
			case InputEnum.Assist:
				inputImage.sprite = _assist;
				break;
		}
	}

}
