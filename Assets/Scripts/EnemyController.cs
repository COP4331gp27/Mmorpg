using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private float accl = 5;
	private int maxSpeed = 20;
	public GameObject target;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 heading = target.transform.position - this.transform.position;
		rb.AddForce (heading * accl);
		if (this.rb.velocity.magnitude > maxSpeed) {
			this.rb.velocity = this.rb.velocity.normalized * maxSpeed;
		}

		Debug.Log ("Moving Towards: " + target.transform.position.ToString ());

	}
}
