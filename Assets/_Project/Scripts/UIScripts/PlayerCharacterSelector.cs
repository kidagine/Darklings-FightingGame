using System.Collections;
using UnityEngine;

public class PlayerCharacterSelector : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector2 _directionInput;
    private Audio _audio;


    public bool HasSelected { get; set; }


    void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    IEnumerator ResetInput()
    {
        _directionInput = Vector2.zero;
        yield return new WaitForSeconds(0.15f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 currentPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 180.0f);
        if (Mathf.Approximately(currentPosition.x, collision.transform.localPosition.x))
        {
            _audio.Sound("Selected").Play();
            PlayerStatsSO playerStats = collision.GetComponent<CharacterButton>().PlayerStatsSO;
            bool isRandomizer = collision.GetComponent<CharacterButton>().IsRandomizer;
        }
    }
}
