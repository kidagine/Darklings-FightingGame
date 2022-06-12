using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPixelOutline : MonoBehaviour
{
	[SerializeField] private GameObject _outlineTextPrefab = default;
	[SerializeField] private float _spacing = 5.0f;
	private readonly List<TextMeshProUGUI> _outlineTexts = new();
	private TextMeshProUGUI _mainText;
	private Transform _outlineTextGroup;

	private void Awake()
	{
		_mainText = GetComponent<TextMeshProUGUI>();
		_outlineTextGroup = Instantiate(_outlineTextPrefab, transform.parent).transform;
		_outlineTextGroup.SetAsFirstSibling();
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

	private void Update()
	{
		if (_mainText.text != _outlineTexts[0].text)
		{
			for (int i = 0; i < _outlineTexts.Count; i++)
			{
				_outlineTexts[i].text = _mainText.text;
				_outlineTexts[i].fontSize = _mainText.fontSize ;
				_outlineTexts[i].alignment = _mainText.alignment;
				_outlineTexts[i].transform.localScale = _mainText.transform.localScale;
			}
		}
	}
}
