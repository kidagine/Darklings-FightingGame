using Demonics.Sounds;
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
        _playerTwoDialogue.PlayDialogue();
    }

    public void SetPlayerNames(string playerOne, string playerTwo)
    {
        _playerOneName.text = playerOne;
        _playerTwoName.text = playerTwo;
    }

    public void PlayTextAppearSoundAnimationEvent()
	{
        _audio.Sound("TextAppear").Play();
	}
}
