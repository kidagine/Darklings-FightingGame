using Demonics.UI;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnlineStartMenu : BaseMenu
{
	[SerializeField] private TextMeshProUGUI _roomID = default;
	[SerializeField] private TextMeshProUGUI _playerReadyText = default;
	[SerializeField] private BaseButton _readyButton = default;
	[SerializeField] private BaseButton _cancelButton = default;
	private readonly string _glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";


	private void OnEnable()
	{
		Host();
		_roomID.text = $"Room ID: {GenerateRoomID()}";
	}

	private void Host()
	{
		
	}

	public string GenerateRoomID()
	{
		string roomID = "";
		for (int i = 0; i < 12; i++)
		{
			roomID += _glyphs[Random.Range(0, _glyphs.Length)];
		}
		roomID = Regex.Replace(roomID.ToUpper(), ".{4}", "$0-");
		return roomID.Remove(roomID.Length - 1);
	}

	public void Ready()
	{
		_playerReadyText.text = "Ready";
		_readyButton.gameObject.SetActive(false);
		_cancelButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_cancelButton.gameObject);
	}

	public void Cancel()
	{
		_playerReadyText.text = "Waiting";
		_cancelButton.gameObject.SetActive(false);
		_readyButton.gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(_readyButton.gameObject);
	}
}
