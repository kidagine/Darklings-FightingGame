using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenu : BaseMenu
{
	[SerializeField] private Image _stageImage = default;
	[SerializeField] private int _sceneIndex = 2;

	public void SelectStageImage(Sprite sprite)
	{
		StartCoroutine(SelectStageCoroutine(sprite));
	}

	public void SetStageIndex(int index)
	{
		SceneSettings.StageIndex = index;
	}

	public void SetTrainingMode(bool state)
	{
		SceneSettings.IsTrainingMode = state;
	}

	IEnumerator SelectStageCoroutine(Sprite sprite)
	{
		_stageImage.sprite = sprite;
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene(2);
	}
}
