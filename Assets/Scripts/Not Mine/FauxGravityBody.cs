using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class FauxGravityBody : MonoBehaviour
{
	/*
	 * Class members
	 */
	public FauxGravityAttractor m_Attractor = null;
	private int m_Grounded = 0;
	
	
	/*
	 * Public grounded member accessor
	 */
	public int Grounded
	{
		get
		{
			// Return value of grounded member
			return this.m_Grounded;
		}
	}
	
	
	/*
	 * Private start class method
	 */
	private void Awake()
	{
		// Set needed rigid body values
		this.GetComponent<Rigidbody>().WakeUp();
		this.GetComponent<Rigidbody>().useGravity = false;
		this.GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	
	/*
	 * Private on collision enter class method
	 */
	private void OnCollisionEnter(Collision c)
	{
		// Check the tag of collided object
		if(c.gameObject.tag == "WorldObject")
		{
			// Increase grounded member
			this.m_Grounded++;
		}
	}
	
	
	/*
	 * Private on collision exit class method
	 */
	private void OnCollisionExit(Collision c)
	{
		// Check the tag of collided object and grounded member value
		if(c.gameObject.tag == "WorldObject" && this.m_Grounded > 0)
		{
			// Decrease grounded member
			this.m_Grounded--;
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
	}
}
