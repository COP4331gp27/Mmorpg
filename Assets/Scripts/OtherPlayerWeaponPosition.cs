using UnityEngine;
using System.Collections;
//this sets the position of the other player's weapon
//this was needed because the weapon script is disabled on the other player
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
