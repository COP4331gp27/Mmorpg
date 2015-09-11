using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float moveSpeed = 10;
	public Vector3 moveDir;
	private Rigidbody m_Rigidbody;
	[SerializeField] private float m_MaxAngularVelocity = 25;
	[SerializeField] private float m_JumpPower = 2; // The force added to the ball when it jumps.
	public bool jump;
	private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
	private GravityAttractor planet;
	private void Start()
	{

		m_Rigidbody = GetComponent<Rigidbody>();
		// Set the maximum angular velocity.
		GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
	}

	void Update () {
		moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
	}
	
	void FixedUpdate() {
		planet = FindObjectOfType<GravityAttractor>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Rigidbody.MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime); 
//		m_Rigidbody.AddForce(moveDir*moveSpeed);
//		m_Rigidbody.AddTorque(new Vector3(moveDir.z, 0, -moveDir.x)*moveSpeed);


		// If on the ground and jump is pressed...

		///make custom jump that obeys new gravity
		///check if on the ground with raycaster
		/// if on the ground, add force opposite move direction OR add raycast in jump arc
		/// should depend on jump power and movespeed

		if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
		{
			float gravityNum = planet.gravity;
			// ... add force in upwards.
			m_Rigidbody.AddForce(moveDir*m_JumpPower*((100-gravityNum)/1000), ForceMode.Impulse);
		}
	}
}