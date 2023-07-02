using UnityEngine;
using UnityEngine.UI;

public class InputDisplay : MonoBehaviour
{
    [SerializeField] private Image _sequenceImage = default;
    [SerializeField] private Sprite[] _sequenceSprites = default;
    [SerializeField] private Color _activedTrigger = default;
    [SerializeField] private Color _disabledTrigger = default;
    [SerializeField] private Image[] _triggers = default;

    public void UpdateDisplay(InputList inputList)
    {
        SetSequenceDisplay(inputList.inputSequence.inputDirectionEnum);
        for (int i = 0; i < _triggers.Length; i++)
            SetTriggerDisplay(_triggers[i], inputList.inputTriggers[i].held);
    }

    private void SetTriggerDisplay(Image triggerImage, bool triggerHeld) => triggerImage.color = triggerHeld ? _activedTrigger : _disabledTrigger;

    private void SetSequenceDisplay(InputDirectionEnum inputDirectionEnum) => _sequenceImage.sprite = _sequenceSprites[(int)inputDirectionEnum];
}
