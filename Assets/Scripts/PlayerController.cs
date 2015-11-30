using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed = 250.0f;
	public float jumpForce;
	public Rigidbody rb;
	public Camera cam;

	void Start ()
	{
        //cam = Instantiate(Resources.Load("Prefabs/MainCamera") as Camera);
        rb = this.transform.GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f,moveVertical);
		movement =cam.transform.TransformDirection(movement);
		movement.Normalize();
		rb.AddForce (movement * speed);
        //Show Velocity in Log
        //Debug.Log ("Velocity = "+ rb.velocity.magnitude);

        if (!(Input.anyKey) && (rb.velocity.magnitude <= 0.43f))
        {
            rb.Sleep();
        }
    }

    public float getSpeed ()
    {
        return rb.velocity.magnitude;
    }
}