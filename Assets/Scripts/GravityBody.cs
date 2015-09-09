using UnityEngine;
using System.Collections;

public class GravityBody : MonoBehaviour {
	public GravityAttractor[] attractor;
	private Transform myTransform;
	private Rigidbody player;
	void Awake() {        
		attractor = Object.FindObjectsOfType<GravityAttractor>();
	}
	private bool attract = true;
	void Start () {
		//player = GetComponent<Rigidbody> ();
		GetComponent<Rigidbody>().WakeUp ();
		//GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		GetComponent<Rigidbody>().useGravity = false;
		myTransform = transform;
	}

	///add better collision handling
	/*
	 * void OnCollisionEnter(Collision c){
	 * if(c.gameObject.layer == 10){
	 * 		grounded++;
	 * }
	 * }
	 * 
	 * void OnCollisionExit(Collision c){
	 * if(c.gameObject.layer == 10 && grounded > 0){
	 * 		grounded--;
	 * }
	 * }
	 * 
	 */
	void FixedUpdate(){
		if (attract) {
			foreach(GravityAttractor planet in attractor){
				planet.Attract(this.myTransform);
			}
		} 
	}
	//RequireComponent(Rigidbody);
}