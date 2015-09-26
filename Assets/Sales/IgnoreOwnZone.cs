using UnityEngine;
using System.Collections;

public class IgnoreOwnZone : MonoBehaviour
{

    public Transform Seller; 

	void Start ()
    {
        Seller = Seller.GetComponent<Transform>();
        Physics.IgnoreCollision(Seller.GetComponent<Collider>(), GetComponent<Collider>());
	}	
	
}
