using Demonics.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenu : BaseMenu
{
	[SerializeField] private Selectable _1bitSelectable = default;
	[SerializeField] private GameObject _optionsGroup = default;
	[SerializeField] private GameObject _stagesGroup = default;
	[SerializeField] private BaseMenu _charactersSelectMenu = default;
	[SerializeField] private Image _stageImage = default;
	[SerializeField] private TextMeshProUGUI _stageName = default;


	public void SelectStageImage()
	{
		SceneSettings.SceneSettingsDecide = true;
		_optionsGroup.SetActive(true);
		_stagesGroup.SetActive(false);
		_1bitSelectable.Select();
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
		SceneSettings.StageIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(StageTypeEnum)).Length - 1);
	}

	public void SetTrainingMode(bool state)
	{
		SceneSettings.IsTrainingMode = state;
	}

	public void Set1Bit(int index)
	{
		if (index == 0)
		{
			SceneSettings.Bit1 = false;
		}
		else if (index == 1)
		{
			SceneSettings.Bit1 = true;
		}
	}

	public void SetMusic(int index)
	{
		SceneSettings.MusicIndex = index;
	}

	public void ConfirmStageMenu()
	{
		if (_optionsGroup.activeSelf)
		{
			SceneManager.LoadScene(2);
		}
	}

	public void BackStageMenu()
	{
		if (_optionsGroup.activeSelf)
		{
			_optionsGroup.SetActive(false);
			_stagesGroup.SetActive(true);
			
		}
		else
		{
			OpenMenuHideCurrent(_charactersSelectMenu);
		}
	}
}
