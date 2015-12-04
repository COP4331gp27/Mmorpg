using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sword : MonoBehaviour 
{
    private GameObject importedCamera;
    private int damage = 10;
    private bool playerHas = false;
    public float zOffset = 1.0f;
    public float yOffset = 0.5f;
    public float xOffset = 0.5f;
    public float turnSpeed = 4.0f;

    private Vector3 offset;
    public Transform player;
    void Start()
    {
        //This is getting camera attached to player who owns this sword
        importedCamera = this.transform.parent.parent.transform.GetChild(0).gameObject; 
		//This is the distance 
        offset = new Vector3(xOffset, yOffset, zOffset);
    }
    [PunRPC]
    void LateUpdate()
    {
		//Make sure your player is their
		if (!player.Equals (null))
        {
			//Move sword based on horizontal user input
                offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * turnSpeed, Vector3.up) * offset;
			//Move sword based on vertical user input 
			    offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse Y") * turnSpeed, Vector3.right) * offset;
			//Set sword direction off of player direction
				transform.forward = player.forward;
			//Have the sword to move with player
				transform.position = player.position + offset;
			//Sword hilt points to player
				transform.LookAt (player.position);
		}
    }
	//Shows the sword is picked up
    public void setWeapon(bool pickedUp)
    {
        playerHas = pickedUp;
    }
	//Gets weapon damage
    public int getDamage()
    {
        return damage;
    }

    //Deals Damage to Player
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Debug.Log("WHY AM I IN HERE?!");
            PhotonView p = collision.collider.GetComponent<PhotonView>();
            Player owner = this.GetComponent<Player>();
            p.RPC("takeDamage", PhotonTargets.All, 10);
            //Debug.Log("Damaged player!" + "\tPlayer's Health = " + p.playerHealth);
        }
        else
        {

        }
    }
}