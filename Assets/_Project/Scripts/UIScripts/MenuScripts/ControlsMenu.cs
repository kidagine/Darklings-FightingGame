using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsMenu : BaseMenu
{
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private GameObject[] _controlSchemes = default;
    private Audio _audio;
    private int _currentControlSchemeIndex;


    void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    public void ToggleControlsScheme()
    {
        _audio.Sound("Select").Play();
        _currentControlSchemeIndex++;
        if (_currentControlSchemeIndex >= _controlSchemes.Length)
        {
            _currentControlSchemeIndex = 0;
        }
        for (int i = 0; i < _controlSchemes.Length; i++)
        {
            _controlSchemes[i].SetActive(false);
        }
        _controlSchemes[_currentControlSchemeIndex].SetActive(true);
    }

    private void OnDisable()
    {
        PreviousSelectable.Select();
        _inputManager.SetPrompts(_inputManager.PreviousPrompts);
        if (HotBarToggle.PreviousSelected != null)
            EventSystem.current.SetSelectedGameObject(HotBarToggle.PreviousSelected.gameObject);
    }
}
