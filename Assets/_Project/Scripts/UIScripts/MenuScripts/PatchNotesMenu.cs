using Demonics.UI;
using UnityEngine;

public class PatchNotesMenu : BaseMenu
{
	[SerializeField] private BaseMenu[] _menues = default;
	private int _previousMenuIndex = 0;


	public void Back()
	{
		OpenMenuHideCurrent(_menues[_previousMenuIndex]);
	}

	public void SetPreviousMenuIndex(int index)
	{
		_previousMenuIndex = index;
	}
}
