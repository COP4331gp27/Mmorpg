using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;


public class Client : MonoBehaviour 
{
	public string serverAdress = "52.33.80.32";
	const int serverPort = 32211;
	public bool isConnected = false;

	static private Client me;
	private Socket server;

	void Awake()
	{
		server = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPAddress remoteIP = IPAddress.Parse (serverAdress);
		IPEndPoint remoteEnd = new IPEndPoint (remoteIP, serverPort);
		me = this;
		server.Connect (remoteEnd);
	}

	void Update()
	{
		if (isConnected != server.Connected) 
		{
			isConnected = server.Connected;
		}
	}

	void OnApplicationQuit()
	{
		server.Close ();
		server = null;
	}

	static public void Send(MessageData msg){
		if (me.server == null) {
			return;
		}
		byte[] sendData = MessageData.toByteArray (msg);
		byte[] buffer = new byte[1];
		buffer [0] = (byte)sendData.Length;
		me.server.Send (buffer);
		me.server.Send (sendData);
 	}
}
