using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenu : BaseMenu
{
	[SerializeField] private Image _stageImage = default;


	public void SelectStageImage()
	{
		StartCoroutine(SelectStageCoroutine());
	}

	public void SetStageImage(Sprite sprite)
	{
		_stageImage.sprite = sprite;
	}

	public void SetStageIndex(int index)
	{
		SceneSettings.StageIndex = index;
	}

	public void SetStageIndexRandom()
	{
		int randomStageIndex = Random.Range(0, 2);
		SceneSettings.StageIndex = randomStageIndex;
	}

	public void SetTrainingMode(bool state)
	{
		SceneSettings.IsTrainingMode = state;
	}

	IEnumerator SelectStageCoroutine()
	{
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene(2);
	}
}
