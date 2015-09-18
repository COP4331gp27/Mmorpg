using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed = 10.0f;
	public float jumpForce;
	private Rigidbody rb;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		movement = Camera.main.transform.TransformDirection(movement);
		movement.Normalize();
		rb.AddForce (movement * speed);

		//rb.AddTorque (movement * speed);

		//Show Velocity in Log
		Debug.Log ("Velocity = "+ rb.velocity.magnitude);
	}
}