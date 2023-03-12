using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    [SerializeField] private PlayerDialogue _otherPlayerDialogue = default;
    [SerializeField] private TextMeshProUGUI _dialogueText = default;
    private Audio _audio;
    private DialogueSO _dialogue;
    private CharacterTypeEnum _opponentCharacter;
    private string _sentence;
    private bool _isPlayerOne;
    public bool FinishedDialogue { get; private set; }
    private int _dialoguePlayFrame;
    private int _dialogueWaitFrame = 3;
    private int _midDialogueFrame;
    private int _endDialogueFrame;
    private bool _skip;

    void Start()
    {
        _audio = GetComponent<Audio>();
    }

    public void Initialize(bool isPlayerOne, DialogueSO dialogue, CharacterTypeEnum opponentCharacter)
    {
        _sentence = null;
        _skip = false;
        _endDialogueFrame = 0;
        _midDialogueFrame = 0;
        _dialoguePlayFrame = 0;
        _dialogueText.text = "";
        _isPlayerOne = isPlayerOne;
        _dialogue = dialogue;
        _opponentCharacter = opponentCharacter;
    }

    public void PlayDialogue()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        _sentence = GetSentence(_dialogue, _opponentCharacter);
    }

    void FixedUpdate()
    {
        if (!_skip)
        {
            if (_sentence != null)
            {
                if (_dialoguePlayFrame > _sentence.Length - 1)
                {
                    _midDialogueFrame = 120;
                    _sentence = null;
                }
                else
                {
                    if (DemonicsWorld.WaitFramesOnce(ref _dialogueWaitFrame))
                    {
                        _dialogueText.text += _sentence[_dialoguePlayFrame];
                        _audio.Sound("Typing").Play();
                        _dialogueWaitFrame = 3;
                        _dialoguePlayFrame++;
                    }
                }
            }
            if (_midDialogueFrame > 0)
            {
                if (DemonicsWorld.WaitFramesOnce(ref _midDialogueFrame))
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    _endDialogueFrame = 60;
                }
            }
            if (_endDialogueFrame > 0)
            {
                if (DemonicsWorld.WaitFramesOnce(ref _endDialogueFrame))
                {
                    FinishedDialogue = true;
                    if (FinishedDialogue && _otherPlayerDialogue.FinishedDialogue)
                    {
                        GameplayManager.Instance.StartRound();
                    }
                    else
                    {
                        _otherPlayerDialogue.PlayDialogue();
                    }
                }
            }
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
        _skip = true;
        transform.GetChild(0).gameObject.SetActive(false);
        FinishedDialogue = true;
    }
}
