using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameplate : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameText = default;
    [SerializeField] private TextMeshProUGUI _playerReadyText = default;
    [SerializeField] private Image _playerPortrait = default;
    [SerializeField] private Sprite[] _portraits = default;


    public void SetData(OnlinePlayerInfo onlinePlayerInfo)
    {
        gameObject.SetActive(true);
        _playerNameText.text = onlinePlayerInfo.PlayerName.ToString();
        _playerReadyText.text = onlinePlayerInfo.IsReady.ToString();
        _playerPortrait.sprite = _portraits[onlinePlayerInfo.Portrait];
    }

    public void ResetToDefault()
    {
        _playerNameText.text = "Demon";
        _playerReadyText.text = "Waiting";
        _playerPortrait.sprite = _portraits[0];
    }
}
