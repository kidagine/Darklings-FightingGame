using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour
{
    [SerializeField] private PlayerUIRender _playerUIRenderOne = default;
    [SerializeField] private PlayerUIRender _playerUIRenderTwo = default;
    [SerializeField] private TextMeshProUGUI _characterOneName = default;
    [SerializeField] private TextMeshProUGUI _characterTwoName = default;
    [SerializeField] private TextMeshProUGUI _stageName = default;
    [SerializeField] private PlayerStatsSO[] _playerStats = default;
    [SerializeField] private GameObject[] _stages = default;


    void Start()
    {
        SetPlayersInfo();
        StartCoroutine(LoadingCoroutine());
    }

    private void SetPlayersInfo()
    {
        _playerUIRenderOne.PlayerStats = _playerStats[SceneSettings.PlayerOne];
        _playerUIRenderTwo.PlayerStats = _playerStats[SceneSettings.PlayerTwo];
        _playerUIRenderOne.SetSpriteLibraryAsset(SceneSettings.ColorOne);
        _playerUIRenderTwo.SetSpriteLibraryAsset(SceneSettings.ColorTwo);
        _playerUIRenderOne.SetAnimator(_playerStats[SceneSettings.PlayerOne]._animation);
        _playerUIRenderTwo.SetAnimator(_playerStats[SceneSettings.PlayerTwo]._animation);
        int stageColorIndex = SceneSettings.Bit1 ? 1 : 0;
        _stages[SceneSettings.StageIndex].transform.GetChild(stageColorIndex).gameObject.SetActive(true);
        _characterOneName.text = Regex.Replace(_playerStats[SceneSettings.PlayerOne].characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
        _characterTwoName.text = Regex.Replace(_playerStats[SceneSettings.PlayerTwo].characterName.ToString(), "([a-z])([A-Z])", "$1 $2");
        _stageName.text = _stages[SceneSettings.StageIndex].name.Substring(0, _stages[SceneSettings.StageIndex].name.IndexOf("_"));
    }

    IEnumerator LoadingCoroutine()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(2);
        loadingOperation.allowSceneActivation = false;
        while (!loadingOperation.isDone)
        {
            if (loadingOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2.5f);
                loadingOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
