using Demonics.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : BaseMenu
{
	[SerializeField] private PlayerUI _playerUI = default;
	public string PauseControllerType { get; set; }


	public void ClosePause()
	{
		_playerUI.ClosePause();
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
