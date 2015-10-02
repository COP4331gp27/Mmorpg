using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Player_NetworkSetup : NetworkBehaviour {
	[SerializeField]Camera cam;
	[SerializeField]AudioListener al;
	// Use this for initialization
	void Start () 
	{
	if(isLocalPlayer)
		{
			GetComponent<PlayerController>().enabled=true;
			cam.enabled=true;
			al.enabled=true;


		}
	
	}
}

