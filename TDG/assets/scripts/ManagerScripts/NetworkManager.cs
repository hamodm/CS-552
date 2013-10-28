using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public int numberOfConnections = 32;
	public int port = 22222;
	public bool useNat = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void StartServer()
	{
		Network.InitializeServer(numberOfConnections,port,useNat);
	}
	void ConnectToServer(ConnectionInfo info)
	{
		NetworkConnectionError error = Network.Connect(info.ipAddress,info.port);
		
		if(error == NetworkConnectionError.NoError)
		{
			SendMessage("GoToNextScene");
		}
	}
	void OnPlayerConnected(NetworkPlayer player)
	{
		print ("Player Connected: "+player.guid);
	}
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		Network.RemoveRPCs(player);
		Network.DestroyPlayerObjects(player);
	}
	void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		SendMessage("GoToNextScene");
		DestroyImmediate(gameObject);
	}
}
public struct ConnectionInfo
{
	public string ipAddress;
	public int port;
}
