using Demonics.UI;
using TMPro;
using UnityEngine;

public class TrainingMenu : BaseMenu
{
	[SerializeField] private TextMeshProUGUI _framedataText = default;
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
			_framedataText.gameObject.SetActive(true);
		}
		else
		{
			_framedataText.gameObject.SetActive(false);
		}
	}

	public void FramedataValue(int value)
	{
		_framedataText.text = value.ToString();
	}
}
