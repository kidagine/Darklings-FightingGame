using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHistory : MonoBehaviour
{
    [SerializeField] Transform _inputHistoryContainer = default;
    private InputHistoryImage _inputHistoryImage;
    public List<InputEnum> Inputs { get; private set; } = new();
    public List<InputDirectionEnum> Directions { get; private set; } = new();
    public List<float> InputTimes { get; private set; } = new();
    public PlayerController PlayerController { get; set; }
    private List<int> previousTriggers = new List<int>();
    private int previousSequence = -1;

    public void UpdateDisplay(InputList inputList)
    {
        int sequence = (int)inputList.inputSequence.inputDirectionEnum;
        List<int> triggers = new List<int>();
        for (int i = 0; i < inputList.inputTriggers.Length; i++)
            if (inputList.inputTriggers[i].held)
                if (!triggers.Contains(i))
                    triggers.Add(i);

        if (triggers.Count != previousTriggers.Count || sequence != previousSequence)
        {
            _inputHistoryContainer.GetChild(_inputHistoryContainer.childCount - 1).transform.SetAsFirstSibling();
            _inputHistoryImage = _inputHistoryContainer.GetChild(0).GetComponent<InputHistoryImage>();
            _inputHistoryImage.UpdateDisplay(sequence, triggers.ToArray());
        }
        if (_inputHistoryImage != null)
            _inputHistoryImage.UpdateFramesDisplay();
        previousSequence = sequence;
        previousTriggers = triggers;
    }
}
