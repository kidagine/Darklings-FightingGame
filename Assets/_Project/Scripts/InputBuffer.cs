using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    private InputHistory _inputHistory;
    private InputDisplay _inputDisplay = default;

    public void Initialize(InputHistory inputHistory, InputDisplay inputDisplay)
    {
        _inputDisplay = inputDisplay;
        _inputHistory = inputHistory;
    }

    public void UpdateBuffer(InputList inputList, InputBufferNetwork inputBuffer)
    {
        _inputDisplay.UpdateDisplay(inputList);
        _inputHistory.UpdateDisplay(inputList, inputBuffer);
    }
}
