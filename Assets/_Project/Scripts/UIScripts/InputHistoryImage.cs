using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHistoryImage : MonoBehaviour
{
	private readonly List<InputTypes> _inputTypes = new List<InputTypes>();
	public int CurrentInputHistoryIndex { get; private set; }


	void Awake()
	{
		foreach (Transform child in transform)
		{
			_inputTypes.Add(child.GetComponent<InputTypes>());
		}
	}

	public Image ActivateHistoryImage(InputEnum inputEnum, bool reset)
	{
		int index = 0;
		for (int i = 0; i < _inputTypes.Count; i++)
		{
			if (_inputTypes[i].InputEnum == inputEnum)
			{
				index = i;
			}
			if (reset)
			{
				_inputTypes[i].gameObject.SetActive(false);
			}
		}
		CurrentInputHistoryIndex++;
		_inputTypes[index].gameObject.SetActive(true);
		transform.SetAsFirstSibling();
		return _inputTypes[index].transform.GetChild(0).GetComponent<Image>();
	}

	public void ActivateEmptyHistoryImage()
	{
		for (int i = 0; i < _inputTypes.Count; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
		CurrentInputHistoryIndex = 0;
		transform.GetChild(0).gameObject.SetActive(false);
		transform.SetAsFirstSibling();
	}
}
