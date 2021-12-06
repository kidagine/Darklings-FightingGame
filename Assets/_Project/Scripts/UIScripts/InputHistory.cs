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

	public void AddInput()
	{
		Image inputImage = _inputImages[_currentInputImageIndex];
		inputImage.gameObject.SetActive(true);
		inputImage.transform.SetAsFirstSibling();
		if (_currentInputImageIndex < _inputImages.Count - 1)
		{
			_currentInputImageIndex++;
		}
	}
}
