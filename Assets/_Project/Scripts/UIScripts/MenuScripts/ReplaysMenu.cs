using Demonics.UI;
using UnityEngine;

public class ReplaysMenu : BaseMenu
{
	[SerializeField] private RectTransform _replaysGroup = default;
	[SerializeField] private GameObject _replayPrefab = default;


	void Start()
	{
		_replaysGroup.anchoredPosition = new Vector2(0.0f, _replaysGroup.anchoredPosition.y);
		for (int i = 0; i < ReplayManager.Instance.ReplayFilesAmount; i++)
		{
			ReplayCard replayCard = Instantiate(_replayPrefab, _replaysGroup).GetComponent<ReplayCard>();
			replayCard.SetData(ReplayManager.Instance.GetReplayData(i));
		}
	}
}
