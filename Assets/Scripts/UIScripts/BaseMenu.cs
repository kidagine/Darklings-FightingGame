using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenu : MonoBehaviour	
{
	[SerializeField] private Selectable _startingOption = default;


	public void OpenMenuHideCurrent(BaseMenu menu)
	{
		gameObject.SetActive(false);
		menu.Show();
	}

	public void OpenMenu(BaseMenu menu)
	{
		menu.gameObject.SetActive(true);
	}

	public void Show()
	{
		gameObject.SetActive(true);
		StartCoroutine(ActivateCoroutine());
	}

	void OnEnable()
	{
		StartCoroutine(ActivateCoroutine());
	}

	IEnumerator ActivateCoroutine()
	{
		yield return null;
		if (_startingOption != null)
		{
			_startingOption.Select();
		}
	}
}