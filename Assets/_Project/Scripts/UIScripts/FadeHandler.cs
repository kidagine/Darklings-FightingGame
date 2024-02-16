using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image _fadeImage = default;
    [Header("Fading")]
    [Range(0f, 5f)]
    [SerializeField] private float _fadeDuration = 1.2f;
    [Range(0f, 5f)]
    private Coroutine _fadeTransitionCoroutine;
    [SerializeField] private bool _fadeOnStart = true;
    [HideInInspector] public UnityEvent onFadeEnd;

    void Awake()
    {
        if (_fadeOnStart)
            StartFadeTransition(false, 1.0f);
    }

    public void StartFadeTransition(bool fadeOut, float duration = default)
    {
        if (duration == 0)
            duration = _fadeDuration;
        if (_fadeTransitionCoroutine != null)
            StopCoroutine(_fadeTransitionCoroutine);
        _fadeTransitionCoroutine = StartCoroutine(FadeTransitionCoroutine(fadeOut, duration));
    }

    IEnumerator FadeTransitionCoroutine(bool fadeOut, float duration)
    {
        int fromAlpha = fadeOut ? 0 : 1;
        int toAlpha = fadeOut ? 1 : 0;
        _fadeImage.color = new Color(0, 0, 0, fromAlpha);
        float elapsedTime = 0.0f;
        float waitTime = duration;
        //yield return new WaitForSecondsRealtime(0.2f);
        while (elapsedTime < waitTime)
        {
            float fade = Mathf.Lerp(fromAlpha, toAlpha, elapsedTime / waitTime);
            _fadeImage.color = new Color(0, 0, 0, fade);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        _fadeImage.color = new Color(0, 0, 0, toAlpha);
        _fadeImage.raycastTarget = false;
        onFadeEnd?.Invoke();
        onFadeEnd.RemoveAllListeners();
    }
}
