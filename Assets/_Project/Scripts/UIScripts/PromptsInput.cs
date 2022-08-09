using UnityEngine;
using UnityEngine.Events;

public class PromptsInput : MonoBehaviour
{
	[SerializeField] private InputManager _inputManager = default;
	public UnityEvent OnConfirm;
	public UnityEvent OnBack;
	public UnityEvent OnCoop;
	public UnityEvent OnStage;
	public UnityEvent OnControls;


	void OnEnable()
	{
		if (_inputManager != null)
			_inputManager.CurrentPrompts = this;
	}
}
