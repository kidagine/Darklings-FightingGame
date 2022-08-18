using Demonics.UI;
using System.Collections;
using UnityEngine;

public class PatchNotesMenu : BaseMenu
{
	[SerializeField] private InputManager _inputManager = default;
	[SerializeField] private BaseMenu[] _menues = default;
	[SerializeField] private PromptsInput[]  _prompts = default;
	[SerializeField] private RectTransform _scrollView = default;
	[SerializeField] private RectTransform _patchNotes = default;
	private readonly int _scrollSpeed = 350;
	private int _previousMenuIndex = 0;
	private float bottomScrollLimit = 0.0f;

	public void Back()
	{
		_inputManager.CurrentPrompts = _prompts[_previousMenuIndex];
		OpenMenuHideCurrent(_menues[_previousMenuIndex]);
	}

	public void SetPreviousMenuIndex(int index)
	{
		_previousMenuIndex = index;
	}

	void Update()
	{
		Scroll();
	}

	private void Scroll()
	{
		float movement = (_inputManager.NavigationInput.y * Time.deltaTime * _scrollSpeed) * -1;
		if (movement < 0 && _scrollView.anchoredPosition.y > 0 || movement > 0 && _scrollView.anchoredPosition.y <= bottomScrollLimit)
		{
			_scrollView.anchoredPosition += new Vector2(0.0f, movement);
			if (_scrollView.anchoredPosition.y < 0.0f)
			{
				_scrollView.anchoredPosition = new Vector2(0.0f, 0.0f);
			}
			else if(_scrollView.anchoredPosition.y > bottomScrollLimit)
			{
				_scrollView.anchoredPosition = new Vector2(0.0f, bottomScrollLimit);
			}
		}
	}

	void OnEnable()
	{
		_scrollView.anchoredPosition = Vector2.zero;
	}

	void Start()
	{
		StartCoroutine(SetUpPatchNotesCoroutine());
	}

	IEnumerator SetUpPatchNotesCoroutine()
	{
		yield return null;
		_patchNotes.anchoredPosition = new Vector2(0.0f, -(_patchNotes.rect.height / 2.0f));
		bottomScrollLimit = _patchNotes.rect.height - 300.0f;
		_scrollView.anchoredPosition = Vector2.zero;
	}
}
