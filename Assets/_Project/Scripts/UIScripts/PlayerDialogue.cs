using Demonics.Sounds;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
	[SerializeField] private PlayerDialogue _otherPlayerDialogue = default;
	[SerializeField] private TextMeshProUGUI _dialogueText = default;
	private Audio _audio;
	private DialogueSO _dialogue;
	private Coroutine _dialogueCoroutine;
	private CharacterTypeEnum _opponentCharacter;
	private bool _isPlayerOne;
	public bool FinishedDialogue { get; private set; }


	void Start()
	{
		_audio = GetComponent<Audio>();
	}

	public void Initialize(bool isPlayerOne, DialogueSO dialogue, CharacterTypeEnum opponentCharacter)
	{
		_isPlayerOne = isPlayerOne;
		_dialogue = dialogue;
		_opponentCharacter = opponentCharacter;
	}

	public void PlayDialogue()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		_dialogueCoroutine = StartCoroutine(PlayDialogueCoroutine(GetSentence(_dialogue, _opponentCharacter)));
	}

	IEnumerator PlayDialogueCoroutine(string sentence)
	{
		for (int i = 0; i < sentence.Length; i++)
		{
			yield return new WaitForSecondsRealtime(0.05f);
			_dialogueText.text += sentence[i];
			_audio.Sound("Typing").Play();
		}
		yield return new WaitForSecondsRealtime(1.5f);
		transform.GetChild(0).gameObject.SetActive(false);
		yield return new WaitForSecondsRealtime(0.25f);
		FinishedDialogue = true;
		if (FinishedDialogue && _otherPlayerDialogue.FinishedDialogue)
		{
			GameManager.Instance.StartRound();
		}
		else
		{
			_otherPlayerDialogue.PlayDialogue();
		}
	}

	private string GetSentence(DialogueSO dialogue, CharacterTypeEnum opponentCharacter)
	{
		if (_isPlayerOne)
		{
			for (int i = 0; i < dialogue.playerOneDialogues.Length; i++)
			{
				if (dialogue.playerOneDialogues[i].character == opponentCharacter)
				{
					return dialogue.playerOneDialogues[i].sentence;
				}
			}
			return "Let's fight.";
		}
		else
		{
			for (int i = 0; i < dialogue.playerTwoDialogues.Length; i++)
			{
				if (dialogue.playerTwoDialogues[i].character == opponentCharacter)
				{
					return dialogue.playerTwoDialogues[i].sentence;
				}
			}
			return "Let's fight.";
		}
	}

	public void StopDialogue()
	{
		if (_dialogueCoroutine != null)
		{
			StopCoroutine(_dialogueCoroutine);
			transform.GetChild(0).gameObject.SetActive(false);
			FinishedDialogue = true;
		}
	}
}
