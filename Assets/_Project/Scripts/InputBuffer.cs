using System.Collections.Generic;
using UnityEngine;

public class InputBuffer : MonoBehaviour
{
    private List<InputBufferAction> _inputBuffer = new List<InputBufferAction>();
    private InputHistory _inputHistory;
    private readonly bool _actionAllowed = true;


    void Update()
    {
        if (_actionAllowed)
        {
            TryBufferedAction();
        }
    }

    public void Initialize(InputHistory inputHistory)
    {
        _inputHistory = inputHistory;
    }

    public void CheckInput(InputEnum inputEnum)
    {
        _inputHistory.AddInput(inputEnum);
        _inputBuffer.Add(new InputBufferAction(InputBufferAction.InputAction.Jump, Time.time));
    }

    private void TryBufferedAction()
    {
        if (_inputBuffer.Count > 0)
        {
            foreach (InputBufferAction ai in _inputBuffer.ToArray())
            {
                _inputBuffer.Remove(ai);
                if (ai.CheckIfValid())
                {
                    DoAction(ai);
                    break;
                }
            }
        }
    }

    private void DoAction(InputBufferAction ai)
    {
        //_actionAllowed = false;
    }
}
