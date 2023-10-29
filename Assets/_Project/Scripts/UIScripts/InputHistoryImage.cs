using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHistoryImage : MonoBehaviour
{
    [SerializeField] private GameObject _frames = default;
    [SerializeField] private TextMeshProUGUI _framesText = default;
    [SerializeField] private Image _sequenceImage = default;
    [SerializeField] private Sprite[] _sequenceSprites = default;
    [SerializeField] private GameObject[] _triggers = default;
    private int _frame;

    public void UpdateDisplay(int sequenceIndex, int[] triggerIndexes)
    {
        _frame = 0;
        _frames.gameObject.SetActive(true);
        UpdateSequencesDisplay(sequenceIndex);
        UpdateTriggersDisplay(triggerIndexes);
    }

    private void UpdateSequencesDisplay(int index)
    {
        _sequenceImage.transform.parent.gameObject.SetActive(true);
        _sequenceImage.sprite = _sequenceSprites[index];
    }

    private void UpdateTriggersDisplay(int[] triggerIndexes)
    {
        for (int i = 0; i < _triggers.Length; i++)
            _triggers[i].SetActive(false);
        for (int i = 0; i < triggerIndexes.Length; i++)
            if (triggerIndexes[i] <= _triggers.Length - 1)
                _triggers[triggerIndexes[i]].SetActive(true);
    }

    public void UpdateFramesDisplay()
    {
        if (_frame < 99)
            _frame++;
        _framesText.text = _frame.ToString();
    }
}
