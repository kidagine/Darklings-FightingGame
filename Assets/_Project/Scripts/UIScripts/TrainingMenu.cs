using Demonics.UI;

public class TrainingMenu : BaseMenu
{
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

	}
}
