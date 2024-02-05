using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JoinServer : MonoBehaviour
{

	public void StartHost()
    {
		NetworkManager.Singleton.StartHost();
    }

	public void StartClient()
	{
		NetworkManager.Singleton.StartClient();
	}

	public void Disconnect()
	{
		NetworkManager.Singleton.DisconnectClient(NetworkManager.ServerClientId);

	}
}

