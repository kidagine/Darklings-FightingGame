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
	[SerializeField] private Sprite _special = default;
	private readonly List<InputHistoryImage> _inputHistoryImages = new List<InputHistoryImage>();
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

	public void AddInput(InputEnum inputEnum)
	{
		if (_inputHistoryImages.Count > 0)
		{
			if (_inputBreakCoroutine != null)
			{
				StopCoroutine(_inputBreakCoroutine);
			}
			if (_inputSubItemCoroutine != null)
			{
				StopCoroutine(_inputSubItemCoroutine);
			}

			if (_isNextInputSubItem && inputEnum != InputEnum.Up && inputEnum != InputEnum.Down && inputEnum != InputEnum.Left  && inputEnum != InputEnum.Right)
			{
				InputHistoryImage inputHistoryImage = _inputHistoryImages[_previousInputImageIndex];
				_isNextInputSubItem = false;
				inputHistoryImage.ActivateHistoryImage(1);
				SetInputImageSprite(inputHistoryImage.GetHistoryImage(1), inputEnum);
			}
			else
			{
				InputHistoryImage inputHistoryImage = _inputHistoryImages[_currentInputImageIndex];
				if (_isNextInputBreak)
				{
					_isNextInputBreak = false;
					inputHistoryImage.ActivateEmptyHistoryImage();
					IncreaseCurrentInputImageIndex();
					inputHistoryImage = _inputHistoryImages[_currentInputImageIndex];
				}
				inputHistoryImage.ActivateHistoryImage();
				SetInputImageSprite(inputHistoryImage.GetHistoryImage(), inputEnum);
				_inputSubItemCoroutine = StartCoroutine(InputSubItemCoroutine());
			}
			IncreaseCurrentInputImageIndex();
			_inputBreakCoroutine = StartCoroutine(InputBreakCoroutine());
		}
	}

	private IEnumerator InputBreakCoroutine()
	{
		yield return new WaitForSecondsRealtime(1.5f);
		_isNextInputBreak = true;
	}

	private IEnumerator InputSubItemCoroutine()
	{
		_isNextInputSubItem = true;
		yield return new WaitForSecondsRealtime(0.15f);
		_isNextInputSubItem = false;
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

	private void SetInputImageSprite(Image inputImage, InputEnum inputEnum)
	{
		switch (inputEnum)
		{
			case InputEnum.Up:
				inputImage.sprite = _up;
				break;
			case InputEnum.Down:
				inputImage.sprite = _down;
				break;
			case InputEnum.Left:
				inputImage.sprite = _left;
				break;
			case InputEnum.Right:
				inputImage.sprite = _right;
				break;
			case InputEnum.Light:
				inputImage.sprite = _light;
				break;
			case InputEnum.Special:
				inputImage.sprite = _special;
				break;
		}
	}
}
