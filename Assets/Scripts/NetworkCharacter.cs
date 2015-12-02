using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {
    private Vector3 otherPosition = Vector3.zero;
    private Quaternion otherRotation = Quaternion.identity;

    
  
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
			//Send health of this network player 
            stream.SendNext(GetComponent<Player>().getHealth());
			//Send position of this network character 
            stream.SendNext(transform.position);
			//Send rotation of this network character 
            stream.SendNext(transform.rotation);
        }
        else
        {
			//Recieve the position sent 
			this.transform.position=(Vector3) stream.ReceiveNext();
			//Recieve the rotation sent
			this.transform.rotation=(Quaternion) stream.ReceiveNext();
        }
    }

