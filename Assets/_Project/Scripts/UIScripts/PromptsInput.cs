using UnityEngine;
using UnityEngine.Events;

public class PromptsInput : MonoBehaviour
{
	public UnityEvent OnConfirm;
	public UnityEvent OnBack;
	public UnityEvent OnCoop;
	public UnityEvent OnStage;
	public UnityEvent OnControls;


	void OnEnable()
	{
		InputManager.Instance.CurrentPrompts = this;
	}
}
