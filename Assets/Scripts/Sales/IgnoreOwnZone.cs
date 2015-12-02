using UnityEngine;
using System.Collections;

public class IgnoreOwnZone : MonoBehaviour
{

    public Transform Seller; 

	void Start ()
    {
		//Just have the seller NPC ignore its own collider
        Seller = Seller.GetComponent<Transform>();
        Physics.IgnoreCollision(Seller.GetComponent<Collider>(), GetComponent<Collider>());
	}	
	
}
