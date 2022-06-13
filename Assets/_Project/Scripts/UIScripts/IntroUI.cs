using Demonics.Sounds;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class IntroUI : MonoBehaviour
{
	[SerializeField] private PlayerDialogue _playerOneDialogue = default;
	[SerializeField] private PlayerDialogue _playerTwoDialogue = default;
	[SerializeField] private TextMeshProUGUI _playerOneName = default;
	[SerializeField] private TextMeshProUGUI _playerTwoName = default;
	private Audio _audio;


	private void Start()
	{
		_audio = GetComponent<Audio>();
	}

	public void PlayDialogueAnimationEvent()
	{
		_playerOneDialogue.PlayDialogue();
	}

	public void SetPlayerNames(string playerOne, string playerTwo)
	{
		_playerOneName.text = Regex.Replace(playerOne, "([a-z])([A-Z])", "$1 $2"); ;
		_playerTwoName.text = Regex.Replace(playerTwo, "([a-z])([A-Z])", "$1 $2"); ;
	}

	public void PlayTextAppearSoundAnimationEvent()
	{
		_audio.Sound("TextAppear").Play();
	}
}
