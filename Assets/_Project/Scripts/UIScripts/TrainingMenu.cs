using Demonics.UI;
using UnityEngine;

public class TrainingMenu : BaseMenu
{
	[SerializeField] private GameObject _framedataText = default;
	public void SetHitboxes(int value)
	{
		if (value == 1)
		{
			TrainingSettings.ShowHitboxes = true;
		}
		else
		{
			TrainingSettings.ShowHitboxes = false;
		}
	}

	public void SetFramedata(int value)
	{
		if (value == 1)
		{
			_framedataText.SetActive(true);
		}
		else
		{
			_framedataText.SetActive(false);
		}
	}
}
