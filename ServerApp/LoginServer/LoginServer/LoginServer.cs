

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MySql.Data.MySqlClient;

namespace LoginServer
{
	class LoginServer
	{
		static LoginServer me;

		private Socket serverSocket;
		//mysql connection string

		ArrayList _connections = new ArrayList ();
		ArrayList _buffer = new ArrayList ();
		ArrayList _byteBuffer = new ArrayList ();

		static mysqlChecker dataBase;

		public static void Main (string[] args)
		{
			LoginServer inst = new LoginServer ();
			inst.setUpServer ();


			Console.WriteLine ("creating mysql");
			dataBase = new mysqlChecker ();
			/*Console.WriteLine ("Checking");
			dataBase.checkDB ("eric", "homberger");

			dataBase.addUser ("test", "case");
			dataBase.addUser ("eric", "case");
			dataBase.checkDB ("test", "case");*/

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
			readData ();
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
							_buffer.Add(readMsg);
							Console.WriteLine("message of type {0}: {1} ", readMsg.type, readMsg.stringData);
							HandleReceivedPacket(readMsg, socket);

							if (me != this){
								Console.WriteLine("This error makes no sense");
							}
						}
					}
				}
			}
		}

		private void HandleReceivedPacket(MessageData data, Socket Sender)
		{
			switch (data.type) 
			{
			case 0:
				Console.WriteLine("this is login request");
				HandleLoginRequest(Sender, data.stringData);
				break;
			case 1:
				Console.WriteLine ("this is an account creation request");
				HandleCreateRequest (Sender, data.stringData);
				break;

			}
		}

		private void HandleCreateRequest(Socket sender, string request){
			MessageData response = new MessageData ();
			response.stringData = "create";
			string[] elem = request.Split ('|');
			Console.WriteLine (elem [0] + " " + elem [1]);

			response.type = dataBase.addUser (elem [0], elem [1]);

			respond (sender, response);
		}

		private void HandleLoginRequest(Socket sender, string request)
		{
			MessageData response = new MessageData ();
			response.stringData = "login";
			string[] elem = request.Split ('|');
			Console.WriteLine (elem [0] + " " + elem [1]);


			response.type = dataBase.checkDB (elem [0], elem [1]);
/*			if (dataBase.checkDB(elem[0], elem[1]) == 1)
			{
				response.type = 1;
			}
			else 
			{
				response.type = -1;
			}*/
			Console.WriteLine ("Responding with " + response.type);
			respond (sender, response);
		}

		private void respond(Socket sender, MessageData response)
		{
			byte[] sendData = MessageData.toByteArray (response);
			byte[] buffer = new byte[1];
			buffer [0] = (byte)sendData.Length;
			sender.Send (buffer);
			sender.Send (sendData);

		}
	}
}
