using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenu : BaseMenu
{
	[SerializeField] private Image _stageImage = default;
	[SerializeField] private TextMeshProUGUI _stageName = default;

	public void SelectStageImage()
	{
		SceneManager.LoadScene(2);
		//StartCoroutine(SelectStageCoroutine());
	}

	public void SetStageImage(Sprite sprite)
	{
		_stageImage.sprite = sprite;
		_stageName.text = sprite.name;
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
