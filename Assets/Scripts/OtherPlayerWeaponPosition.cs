using UnityEngine;
using System.Collections;

public class OtherPlayerWeaponPosition : MonoBehaviour {

    public float zOffset = 1.0f;
    public float yOffset = 0.5f;
    public float xOffset = 0.5f;
    public float turnSpeed = 4.0f;

    private Vector3 offset;
    public Transform player;
    void Start()
    {
        
        //player = GetComponentInParent<Transform> ();
        offset = new Vector3(xOffset, yOffset, zOffset);
    }
}
