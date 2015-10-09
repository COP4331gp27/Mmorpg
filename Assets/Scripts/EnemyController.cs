using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private float accl = 2;
	private int maxSpeed = 10;
	public GameObject target;
	private Rigidbody rb;
	public Collider[] inRange;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		target = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target == null)
		Vector3 heading = target.transform.position - this.transform.position;
		rb.AddForce (heading * accl);
		if (this.rb.velocity.magnitude > maxSpeed) {
			this.rb.velocity = this.rb.velocity.normalized * maxSpeed;
		}

		Debug.Log ("Moving Towards: " + target.transform.position.ToString ());

	}

	public GameObject findTarget(){
		GameObject newTarget = null;
		inRange = Physics.OverlapSphere (transform.position, 100);		//find all coliders in range


	}
}
