using UnityEngine;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private PlayerDialogue _playerDialogue = default;


    public void PlayDialogueAnimationEvent()
    {
        _playerDialogue.PlayDialogue();
    }
}
