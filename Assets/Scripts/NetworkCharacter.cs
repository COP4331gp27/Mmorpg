using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {
    private Vector3 otherPosition;
    private Quaternion otherRotation;
    

    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(health);
        }
        else
        {
            otherPosition = (Vector3)stream.ReceiveNext();
            otherRotation = (Quaternion)stream.ReceiveNext();
            //need to get everyone in the room's health and other shared stats
            //health = (int)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, otherPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, otherRotation, Time.deltaTime * 5);
        }
    }

    
}
