using System.Collections;
using UnityEngine;

public class PatchNotesMenu : BaseMenu
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private PromptsInput _homePrompts = default;
    [SerializeField] private RectTransform _scrollView = default;
    [SerializeField] private RectTransform _patchNotes = default;
    private readonly int _scrollSpeed = 350;
    private float bottomScrollLimit = 0.0f;

    void Update() => Scroll(_inputManager.NavigationInput.y);

    public void Scroll(float navigationInput)
    {
        float movement = navigationInput * Time.deltaTime * _scrollSpeed * -1;
        if (movement < 0 && _scrollView.anchoredPosition.y > 0 || movement > 0 && _scrollView.anchoredPosition.y <= bottomScrollLimit)
        {
            _scrollView.anchoredPosition += new Vector2(0.0f, movement);
            if (_scrollView.anchoredPosition.y < 0.0f)
                _scrollView.anchoredPosition = new Vector2(0.0f, 0.0f);
            else if (_scrollView.anchoredPosition.y > bottomScrollLimit)
                _scrollView.anchoredPosition = new Vector2(0.0f, bottomScrollLimit);
        }
    }

    void OnEnable() => _scrollView.anchoredPosition = Vector2.zero;

    void OnDisable() => _inputManager.SetPrompts(_inputManager.PreviousPrompts);

    void Start() => StartCoroutine(SetUpPatchNotesCoroutine());

    IEnumerator SetUpPatchNotesCoroutine()
    {
        yield return null;
        _patchNotes.anchoredPosition = new Vector2(0.0f, -(_patchNotes.rect.height / 2.0f));
        bottomScrollLimit = _patchNotes.rect.height - 300.0f;
        _scrollView.anchoredPosition = Vector2.zero;
    }
}
