using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Player_SyncPosition : NetworkBehaviour {

	[SyncVar]
	private Vector3 syncPos;

	[SerializeField] Rigidbody myRB;
	[SerializeField] float lerpRate=15;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
	void LerpPosition()
	{
		if(!isLocalPlayer)
		{
			myRB.position= Vector3.Lerp(myRB.position,syncPos,Time.deltaTime*lerpRate);
		}

}
	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		syncPos = pos;
	}
	[ClientCallback]
	void TransmitPosition()
	{
		if(isLocalPlayer)
		CmdProvidePositionToServer (myRB.position);
	}

}
