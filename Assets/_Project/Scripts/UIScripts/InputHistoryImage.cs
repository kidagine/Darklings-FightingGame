using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHistoryImage : MonoBehaviour
{
    public List<InputTypes> InputTypes { get; private set; } = new List<InputTypes>();

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
        //InputTypes[0].gameObject.SetActive(true);
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
}
