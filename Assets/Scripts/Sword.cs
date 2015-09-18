using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour 
{
	
	public float turnSpeed = 4.0f;
	
	//Distance to or from 
	public float zOffset = 1.0f;
	public float yOffset = 0.5f;
	public float xOffset = 0.5f;
	public Transform player;
	private Vector3 offset;
	
	void Start () 
	{
		offset = new Vector3(xOffset, yOffset, zOffset);
	}
	
	void LateUpdate()
	{
		//While Holding Left Click Rotate weapon Around 
		
		if (Input.GetMouseButton (0)) 
		{ 
			
			offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * turnSpeed, Vector3.up) * offset;
			//Causes problems with clipping
			offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse Y") * turnSpeed, Vector3.right) * offset;
		}
		transform.position = player.position + offset; 
		transform.LookAt (player.position);
		
	}
}