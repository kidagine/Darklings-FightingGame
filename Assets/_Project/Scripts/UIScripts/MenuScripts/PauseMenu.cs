using Demonics.UI;
using UnityEngine;

public class PauseMenu : BaseMenu
{
	[SerializeField] private PlayerUI _playerUI = default;


	public void ClosePause()
	{
		_playerUI.ClosePause();
	}
}
