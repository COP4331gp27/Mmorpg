using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sword : MonoBehaviour 
{

    private int damage = 10;
    private bool playerHas = false;
    public float zOffset = 1.0f;
    public float yOffset = 0.5f;
    public float xOffset = 0.5f;
    public float turnSpeed = 4.0f;
    //PhotonView photonView;

    private Vector3 offset;
    public Transform player;
    void Start()
    {
        //photonView = GetComponent<PhotonView>();
        //player = GetComponentInParent<Transform> ();
        offset = new Vector3(xOffset, yOffset, zOffset);
    }
    [PunRPC]
    void LateUpdate()
    {
        //While Holding Left Click Rotate weapon Around  
		if (!player.Equals (null)) {
            
			offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse X") * turnSpeed, Vector3.up) * offset;
			//Causes problems with clipping
			offset = Quaternion.AngleAxis (Input.GetAxis ("Mouse Y") * turnSpeed, Vector3.right) * offset;
			transform.forward = player.forward;
			transform.position = player.position + offset;
			transform.LookAt (player.position);
		}

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
        if (other.tag == "Player" && other.GetComponent<InventoryManager>().findSpecificItem("PlayerSword") == false)
        {
            
            ItemData swordItem = this.GetComponent<ItemData>();
            swordItem.damage = damage;
            
            //need to figure this out without using Find. It's expensive
            //Transform temp = other.transform.GetChild(1);
            //foreach (GameObject child in temp)
            //{
            //    if (child.ToString()
            //}
            Transform swordTransform = other.transform.GetChild(1).Find("PlayerSword");
            //Transform swordObject = sword.GetComponent<Transform>();
            //Debug.Log("WHAT AM I?!: " + swordTransform.ToString());
            Player p = other.transform.GetComponent<Player>();
            p.pickUpItem(swordItem);
            PlayerSword sword = swordTransform.GetComponent<PlayerSword>();
            sword.player = p.GetComponent<Transform>();
            swordTransform.gameObject.SetActive(true);
            
            
        }
    }

   
    
    //void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.collider.tag == "Player" && this.playerHas)
    //    {
    //        //Debug.Log("WHY AM I IN HERE?!");
    //        //Player p = collision.collider.GetComponent<Player>();
    //        //Player owner = this.GetComponent<Player>();
    //        //p.takeDamage(getDamage()+owner.getDamage());
    //        int myDamage = GetComponent<Player>().getDamage();
    //        collision.transform.GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, getDamage() + myDamage);
    //        //Debug.Log("Damaged player!" + "\tPlayer's Health = " + p.playerHealth);
    //    }
    //    else
    //    {

    //    }
    //}
}