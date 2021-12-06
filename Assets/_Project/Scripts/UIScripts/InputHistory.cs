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
	private int _currentInputImageIndex;


	void Awake()
	{
		foreach (Transform child in transform)
		{
			_inputImages.Add(child.GetComponent<Image>());
		}	
	}

	public void AddInput(InputEnum inputEnum)
	{
		Image inputImage = _inputImages[_currentInputImageIndex];
		inputImage.gameObject.SetActive(true);
		inputImage.transform.SetAsFirstSibling();
		SetInputImageSprite(inputImage, inputEnum);
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
