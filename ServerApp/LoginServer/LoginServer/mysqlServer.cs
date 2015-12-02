using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;


namespace LoginServer
{
	public class mysqlChecker 
	{
		MySql.Data.MySqlClient.MySqlConnection conn;
		string myConnectionString;
		string command;
		MySql.Data.MySqlClient.MySqlCommand mysqlCom;

		public mysqlChecker ()
		{
			myConnectionString = "server=mmo.ce1uqa5e0n15.us-west-2.rds.amazonaws.com;" +
				"uid=eric;pwd=climber01;database=users;port=3306";

			try
			{
				conn = new MySqlConnection ();
				conn.ConnectionString = myConnectionString;

			}
			catch(MySql.Data.MySqlClient.MySqlException ex)
			{
				Console.WriteLine(ex.ToString());
			}

		}


		public int checkDB(string name, string pass)
		{
			command = " SELECT password FROM usernames WHERE name='" + name + "';";
			mysqlCom = new MySqlCommand (command, conn);
			string correct;
			Console.WriteLine ("Sent command " + command);
			conn.Open ();

			MySqlDataReader reader = mysqlCom.ExecuteReader ();
			reader.Read ();

			correct = "";


			if (reader.HasRows) {
				correct = reader.GetString ("password");
			} 
			else
			{
				Console.WriteLine ("No user found");
				conn.Close ();
				return -2;
			}

			reader.Close ();
			conn.Close ();
			if (correct == pass) {
				Console.WriteLine ("passed");
				return 1;
			}
			else 
			{
				Console.WriteLine ("failed");
				return -1;
			}
		}

		public bool addUser(string name, string pass)
		{
			command = "SELECT name FROM usernames WHERE name='" + name + "';";
			mysqlCom = new MySqlCommand (command, conn);

			Console.WriteLine ("Sent command " + command);
			conn.Open ();

			MySqlDataReader reader = mysqlCom.ExecuteReader ();
			reader.Read ();


			if (reader.HasRows)
			{
				Console.WriteLine ("duplicate found");
				reader.Close ();
				conn.Close ();
				return false;
			}
			else 
			{
				reader.Close ();
				Console.WriteLine ("no duplicate found");
				command = "insert into usernames (name, password) values ('" + name + "', '" + pass + "');";

				mysqlCom = new MySqlCommand (command, conn);

				mysqlCom.ExecuteNonQuery ();

				Console.WriteLine ("Sent command " + command);

				conn.Close ();
				return true;
			}
		}
	}
}

