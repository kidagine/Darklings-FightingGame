using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkExtenderManager : SingletonNetwork<NetworkExtenderManager>
{
	[SerializeField] private TextMeshProUGUI _networkTypeText = default;
	private readonly List<GameObject> _connectedClients = new();


	public List<GameObject> SpawnConnectedClients(GameObject clientPrefab, Transform[] spawnPositions = default)
	{
		if (IsServer)
		{
			int i = 0;
			foreach (NetworkClient networkClient in NetworkManager.Singleton.ConnectedClientsList)
			{
				_connectedClients.Add(Instantiate(clientPrefab, spawnPositions[i].position, Quaternion.identity));
				_connectedClients[^1].GetComponent<NetworkObject>().SpawnAsPlayerObject(networkClient.ClientId);
				i++;
			}
			return _connectedClients;
		}
		return null;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			Host();
		}
		if (Input.GetKeyDown(KeyCode.F2))
		{
			Client();
		}
	}

	public void Host()
	{
		if (NetworkManager.Singleton.StartHost())
		{
			_networkTypeText.text = "Host";
			Debug.Log("Host started");
		}
		else
		{
			Debug.Log("Host error");
		}
	}

	public void Client()
	{
		if (NetworkManager.Singleton.StartClient())
		{
			_networkTypeText.text = "Client";
			Debug.Log("Client started");
		}
		else
		{
			Debug.Log("Client error");
		}
	}
}
