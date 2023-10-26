using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHistory : MonoBehaviour
{
    [SerializeField] Transform _inputHistoryContainer = default;
    private InputHistoryImage _inputHistoryImage;
    public PlayerController PlayerController { get; set; }
    private List<int> previousTriggers = new();
    private int previousSequence = -1;
    public List<InputItemNetwork> Inputs { get; set; } = new();
    public InputItemNetwork[] Sequences { get; set; }
    public void UpdateDisplay(InputList inputList, InputBufferNetwork inputBuffer)
    {
        Inputs.Clear();
        Inputs.AddRange(inputBuffer.triggers);
        Inputs.AddRange(inputBuffer.sequences);
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
