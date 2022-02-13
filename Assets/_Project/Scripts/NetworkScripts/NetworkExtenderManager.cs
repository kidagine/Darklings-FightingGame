using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkExtenderManager : SingletonNetwork<NetworkExtenderManager>
{
	private readonly List<GameObject> _connectedClients = new();


	public List<GameObject> SpawnConnectedClients(GameObject clientPrefab, Transform[] spawnPositions = default)
	{
		if (IsServer)
		{
			int i = 0;
			foreach (NetworkClient networkClient in NetworkManager.Singleton.ConnectedClientsList)
			{
				_connectedClients.Add(Instantiate(clientPrefab, spawnPositions[i].position, Quaternion.identity));
				_connectedClients[_connectedClients.Count - 1].GetComponent<NetworkObject>().SpawnAsPlayerObject(networkClient.ClientId);
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
			Debug.Log("Client started");
		}
		else
		{
			Debug.Log("Client error");
		}
	}
}
