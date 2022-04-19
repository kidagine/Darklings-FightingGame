using Demonics.UI;
using UnityEngine;

public class PatchNotesMenu : BaseMenu
{
	[SerializeField] private BaseMenu[] _menues = default;
	[SerializeField] private RectTransform _scrollView = default;
	private int _previousMenuIndex = 0;
	private readonly int _scrollMultiplier = 250;


	public void Back()
	{
		OpenMenuHideCurrent(_menues[_previousMenuIndex]);
	}

	public void SetPreviousMenuIndex(int index)
	{
		_previousMenuIndex = index;
	}

	void Update()
	{
		Scroll("KeyboardOne");
		Scroll("KeyboardTwo");
		Scroll("ControllerOne");
		Scroll("ControllerTwo");
	}

	private void Scroll(string inputName)
	{
		float movement = (Input.GetAxisRaw(inputName + "Vertical") * Time.deltaTime * _scrollMultiplier) * -1;
		if (movement < 0 && _scrollView.anchoredPosition.y >= 0 || movement > 0 && _scrollView.anchoredPosition.y <= 700	)
		{
			_scrollView.anchoredPosition += new Vector2(0.0f, movement);
		}
	}

	private void OnEnable()
	{
		_scrollView.anchoredPosition = Vector2.zero;
	}
}
