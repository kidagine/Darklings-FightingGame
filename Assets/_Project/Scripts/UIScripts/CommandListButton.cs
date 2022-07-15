using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandListButton : MonoBehaviour
{
	[SerializeField] private CommandListMenu _commandListMenu = default;
	[SerializeField] private TextMeshProUGUI _commandName = default;
	[SerializeField] private Image _knockdownImage = default;
	[SerializeField] private Image _reversalImage = default;
	[SerializeField] private Image _projectileImage = default;

	private ArcanaSO _command;


	public void SetData(ArcanaSO command)
	{
		_command = command;
		_commandName.text = _command.arcanaName;
		_reversalImage.gameObject.SetActive(false);
		_knockdownImage.gameObject.SetActive(false);
		_projectileImage.gameObject.SetActive(false);
		if (_command.reversal)
		{
			_reversalImage.gameObject.SetActive(true);
		}
		if (_command.causesKnockdown)
		{
			_knockdownImage.gameObject.SetActive(true);
		}
		if (_command.isProjectile)
		{
			_projectileImage.gameObject.SetActive(true);
		}
	}

	public void UpdateShowcase()
	{
		_commandListMenu.SetCommandListShowcase(_command);
	}
}
