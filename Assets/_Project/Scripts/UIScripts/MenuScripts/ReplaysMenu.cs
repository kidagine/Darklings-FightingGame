using Demonics.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplaysMenu : BaseMenu
{
	[SerializeField] private RectTransform _scrollView = default;
	[SerializeField] private RectTransform _replaysGroup = default;
	[SerializeField] private GameObject _replayPrefab = default;
	[SerializeField] private GameObject _noReplaysFound = default;
	private readonly List<ReplayCard> _replayCards = new();
	

	void Start()
	{
		_replaysGroup.anchoredPosition = new Vector2(0.0f, _replaysGroup.anchoredPosition.y);
		if (ReplayManager.Instance.ReplayFilesAmount > 0)
		{
			for (int i = 0; i < ReplayManager.Instance.ReplayFilesAmount; i++)
			{
				ReplayCard replayCard = Instantiate(_replayPrefab, _replaysGroup).GetComponent<ReplayCard>();
				replayCard.SetData(ReplayManager.Instance.GetReplayData(i));
				int index = i;
				replayCard.GetComponent<BaseButton>()._onClickedAnimationEnd.AddListener(() => LoadReplayMatch(index));
				replayCard.GetComponent<BaseButton>()._scrollView = _scrollView;

				_replayCards.Add(replayCard);
			}
			_replayCards[0].GetComponent<BaseButton>()._scrollUpAmount = 0.0f;
			_replayCards[_replayCards.Count - 1].GetComponent<BaseButton>()._scrollDownAmount = 0.0f;
			_replayCards[0].GetComponent<Button>().Select();
		}
		else
		{
			_noReplaysFound.SetActive(true);
		}
		StartCoroutine(SetUpScrollViewCoroutine());
	}


	public void LoadReplayMatch(int index)
	{
		ReplayCard replayCard = _replayCards[index];
		if (replayCard.ReplayCardData.versionNumber.Trim() == ReplayManager.Instance.VersionNumber)
		{
			ReplayManager.Instance.SetReplay();
			SceneSettings.IsTrainingMode = false;
			SceneSettings.ReplayIndex = index;
			SceneManager.LoadScene(2);
		}
		else
		{
			replayCard.GetComponent<Animator>().Rebind();
			Debug.Log("Different version");
			//TODO
		}
	}

	void OnEnable()
	{
		_scrollView.anchoredPosition = Vector2.zero;
		if (_replayCards.Count > 0)
		{
			_replayCards[0].GetComponent<Button>().Select();
		}
	}

	IEnumerator SetUpScrollViewCoroutine()
	{
		yield return null;
		_replaysGroup.anchoredPosition = new Vector2(0.0f, -(_replaysGroup.rect.height / 2.0f));
		_scrollView.anchoredPosition = Vector2.zero;
	}
}
