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
        List<int> triggers = new();
        for (int i = 0; i < inputList.inputTriggers.Length; i++)
            if (inputList.inputTriggers[i].held && !inputList.inputTriggers[i].sequence)
                if (!triggers.Contains(i))
                    triggers.Add(i);

        int sequence = 0;
        for (int i = 0; i < inputList.inputTriggers.Length; i++)
            if (inputList.inputTriggers[i].held && inputList.inputTriggers[i].sequence)
                sequence = (int)inputList.inputTriggers[i].inputEnum;

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
