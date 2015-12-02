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

	static public void Send(MessageData msg
	                        ){
		Debug.Log("entering send");

		if (me.server == null) 
		{
			Debug.Log("no connection");
			return;
		}
		byte[] sendData = MessageData.toByteArray (msg);
		byte[] buffer = new byte[1];
		buffer [0] = (byte)sendData.Length;
		me.server.Send (buffer);
		me.server.Send (sendData);
 	}

	static public int  LoginCheck()
	{
		Debug.Log ("checking Log in");
		byte[] receivedBytes = new byte[512];		//this is the recieved message
		ArrayList buffer = new ArrayList ();		//we need a buffer to hold the values
		int read = me.server.Receive (receivedBytes);		//recieve the message into recieved bytes and return length

		for (int i = 0; i < read; i++) 
		{
			buffer.Add(receivedBytes[i]);		//read the recieved bytes into the buffer
		}
		while (buffer.Count > 0)				//while ther is still something in the buffer
		{
			int length = (byte) buffer[0];				//the value is the length of the next value
			if (length < buffer.Count)			//if the length is less than the remaining count
			{
				ArrayList thisMsgBytes = new ArrayList(buffer);							//make a new copy of the data
				thisMsgBytes.RemoveRange(length + 1, thisMsgBytes.Count - (length + 1));	//remove the data past length
				thisMsgBytes.RemoveRange(0, 1);												//remove the length value
				if (thisMsgBytes.Count != length)											//if we don't end up with the correct amount of bytes
				{
					Debug.Log("error reading response");
				}

				buffer.RemoveRange(0, length + 1);			//remove this value from the buffer
				byte[] readByte = (byte[])thisMsgBytes.ToArray(typeof(byte));		//turn the bytes from this section into an array as opposed to an arraylist
				MessageData response = MessageData.FromByteArray(readByte);			//this turns the readByte back into the message

				Debug.Log("returning " + response.type);

				return response.type;				//so this isn't great because it kills the buffer but we shoul,d only need one message of this type for each time

			}
		}
		return -1;
	}
}
