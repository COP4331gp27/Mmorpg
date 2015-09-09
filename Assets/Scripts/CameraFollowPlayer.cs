using UnityEngine;
using System.Collections;


public class CameraFollowPlayer : MonoBehaviour
{
	public Transform target;
	Vector3 cameraVector;
	Quaternion aboveBall;

	void OnStart(){
		cameraVector = transform.position+target.position;
		//aboveBall = new Quaternion (transform.position.x, transform.position.y, transform.position.z, 20.0f);
	}

	void Update () {
		//transform.position = target.position - cameraVector;
		transform.LookAt (target.position);
		transform.position = target.position + cameraVector;
//		transform.position.x = target.position.x;
//		transform.position.y = target.position.y;
		//transform.rotation = target.rotation;
	}

}