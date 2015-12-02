using UnityEngine;
using System.Collections;

public class NetworkWeapon : MonoBehaviour {

    private Vector3 otherPosition = Vector3.zero;
    private Quaternion otherRotation = Quaternion.identity;
    private Rigidbody otherPlayer;

    void Start()
    {
		//gives sword to player who picks it up
        if (GetComponent<PhotonView>().isMine)
        {
            GetComponent<Sword>().enabled = true;
        }
        else
        {
			//shows other player swords
            GetComponent<OtherPlayerWeaponPosition>().enabled = true;
        }
        	
        otherPlayer = this.GetComponent<Rigidbody>();
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
			//Send position of this network character 
            stream.SendNext(transform.position);
			//Send rotation of this network weapon
            stream.SendNext(transform.rotation);
			//Send rigidbody of this network weapon
            stream.SendNext(transform.GetComponent<Rigidbody>());
            //stream.SendNext(health);
        }
        else
        {
			//Recieve the position sent 
            otherPosition = (Vector3)stream.ReceiveNext();
			//Recieve the rotation sent 
            otherRotation = (Quaternion)stream.ReceiveNext();
			//Recieve the Rigid Body sent
            otherPlayer = (Rigidbody)stream.ReceiveNext();
            
            //need to get everyone in the room's health and other shared stats
            //health = (int)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GetComponent<PhotonView>().isMine)
        {
           
            //do nothing, this is my character
        }
        else
        {
            
            transform.position = Vector3.Lerp(transform.position, otherPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, otherRotation, Time.deltaTime * 5);
            //transform.position = Vector3.Lerp(transform.position, otherPosition, 0.1f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, otherRotation, 0.1f);
            //Debug.Log("pos: " + transform.position);
        }
    }
}
