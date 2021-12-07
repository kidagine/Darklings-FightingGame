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
	private readonly List<Image> _inputImages = new List<Image>();
	private Coroutine _inputBreakCoroutine;
	private int _currentInputImageIndex;
	private bool _isNextInputBreak;


	void Awake()
	{
		foreach (Transform child in transform)
		{
			_inputImages.Add(child.GetChild(0).GetComponent<Image>());
		}	
	}

	public void AddInput(InputEnum inputEnum)
	{
		if (_inputImages.Count > 0)
		{
			if (_inputBreakCoroutine != null)
			{
				StopCoroutine(_inputBreakCoroutine);
			}

			Image inputImage = _inputImages[_currentInputImageIndex];
			if (_isNextInputBreak)
			{
				_isNextInputBreak = false;
				inputImage.enabled = false;
				IncreaseCurrentInputImageIndex();
				inputImage.transform.parent.gameObject.SetActive(true);
				inputImage.transform.parent.SetAsFirstSibling();
				inputImage = _inputImages[_currentInputImageIndex];
			}
			inputImage.enabled = true;
			inputImage.transform.parent.gameObject.SetActive(true);
			inputImage.transform.parent.SetAsFirstSibling();
			SetInputImageSprite(inputImage, inputEnum);
			IncreaseCurrentInputImageIndex();
			_inputBreakCoroutine = StartCoroutine(InputBreakCoroutine());
		}
	}

	private IEnumerator InputBreakCoroutine()
	{
		yield return new WaitForSecondsRealtime(1.0f);
		_isNextInputBreak = true;
	}

	private void IncreaseCurrentInputImageIndex()
	{
		if (_currentInputImageIndex < _inputImages.Count - 1)
		{
			_currentInputImageIndex++;
		}
		else
		{
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
		}
	}
}
