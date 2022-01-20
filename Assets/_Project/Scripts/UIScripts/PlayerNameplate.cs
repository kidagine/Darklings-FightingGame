using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameplate : NetworkBehaviour
{
	[SerializeField] private TextMeshProUGUI _playerNameText = default;
	[SerializeField] private TextMeshProUGUI _playerReadyText = default;
	[SerializeField] private TextMeshProUGUI _playerAssistText = default;
	[SerializeField] private Image _playerPortrait = default;
	[SerializeField] private MultiDimensionalSprite[] _portraits = default;


	public void SetData(OnlinePlayerInfo onlinePlayerInfo)
	{
		gameObject.SetActive(true);
		_playerNameText.text = onlinePlayerInfo.PlayerName.ToString();
		_playerReadyText.text = onlinePlayerInfo.IsReady.ToString();
		_playerAssistText.text = ((char)(65 + onlinePlayerInfo.Assist)).ToString();
		_playerPortrait.sprite = _portraits[onlinePlayerInfo.Character].intArray[onlinePlayerInfo.Color];
	}

	void OnDisable()
	{
		gameObject.SetActive(false);
	}
}
