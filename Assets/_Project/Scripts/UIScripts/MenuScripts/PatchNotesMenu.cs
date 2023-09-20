using System.Collections;
using UnityEngine;

public class PatchNotesMenu : BaseMenu
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private RectTransform _scrollView = default;
    [SerializeField] private RectTransform _patchNotes = default;
    [SerializeField] private Vector2 _boundaries = default;
    private readonly int _scrollSpeed = 350;
    private float bottomScrollLimit = 0.0f;

    void Update() => Scroll(_inputManager.NavigationInput.y * 4);

    public void Scroll(float navigationInput)
    {
        float movement = navigationInput * Time.deltaTime * _scrollSpeed * -1;
        if (movement < 0 && _patchNotes.anchoredPosition.y > _boundaries.x || movement > 0 && _patchNotes.anchoredPosition.y <= _boundaries.y)
        {
            _patchNotes.anchoredPosition += new Vector2(0.0f, movement);
            if (_patchNotes.anchoredPosition.y < _boundaries.x)
                _patchNotes.anchoredPosition = new Vector2(0.0f, _boundaries.x);
            else if (_patchNotes.anchoredPosition.y > _boundaries.y)
                _patchNotes.anchoredPosition = new Vector2(0.0f, _boundaries.y);
        }
    }

    void OnEnable() => _scrollView.anchoredPosition = Vector2.zero;

    void OnDisable() => _inputManager.SetPrompts(_inputManager.PreviousPrompts);

    void Start() => StartCoroutine(SetUpPatchNotesCoroutine());

    IEnumerator SetUpPatchNotesCoroutine()
    {
        yield return null;
        bottomScrollLimit = _patchNotes.rect.height - 300.0f;
        _boundaries = new Vector2(-_patchNotes.rect.height, _scrollView.rect.height);
        _patchNotes.anchoredPosition = new Vector2(0.0f, -_patchNotes.rect.height);
    }
}
