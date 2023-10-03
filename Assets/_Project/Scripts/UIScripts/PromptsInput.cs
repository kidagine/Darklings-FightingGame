using UnityEngine;
using UnityEngine.Events;

public class PromptsInput : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu = default;
    [SerializeField] private InputManager _inputManager = default;
    public UnityEvent OnConfirm;
    public UnityEvent OnBack;
    public UnityEvent OnCoop;
    public UnityEvent OnRebind;
    public UnityEvent OnStage;
    public UnityEvent OnControls;
    public UnityEvent OnToggleFramedata;
    public UnityEvent OnLeftPage;
    public UnityEvent OnRightPage;
    public UnityEvent OnLeftNavigation;
    public UnityEvent OnRightNavigation;
    public UnityEvent OnOptions;

    void OnEnable()
    {
        if (_inputManager != null)
            _inputManager.SetPrompts(this);
        else
        {
            if (_pauseMenu != null)
                GameplayManager.Instance.PauseMenu.PlayerInput.GetComponent<PlayerController>().CurrentPrompts = GetComponent<PromptsInput>();
            else
                GameplayManager.Instance.PlayerOne.GetComponent<PlayerController>().CurrentPrompts = GetComponent<PromptsInput>();
        }
    }

}
