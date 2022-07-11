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
	[SerializeField] private Sprite _throw = default;
	[SerializeField] private Sprite _parry = default;
	private readonly List<InputHistoryImage> _inputHistoryImages = new();
	private readonly List<InputEnum> _inputEnums = new();
	private Coroutine _inputBreakCoroutine;
	private Coroutine _inputSubItemCoroutine;
	private int _currentInputImageIndex;
	private int _previousInputImageIndex;
	private bool _isNextInputBreak;
	private bool _isNextInputSubItem;


	void Awake()
	{
		foreach (Transform child in transform)
		{
			_inputHistoryImages.Add(child.GetComponent<InputHistoryImage>());
		}	
	}

	public void AddInput(InputEnum inputEnum, InputDirectionEnum inputDirectionEnum = InputDirectionEnum.None)
	{
		Debug.Log("a");
		if (_inputHistoryImages.Count > 0 && gameObject.activeSelf)
		{
			if (_inputBreakCoroutine != null)
			{
				StopCoroutine(_inputBreakCoroutine);
			}


			if (_isNextInputSubItem && !_inputEnums.Contains(inputEnum))
			{
				InputHistoryImage inputHistoryImage = _inputHistoryImages[_previousInputImageIndex];
				Image image = inputHistoryImage.ActivateHistoryImage(inputEnum, false);
				SetInputImageSprite(image, inputEnum, inputDirectionEnum);
				_inputEnums.Add(inputEnum);
			}
			else
			{
				if (_inputSubItemCoroutine != null)
				{
					StopCoroutine(_inputSubItemCoroutine);
				}
				InputHistoryImage inputHistoryImage = _inputHistoryImages[_currentInputImageIndex];
				if (_isNextInputBreak)
				{
					_isNextInputBreak = false;
					inputHistoryImage.ActivateEmptyHistoryImage();
					IncreaseCurrentInputImageIndex();
					inputHistoryImage = _inputHistoryImages[_currentInputImageIndex];
				}
				Image image = inputHistoryImage.ActivateHistoryImage(inputEnum, true);
				SetInputImageSprite(image, inputEnum, inputDirectionEnum);
				_inputEnums.Add(inputEnum);

				IncreaseCurrentInputImageIndex();
				_inputBreakCoroutine = StartCoroutine(InputBreakCoroutine());
				_inputSubItemCoroutine = StartCoroutine(InputSubItemCoroutine());
			}

		}
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
			case InputEnum.Throw:
				inputImage.sprite = _throw;
				break;
			case InputEnum.Parry:
				inputImage.sprite = _parry;
				break;
		}
	}
}
