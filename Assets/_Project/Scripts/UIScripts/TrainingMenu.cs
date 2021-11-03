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

	public void SetSlowdown(int value)
	{
		switch (value)
		{
			case 0:
				GameManager.Instance.GameSpeed = 1.0f;
				break;
			case 1:
				GameManager.Instance.GameSpeed = 0.75f;
				break;
			case 2:
				GameManager.Instance.GameSpeed = 0.5f;
				break;
			case 3:
				GameManager.Instance.GameSpeed = 0.25f;
				break;
			case 4:
				GameManager.Instance.GameSpeed = 0.10f;
				break;
		}
	}

	public void FramedataValue(int value, int recovery)
	{
		if (_framedataText.gameObject.activeSelf)
		{
			_framedataText.text = $"{value}/{recovery}";
		}
	}
}
