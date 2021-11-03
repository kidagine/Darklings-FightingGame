using Demonics.UI;
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
		SceneSettings.SceneSettingsDecide = true;
		SceneManager.LoadScene(2);
	}

	public void SetStageImage(Sprite sprite)
	{
		_stageImage.sprite = sprite;
		_stageName.text = sprite.name;
	}

	public void SetStageIndex(int index)
	{
		SceneSettings.RandomStage = false;
		SceneSettings.StageIndex = index;
	}

	public void SetStageIndexRandom()
	{
		SceneSettings.RandomStage = true;
	}

	public void SetTrainingMode(bool state)
	{
		SceneSettings.IsTrainingMode = state;
	}
}
