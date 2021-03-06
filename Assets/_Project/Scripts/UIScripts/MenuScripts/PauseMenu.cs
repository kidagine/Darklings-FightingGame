using Demonics.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : BaseMenu
{
	[SerializeField] private PlayerUI _playerUI = default;
	[SerializeField] private TextMeshProUGUI _whoPaused = default;
	public string PauseControllerType { get; set; }
	public bool PlayerOnePaused { get; private set; }

	public void ClosePause()
	{
		_playerUI.ClosePause();
	}

	public void SetWhoPaused(bool playerOnePaused)
	{
		PlayerOnePaused = playerOnePaused;
		if (playerOnePaused)
		{
			_whoPaused.text = "Player 1 Paused";
		}
		else
		{
			_whoPaused.text = "Player 2 Paused";
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
