using UnityEngine;

public class PlayerGravityBody : FauxGravityBody
{
	/*
	 * Class members
	 */
	public float m_MaxSpeed = 4.5f;
	public float m_Force = 8.0f;
	public float m_JumpSpeed = 5.0f;
	private bool m_Jumping = false;
	
	
	/*
	 *  Public horizontal force accessor
	 */
	public float HorizontalForce
	{
		get
		{
			// Return the calculated horizontal force
			return Input.GetAxis("Horizontal") * this.m_Force;
		}
	}
	
	
	/*
	 * Public vertical force accessor
	 */
	public float VerticalForce
	{
		get
		{
			// Return the caluclated vertical force
			return Input.GetAxis("Vertical") * this.m_Force;
		}
	}
	
	
	/*
	 * Private update class method
	 */
	private void Update()
	{
		// Check if jump button was pressed and ridig body is grounded
		if(Input.GetButtonDown("Jump") == true && this.Grounded >= 1)
		{
			// Set jumping member value;
			this.m_Jumping = true;
		}
	}
	
	
	/*
	 * Private fixed update class method
	 */
	private void FixedUpdate()
	{
		// Check if attractor member is set
		if(this.m_Attractor != null)
		{
			// Apply attract to object
			this.m_Attractor.Attract(this);
		}
		
		// Check if rigid body velocity is within limits and grounded
		if(this.GetComponent<Rigidbody>().velocity.magnitude < this.m_MaxSpeed && this.Grounded >= 1)
		{
			// Apply forces to rigid body
			this.GetComponent<Rigidbody>().AddForce(this.transform.rotation * Vector3.forward * this.VerticalForce);
			this.GetComponent<Rigidbody>().AddForce(this.transform.rotation * Vector3.right * this.HorizontalForce);
		}
		
		// Check the state of jumping member
		if(this.m_Jumping == true)
		{
			// Apply jump forces
			this.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity + (this.GetComponent<Rigidbody>().transform.up * this.m_JumpSpeed);
			
			// Set jumping member value
			this.m_Jumping = false;
		}
	}
}
