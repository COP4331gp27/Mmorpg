using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	
	public float turnSpeed = 4.0f;

	//Distance to or from 
	public float zOffset;
	public float yOffset;

	public Transform player;
	private Vector3 offset;

	void Start () 
	{
		offset = new Vector3(0, yOffset, zOffset);
	}
	
	void LateUpdate()
	{
		//While Holding Right Click Rotate Camera Around 

		if (Input.GetMouseButton (1)) { 

			offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * turnSpeed, Vector3.up) * offset;
			//Causes problems with clipping
			offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse Y") * turnSpeed, Vector3.right) * offset;
		}
		transform.position = player.position + offset; 
		transform.LookAt (player.position);

	}
}