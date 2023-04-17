using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersusMenu : BaseMenu
{
    [SerializeField] private Selectable _localOption = default;
    [SerializeField] private GameObject _unavailableText = default;
    [SerializeField] private GameObject _experimentalText = default;
    [SerializeField] private Image _startingImage = default;
    [SerializeField] private TextMeshProUGUI _startingText = default;

    void OnEnable()
    {
#if UNITY_WEBGL
        StartCoroutine(ActivateCoroutine());
#endif
#if !UNITY_WEBGL
        _experimentalText.SetActive(true);
#endif
    }

    IEnumerator ActivateCoroutine()
    {
        yield return null;
        _startingOption.GetComponent<Button>().interactable = false;
        _startingOption.GetComponent<Animator>().enabled = false;
        _startingText.color = Color.gray;
        _startingImage.color = Color.gray;
        _unavailableText.SetActive(true);
        yield return null;
        _localOption.Select();
    }
}
