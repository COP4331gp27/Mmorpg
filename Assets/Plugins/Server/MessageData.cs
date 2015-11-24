/*
 * Created using mon Develop
 * Created by Eric Homberger
 */

using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class MessageData{
	public int type = 0;
	public string stringData = "";

	public static MessageData FromByteArray(byte[] input){
		MemoryStream memStream = new MemoryStream (input);
		BinaryFormatter form = new BinaryFormatter ();
		MessageData me = new MessageData ();

		me.type = (int)form.Deserialize (memStream);
		me.stringData = (string)form.Deserialize (memStream);

		if (me.stringData == "") {
			me.type = 999;			//error code
			me.stringData = "nothing included";
		}

		return me;
	}

	public static byte[] toByteArray(MessageData msg){
		MemoryStream memStream = new MemoryStream ();
		BinaryFormatter form = new BinaryFormatter ();

		form.Serialize (memStream, msg.type);
		form.Serialize (memStream, msg.stringData);

		return memStream.ToArray ();
	}
}
