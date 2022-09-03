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
	public UnityEvent OnLeftPage;
	public UnityEvent OnRightPage;


	void OnEnable()
	{
		if (_inputManager != null)
		{
			_inputManager.CurrentPrompts = this;
		}
		else
		{
			if (_pauseMenu != null)
			{
				GameManager.Instance.PauseMenu.PlayerInput.GetComponent<PlayerController>().CurrentPrompts = GetComponent<PromptsInput>();
			}
			else
			{
				GameManager.Instance.PlayerOne.GetComponent<PlayerController>().CurrentPrompts = GetComponent<PromptsInput>();
			}
		}
	}

}
