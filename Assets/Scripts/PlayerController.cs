using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed = 250.0f;
	public float jumpForce;
	public Rigidbody rb;
	public Camera cam;
	//Gets rigidbody of the player
	void Start ()
	{
        //cam = Instantiate(Resources.Load("Prefabs/MainCamera") as Camera);
        rb = this.transform.GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
		//moves left and right
		float moveHorizontal = Input.GetAxis ("Horizontal");
		//moves forward and back
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f,moveVertical);
		// makes the camera affect movement
		movement =cam.transform.TransformDirection(movement);
		movement.Normalize();
		//move 
		rb.AddForce (movement * speed);
        //Show Velocity in Log
        //Debug.Log ("Velocity = "+ rb.velocity.magnitude);
		//stop if going slower than .43
        if (!(Input.anyKey) && (rb.velocity.magnitude <= 0.43f))
        {
            rb.Sleep();
        }
    }
	//get speed
    public float getSpeed ()
    {
        return rb.velocity.magnitude;
    }
}