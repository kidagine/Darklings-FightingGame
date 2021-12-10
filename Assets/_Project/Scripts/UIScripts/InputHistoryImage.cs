using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHistoryImage : MonoBehaviour
{
	private readonly List<Image> _inputImages = new List<Image>();
	public int CurrentInputHistoryIndex { get; private set; }

	void Awake()
	{
		foreach (Transform child in transform)
		{
			_inputImages.Add(child.GetChild(0).GetComponent<Image>());
		}
	}

	public void ActivateHistoryImage(int index = 0)
	{
		_inputImages[index].enabled = true;
		for (int i = 0; i < _inputImages.Count; i++)
		{
			if (i != 0)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		CurrentInputHistoryIndex++;
		transform.GetChild(index).gameObject.SetActive(true);
		transform.SetAsFirstSibling();
	}

	public void ActivateEmptyHistoryImage()
	{
		_inputImages[0].enabled = false;
		for (int i = 0; i < _inputImages.Count; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
		CurrentInputHistoryIndex = 0;
		transform.GetChild(0).gameObject.SetActive(true);
		transform.SetAsFirstSibling();
	}

	public Image GetHistoryImage(int index = 0)
	{
		return _inputImages[index];
	}
}
