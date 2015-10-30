using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour {
	public float gravity;
	public float gravityRadius = 0;
	public float gravityRadiusFade = 0;
	public bool RadiiProportionateToScale = true;
	public float radiusProportion = 0.75f;
	public float radiusFadeProportion = 1.5f;
	public bool DEBUG = true;
	public Vector3 gravityUp;

	public void Attract(Transform body) {
		//Vector3 gravityUp;
		//Vector3 localUp;
		//Vector3 localForward;
		//Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;
		Transform t = body.transform;
		//Rigidbody r = body.GetComponent<Rigidbody>();
		//float shipDistance =(float) Vector3.Distance (transform.position, body.transform.position);

		gravityUp = t.position - transform.position;
		gravityUp.Normalize();

		gravityRadius = transform.localScale.x * 0.75f;
		gravityRadiusFade = transform.localScale.x * 1.5f;

//		if (shipDistance < gravityRadius) {
//			r.AddForce (gravityUp * gravity);
//		} else if (shipDistance > gravityRadius || shipDistance < gravityRadiusFade) {
//			r.AddForce(gravityUp*(gravity*(1-(shipDistance-gravityRadius)/(gravityRadiusFade-gravityRadius))));
//		}
		/*ADD CODE FOR ISGROUNDED HERE
		 * 
		 * if(body.grounded({
		 * 		r.drag = 0;
		 * }
		 * 
		 * 
		*/
//		if (RadiiProportionateToScale) {
//			gravityRadius = transform.localScale.x*radiusProportion;
//			gravityRadiusFade = transform.localScale.x*radiusFadeProportion;
//		}


		body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
		
		Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);

		///add fading gravity here
		/// while not grounded:
		/// subtract from gravityUp based on height from the ground
		//return gravityUp;
	}

} 