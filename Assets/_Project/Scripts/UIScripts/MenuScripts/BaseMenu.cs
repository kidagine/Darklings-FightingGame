using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseMenu : MonoBehaviour
{
    [SerializeField] protected Selectable _startingOption = default;
    public Selectable PreviousSelectable { get; private set; }

    public void OpenMenuHideCurrent(BaseMenu menu)
    {
        EventSystem.current.SetSelectedGameObject(null);
        gameObject.SetActive(false);
        menu.Show();
    }

    public void OpenMenu(BaseMenu menu)
    {
        menu.gameObject.SetActive(true);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        if (gameObject.activeSelf)
            StartCoroutine(ActivateCoroutine());
    }

    public void Hide()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        EventSystem.current.SetSelectedGameObject(null);
        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        if (gameObject.activeSelf)
            StartCoroutine(ActivateCoroutine());
    }

    protected IEnumerator ActivateCoroutine()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected != null)
            PreviousSelectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
        yield return null;
        if (_startingOption != null)
            _startingOption.Select();
    }
}
