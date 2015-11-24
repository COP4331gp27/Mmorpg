

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MySql.Data.MySqlClient;

namespace loginServer
{
	class loginServer
	{
		static loginServer me;

		private Socket serverSocket;
		//mysql connection string

		ArrayList _connections = new ArrayList ();
		ArrayList _buffer = new ArrayList ();
		ArrayList _byteBuffer = new ArrayList ();

		public static void Main (string[] args)
		{
			loginServer inst = new loginServer ();
			inst.setUpServer ();

			while (true) 
			{
				inst.holdListening();
			}

			Console.ReadLine ();
		}

		private void setUpServer()
		{
			Console.WriteLine ("setting up");
			serverSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ipLocal = new IPEndPoint (IPAddress.Any, 32211);

			serverSocket.Bind (ipLocal);
			serverSocket.Listen (100);
			me = this;
			Console.WriteLine ("server started on " + ipLocal.ToString ());
		}

		private void holdListening()
		{
			ArrayList listening = new ArrayList ();
			listening.Add (serverSocket);
			Socket.Select (listening, null, null, 1000);

			for (int i = 0; i < listening.Count; i++) 
			{
				Socket newSocket = ((Socket) listening[i]).Accept();

				_connections.Add(newSocket);
				_byteBuffer.Add(new ArrayList());
				Console.WriteLine("new connection from " + newSocket.LocalEndPoint.ToString());
			}
		}

		private void readData()
		{
			if (_connections.Count > 0) 
			{
				ArrayList cons = new ArrayList(_connections);
				Socket.Select(cons, null, null, 1000);
				foreach(Socket socket in cons)
				{
					byte[] receivedBytes = new byte[512];
					ArrayList buffer = (ArrayList) _byteBuffer[_connections.IndexOf(socket)];
					int read = socket.Receive(receivedBytes);

					for (int i = 0; i < read; i++)
					{
						buffer.Add(receivedBytes[i]);
					}
					while (buffer.Count > 0)
					{
						int length = (byte) buffer[0];
						if (length < buffer.Count)
						{
							ArrayList thisMsgBytes = new ArrayList(buffer);
							thisMsgBytes.RemoveRange(length + 1, thisMsgBytes.Count - (length + 1));
							thisMsgBytes.RemoveRange(0,1);
							if (thisMsgBytes.Count != length)
							{
								Console.WriteLine("error reading message");
							}
							buffer.RemoveRange(0, length + 1);
							byte[] readBytes = (byte[]) thisMsgBytes.ToArray(typeof(byte));
							MessageData readMsg = MessageData.FromByteArray(readBytes);
							buffer.Add(readMsg);
							Console.WriteLine("message of type {0}: {1} ", readMsg.type, readMsg.stringData);
							//TODO: ahndle recieved packet

							if (me != this){
								Console.WriteLine("This error makes no sense");
							}
						}
					}
				}
			}
		}
	}
}
