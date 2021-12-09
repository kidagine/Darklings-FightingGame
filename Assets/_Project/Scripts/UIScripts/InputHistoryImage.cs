using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHistoryImage : MonoBehaviour
{
	private readonly List<Image> _inputImages = new List<Image>();


	void Awake()
	{
		foreach (Transform child in transform)
		{
			_inputImages.Add(child.GetChild(0).GetComponent<Image>());
		}
	}

	public void ActivateHistoryImage()
	{
		_inputImages[0].enabled = true;
		transform.GetChild(0).gameObject.SetActive(true);
		transform.SetAsFirstSibling();
	}

	public void ActivateEmptyHistoryImage()
	{
		_inputImages[0].enabled = false;
		transform.GetChild(0).gameObject.SetActive(true);
		transform.SetAsFirstSibling();
	}

	public Image GetHistoryImage()
	{
		return _inputImages[0];
	}
}
