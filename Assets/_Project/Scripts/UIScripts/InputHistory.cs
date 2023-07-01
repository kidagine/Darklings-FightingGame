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
    private List<int> previousInputs = new List<int>();


    public void UpdateDisplay(InputList inputList)
    {
        List<int> currentInputs = new List<int>();
        for (int i = 0; i < inputList.inputTriggers.Length; i++)
            if (inputList.inputTriggers[i].held)
                if (!currentInputs.Contains(i))
                    currentInputs.Add(i);

        if (currentInputs.Count != previousInputs.Count)
        {
            _inputHistoryContainer.GetChild(_inputHistoryContainer.childCount - 1).transform.SetAsFirstSibling();
            _inputHistoryImage = _inputHistoryContainer.GetChild(0).GetComponent<InputHistoryImage>();
            _inputHistoryImage.UpdateDisplay(0, currentInputs.ToArray());
        }
        if (_inputHistoryImage != null)
            _inputHistoryImage.UpdateFramesDisplay();
        previousInputs = currentInputs;
    }
}
