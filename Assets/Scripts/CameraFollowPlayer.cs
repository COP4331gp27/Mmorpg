using UnityEngine;
using System.Collections;


public class CameraFollowPlayer : MonoBehaviour
{
	Transform target;
	
	void Update () {
		transform.LookAt (target);
//		transform.position.x = target.position.x;
//		transform.position.y = target.position.y;
		transform.rotation = target.rotation;
	}

}