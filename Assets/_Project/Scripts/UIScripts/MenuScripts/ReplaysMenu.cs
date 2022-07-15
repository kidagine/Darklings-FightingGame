using Demonics.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplaysMenu : BaseMenu
{
	[SerializeField] private RectTransform _replaysGroup = default;
	[SerializeField] private GameObject _replayPrefab = default;
	private readonly List<ReplayCard> _replayCards = new();

	void Start()
	{
		_replaysGroup.anchoredPosition = new Vector2(0.0f, _replaysGroup.anchoredPosition.y);
		for (int i = 0; i < ReplayManager.Instance.ReplayFilesAmount; i++)
		{
			ReplayCard replayCard = Instantiate(_replayPrefab, _replaysGroup).GetComponent<ReplayCard>();
			replayCard.SetData(ReplayManager.Instance.GetReplayData(i));
			replayCard.GetComponent<BaseButton>()._onClickedAnimationEnd.AddListener(()=> LoadReplayMatch(0));
			_replayCards.Add(replayCard);
		}
		_replayCards[0].GetComponent<Button>().Select();
	}

	public void LoadReplayMatch(int index)
	{
		ReplayCard replayCard = _replayCards[index];
		SceneSettings.SceneSettingsDecide = true;
		SceneSettings.PlayerOne = replayCard.ReplayCardData.characterOne;
		SceneSettings.ColorOne = replayCard.ReplayCardData.colorOne;
		SceneSettings.AssistOne = replayCard.ReplayCardData.assistOne;
		SceneSettings.PlayerTwo = replayCard.ReplayCardData.characterTwo;
		SceneSettings.ColorTwo = replayCard.ReplayCardData.colorTwo;
		SceneSettings.AssistTwo = replayCard.ReplayCardData.assistTwo;
		SceneSettings.StageIndex = replayCard.ReplayCardData.stage;
		SceneSettings.MusicName = replayCard.ReplayCardData.musicName;
		SceneSettings.Bit1 = replayCard.ReplayCardData.bit1;
		SceneManager.LoadScene(2);
	}
}
