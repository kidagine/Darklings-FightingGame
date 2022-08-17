using TMPro;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
	[SerializeField] private GameObject _debugPrefab = default;
	[SerializeField] private Transform _debugContent = default;


	void Awake()
	{
		Printer.SetLoaded(this);
	}

	public void Log(object message)
	{
		Debug.Log(message);
		TextMeshProUGUI debugText = Instantiate(_debugPrefab, _debugContent).GetComponent<TextMeshProUGUI>();
		debugText.text = message.ToString();	
	}
}
