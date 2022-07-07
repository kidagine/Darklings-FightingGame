using TMPro;
using UnityEngine;

public class CommandListButton : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _commandName = default;


	public void SetData(string commandName)
	{
		_commandName.text = commandName;
	}
}
