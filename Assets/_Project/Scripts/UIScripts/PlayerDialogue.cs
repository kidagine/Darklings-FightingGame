using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
	[SerializeField] private PlayerDialogue _otherPlayerDialogue = default;
	[SerializeField] private TextMeshProUGUI _dialogueText = default;
	private DialogueSO _dialogue;
	private CharacterTypeEnum _opponentCharacter;


	public void Initialize(DialogueSO dialogue, CharacterTypeEnum opponentCharacter)
	{
		_dialogue = dialogue;
		_opponentCharacter = opponentCharacter;
	}

	public void PlayDialogue()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		StartCoroutine(PlayDialogueCoroutine(GetSentence(_dialogue, _opponentCharacter)));
	}

	IEnumerator PlayDialogueCoroutine(string sentence)
	{
		for (int i = 0; i < sentence.Length; i++)
		{
			yield return new WaitForSeconds(0.05f);
			_dialogueText.text += sentence[i];
		}
		yield return new WaitForSeconds(1.0f);
		transform.GetChild(0).gameObject.SetActive(false);
		yield return new WaitForSeconds(0.3f);
		if (_otherPlayerDialogue != null)
		{
			_otherPlayerDialogue.PlayDialogue();
		}
		else
		{
			GameManager.Instance.StartRound();
		}
	}

	private string GetSentence(DialogueSO dialogue, CharacterTypeEnum opponentCharacter)
	{
		for (int i = 0; i < dialogue.dialogues.Length; i++)
		{
			if (dialogue.dialogues[i].character == opponentCharacter)
			{
				return dialogue.dialogues[i].sentence;
			}
		}
		return "Let's fight.";
	}
}
