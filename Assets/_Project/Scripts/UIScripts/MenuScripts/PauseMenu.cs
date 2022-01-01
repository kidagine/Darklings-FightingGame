using Demonics.UI;
using UnityEngine;

public class PauseMenu : BaseMenu
{
	[SerializeField] private PlayerUI _playerUI = default;


	void Update()
	{
		if (Input.GetButtonDown("ControllerOne" + "Pause") || Input.GetButtonDown("ControllerTwo" + "Pause") || Input.GetButtonDown("Keyboard" + "Pause"))
		{
			_playerUI.ClosePause();
		}
	}
}
