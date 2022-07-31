using System.Collections;
using TMPro;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _musicName = default;


    public void ShowMusicMenu(string music)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowMusicMenuCoroutine(music));
    }

    IEnumerator ShowMusicMenuCoroutine(string music)
    {
        _musicName.text = music;
        yield return new WaitForSecondsRealtime(2.5f);
        gameObject.SetActive(false);
    }
}
