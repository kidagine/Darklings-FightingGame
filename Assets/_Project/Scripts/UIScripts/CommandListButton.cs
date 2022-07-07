using TMPro;
using UnityEngine;

public class CommandListButton : MonoBehaviour
{
	[SerializeField] private CommandListMenu _commandListMenu = default;
	[SerializeField] private TextMeshProUGUI _commandName = default;
	private ArcanaSO _command;


	public void SetData(ArcanaSO command)
	{
		_command = command;
		_commandName.text = _command.arcanaName;
	}

	public void UpdateShowcase()
	{
		_commandListMenu.SetCommandListShowcase(_command);
	}
}
