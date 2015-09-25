using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {

    private int damage = 20;
    private bool playerHas = false;
    public float zOffset = 1.0f;
    public float yOffset = 0.5f;
    public float xOffset = 0.5f;
    public float turnSpeed = 2.0f;

    private Vector3 offset;
    public Transform player;
    void Start()
    {

        //player = GetComponentInParent<Transform> ();
        offset = new Vector3(xOffset, yOffset, zOffset);
    }

    void LateUpdate()
    {
        //While Holding Left Click Rotate weapon Around  

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        //Causes problems with clipping
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset;
        transform.forward = player.forward;
        transform.position = player.position + offset;
        transform.LookAt(player.position);


    }
    public void setWeapon(bool pickedUp)
    {
        playerHas = pickedUp;
    }

    public int getDamage()
    {
        return damage;
    }

    void OnTriggerEnter(Collider other)
    {
        //USING TRIGGERS FOR NON-COMBAT
        if(other.tag == "Player")
        {
            setWeapon(true);
        }
    }

}
