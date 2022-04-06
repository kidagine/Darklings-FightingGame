using Demonics.UI;
using UnityEngine;

public class PauseMenu : BaseMenu
{
	[SerializeField] private PlayerUI _playerUI = default;
	public string PauseControllerType { get; set; }


	public void ClosePause()
	{
		_playerUI.ClosePause();
	}
}
