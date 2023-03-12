using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplaysMenu : BaseMenu
{
    [SerializeField] private BaseMenu _replayIncompatibleMenu = default;
    [SerializeField] private RectTransform _scrollView = default;
    [SerializeField] private RectTransform _replaysGroup = default;
    [SerializeField] private GameObject _replayPrefab = default;
    [SerializeField] private GameObject _noReplaysFound = default;
    private readonly List<ReplayCard> _replayCards = new();
    private ReplayCard _currentReplayCard;

    void Start()
    {
        _replaysGroup.anchoredPosition = new Vector2(0.0f, _replaysGroup.anchoredPosition.y);
        if (ReplayManager.Instance.ReplayAmount > 1)
        {
            for (int i = 0; i < ReplayManager.Instance.ReplayAmount - 1; i++)
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
        _currentReplayCard = _replayCards[index];
        if (_currentReplayCard.ReplayCardData.versionNumber.Trim() == ReplayManager.Instance.VersionNumber)
        {
            SceneSettings.IsOnline = false;
            SceneSettings.SceneSettingsDecide = true;
            SceneSettings.IsTrainingMode = false;
            SceneSettings.ReplayIndex = index;
            ReplayManager.Instance.SetReplay();
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("Replay version incompatible:" + _currentReplayCard.ReplayCardData.versionNumber.Trim() + "," + ReplayManager.Instance.VersionNumber);
            _currentReplayCard.GetComponent<Animator>().Rebind();
            _replayIncompatibleMenu.Show();
        }
    }

    public void CloseIncompatible()
    {
        _currentReplayCard.GetComponent<Button>().Select();
    }

    void OnEnable()
    {
        _scrollView.anchoredPosition = Vector2.zero;
        StartCoroutine(SetUpScrollViewCoroutine());
    }

    IEnumerator SetUpScrollViewCoroutine()
    {
        yield return null;
        _replaysGroup.anchoredPosition = new Vector2(0.0f, -(_replaysGroup.rect.height / 2.0f));
        _scrollView.anchoredPosition = Vector2.zero;
        if (_replayCards.Count > 0)
        {
            _replayCards[0].GetComponent<Button>().Select();
        }
    }
}
