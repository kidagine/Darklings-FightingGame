using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPixelOutline : MonoBehaviour
{
	[SerializeField] private GameObject _outlineTextPrefab = default;
	[SerializeField] private float _spacing = 5.0f;
	private readonly List<TextMeshProUGUI> _outlineTexts = new();
	private TextMeshProUGUI _mainText;
	private RectTransform _outlineTextGroup;

	private void Awake()
	{
		_mainText = GetComponent<TextMeshProUGUI>();
		_outlineTextGroup = Instantiate(_outlineTextPrefab, transform.parent).GetComponent<RectTransform>();
		_outlineTextGroup.SetAsFirstSibling();
		_outlineTextGroup.anchoredPosition = _mainText.rectTransform.anchoredPosition;
		_outlineTextGroup.anchorMin = _mainText.rectTransform.anchorMin;
		_outlineTextGroup.anchorMax = _mainText.rectTransform.anchorMax;
		_outlineTextGroup.pivot = _mainText.rectTransform.pivot;
		foreach (Transform outlineText in _outlineTextGroup.transform)
		{
			_outlineTexts.Add(outlineText.GetComponent<TextMeshProUGUI>());
		}
		SetSpacing();
	}

	private void SetSpacing()
	{
		_outlineTexts[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(_spacing, 0.0f);
		_outlineTexts[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-_spacing, 0.0f);
		_outlineTexts[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, _spacing);
		_outlineTexts[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -_spacing);
	}

	private void LateUpdate()
	{
		for (int i = 0; i < _outlineTexts.Count; i++)
		{
			_outlineTexts[i].text = _mainText.text;
			_outlineTexts[i].fontSize = _mainText.fontSize;
			_outlineTexts[i].alignment = _mainText.alignment;
			_outlineTexts[i].fontStyle = _mainText.fontStyle;
			_outlineTexts[i].enableWordWrapping = _mainText.enableWordWrapping;
			_outlineTexts[i].rectTransform.sizeDelta = _mainText.rectTransform.sizeDelta;
			_outlineTexts[i].transform.localRotation = _mainText.transform.localRotation;
			_outlineTexts[i].transform.localScale = _mainText.transform.localScale;
		}
	}
}
