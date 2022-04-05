using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainingSubOption : MonoBehaviour
{
	[SerializeField] private Selectable _initialSelectable = default;
	private Selectable _currentInitialSelectable;


	public void Activate()
	{
		gameObject.SetActive(true);
		if (_currentInitialSelectable == null)
		{
			_currentInitialSelectable = _initialSelectable;
		}
		_currentInitialSelectable.Select();
	}

	public void Disable()
	{
		if (gameObject.activeSelf)
		{
			_currentInitialSelectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
		}
		gameObject.SetActive(false);
	}
}
