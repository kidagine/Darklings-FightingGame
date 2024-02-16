using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : BaseMenu
{
    [SerializeField] private PlayerUI _playerUI = default;
    [SerializeField] private TextMeshProUGUI _whoPaused = default;
    [SerializeField] private PromptsInput _prompts = default;
    [SerializeField] private BaseMenu _trainingTopBar = default;
    [SerializeField] private InputManager _inputManager = default;
    public bool PlayerOnePaused { get; private set; }
    public PlayerInput PlayerInput { get; set; }

    public void ClosePause()
    {
        _inputManager.SetPrompts(null);
        _playerUI.ClosePause();
        _prompts.enabled = false;
    }

    public void SetWhoPaused(bool playerOnePaused)
    {
        GameplayManager.Instance.PauseMenu = this;
        PlayerOnePaused = playerOnePaused;
        if (playerOnePaused)
            _whoPaused.text = "P1 Paused";
        else
            _whoPaused.text = "P2 Paused";
    }

    protected override void OnEnable()
    {
        if (_trainingTopBar != null)
            _trainingTopBar.Show();
        if (gameObject.activeSelf)
            StartCoroutine(PromptEnablerCoroutine());
        base.OnEnable();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator PromptEnablerCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _prompts.enabled = true;
    }
}
