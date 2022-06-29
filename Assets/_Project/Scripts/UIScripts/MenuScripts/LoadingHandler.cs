using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour
{
	[SerializeField] private PlayerStats _characterOne = default;
	[SerializeField] private PlayerStats _characterTwo = default;
	[SerializeField] private TextMeshProUGUI _characterOneName = default;
	[SerializeField] private TextMeshProUGUI _characterTwoName = default;
	[SerializeField] private TextMeshProUGUI _stageName = default;
	[SerializeField] private TextMeshProUGUI _loadingProgressText = default;
	[SerializeField] private PlayerStatsSO[] _playerStats = default;
	[SerializeField] private GameObject[] _stages = default;


	void Start()
	{
		SetPlayersInfo();
		StartCoroutine(LoadingCoroutine());
	}

	private void SetPlayersInfo()
	{
		_stages[SceneSettings.StageIndex].SetActive(true);
		_characterOne.PlayerStatsSO = _playerStats[SceneSettings.PlayerOne];
		_characterTwo.PlayerStatsSO = _playerStats[SceneSettings.PlayerTwo];
		_characterOne.GetComponent<PlayerAnimator>().SetSpriteLibraryAsset(SceneSettings.ColorOne);
		_characterTwo.GetComponent<PlayerAnimator>().SetSpriteLibraryAsset(SceneSettings.ColorTwo);
		_characterOneName.text = _playerStats[SceneSettings.PlayerOne].characterName.ToString();
		_characterTwoName.text = _playerStats[SceneSettings.PlayerTwo].characterName.ToString();
		_stageName.text = _stages[SceneSettings.StageIndex].name.Substring(0, _stages[SceneSettings.StageIndex].name.IndexOf("_"));
	}

	IEnumerator LoadingCoroutine()
	{
		AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(3);
		loadingOperation.allowSceneActivation = false;
		while (!loadingOperation.isDone)
		{
			if (loadingOperation.progress >= 0.9f)
			{
				_loadingProgressText.text =  "99%";
				yield return new WaitForSeconds(3.0f);
				_loadingProgressText.text = "100%";
				loadingOperation.allowSceneActivation = true;
			}
			else
			{
				_loadingProgressText.text = Mathf.Floor(loadingOperation.progress * 100) + "%";
			}
			yield return null;
		}
	}
}
