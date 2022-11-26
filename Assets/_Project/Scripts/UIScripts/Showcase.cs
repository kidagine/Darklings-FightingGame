using System.Collections;
using UnityEngine;

public class Showcase : MonoBehaviour
{
	[SerializeField] private Transform _showcases = default;
	[SerializeField] private Transform _showcaseDots = default;
	private Coroutine _showcaseCoroutine;
	private int _count;


	IEnumerator ShowcaseCoroutine()
	{
		while (true)
		{
			DisableAllShowcases();
			_showcases.GetChild(_count).gameObject.SetActive(true);
			_showcaseDots.GetChild(_count).GetChild(1).gameObject.SetActive(true);
			_count++;
			if (_count == _showcases.childCount)
			{
				_count = 0;
			}
			yield return new WaitForSecondsRealtime(6.0f);
		}
	}

	private void DisableAllShowcases()
	{
		foreach (Transform transform in _showcases)
		{
			transform.gameObject.SetActive(false);
		}
		foreach (Transform transform in _showcaseDots)
		{
			transform.GetChild(1).gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		_showcaseCoroutine = StartCoroutine(ShowcaseCoroutine());
	}

	private void OnDisable()
	{
		StopCoroutine(_showcaseCoroutine);
		DisableAllShowcases();
		_count = 0;
		_showcases.GetChild(0).gameObject.SetActive(true);
		_showcaseDots.GetChild(0).GetChild(1).gameObject.SetActive(true);
	}
}
