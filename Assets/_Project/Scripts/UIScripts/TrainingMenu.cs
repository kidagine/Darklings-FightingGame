using Demonics.UI;
using TMPro;
using UnityEngine;

public class TrainingMenu : BaseMenu
{
	[SerializeField] private GameObject _p1 = default;
	[SerializeField] private GameObject _p2 = default;
	[SerializeField] private TextMeshProUGUI _framedataOneText = default;
	[SerializeField] private TextMeshProUGUI _framedataTwoText = default;


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

	public void SetDisplay(int value)
	{
		switch (value)
		{
			case 0:
				_p1.SetActive(false);
				_p2.SetActive(false);
				break;
			case 1:
				_p1.SetActive(true);
				_p2.SetActive(false);
				break;
			case 2:
				_p1.SetActive(false);
				_p2.SetActive(true);
				break;
			case 3:
				_p1.SetActive(true);
				_p2.SetActive(true);
				break;
		}
	}

	public void FramedataValue(bool isPlayerOne, int value, int recovery)
	{
		if (isPlayerOne)
		{
			if (_framedataOneText.gameObject.activeSelf)
			{
				_framedataOneText.text = $"{value}/{recovery}";
			}
		}
		else
		{
			if (_framedataTwoText.gameObject.activeSelf)
			{
				_framedataTwoText.text = $"{value}/{recovery}";
			}
		}
	}
}
