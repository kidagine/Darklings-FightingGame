using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHistoryImage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _framesActionText = default;
    public List<InputTypes> InputTypes { get; private set; } = new List<InputTypes>();
    private static int _previousFrame;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            InputTypes.Add(child.GetComponent<InputTypes>());
        }
    }

    public Image ActivateHistoryImage(InputEnum inputEnum, bool reset)
    {
        int index = 1;
        if (reset)
            UpdateFramesUntilAction();
        for (int i = 1; i < InputTypes.Count; i++)
        {
            if (InputTypes[i].InputEnum == inputEnum)
            {
                index = i;
            }
            if (reset)
            {
                InputTypes[i].gameObject.SetActive(false);
            }
        }
        InputTypes[index].gameObject.SetActive(true);
        transform.SetAsFirstSibling();
        return InputTypes[index].transform.GetChild(0).GetComponent<Image>();
    }

    public void ActivateEmptyHistoryImage()
    {
        for (int i = 0; i < InputTypes.Count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(false);
        transform.SetAsFirstSibling();
    }

    private void UpdateFramesUntilAction()
    {
        InputTypes[0].gameObject.SetActive(true);
        int framesUntilAction = 0;
        int cache = _previousFrame;
        int totalFramesUntilAction = DemonicsWorld.Frame - cache;
        if (totalFramesUntilAction > 99)
            framesUntilAction = 99;
        else
            framesUntilAction = totalFramesUntilAction;
        _previousFrame = DemonicsWorld.Frame;
        _framesActionText.text = framesUntilAction.ToString();
    }
}
