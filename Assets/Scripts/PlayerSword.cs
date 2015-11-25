using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSword : MonoBehaviour
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
        if (!player.Equals(null))
        {

            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            //Causes problems with clipping
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset;
            transform.forward = player.forward;
            transform.position = player.position + offset;
            transform.LookAt(player.position);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && this.playerHas)
        {
            //Debug.Log("WHY AM I IN HERE?!");
            //Player p = collision.collider.GetComponent<Player>();
            //Player owner = this.GetComponent<Player>();
            //p.takeDamage(getDamage()+owner.getDamage());
            int myDamage = GetComponent<Player>().getDamage();
            collision.transform.GetComponent<PhotonView>().RPC("takeDamage", PhotonTargets.All, getDamage() + myDamage);
            //Debug.Log("Damaged player!" + "\tPlayer's Health = " + p.playerHealth);
        }
        else
        {

        }
    }
}