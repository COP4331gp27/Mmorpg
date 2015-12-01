using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {
    private Vector3 otherPosition = Vector3.zero;
    private Quaternion otherRotation = Quaternion.identity;

    
    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(GetComponent<Player>().getHealth());
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(transform.GetComponent<Rigidbody>());
            //stream.SendNext(health);
        }
        else
        {
			this.transform.position=(Vector3) stream.ReceiveNext();
			this.transform.rotation=(Quaternion) stream.ReceiveNext();
            //otherPosition = (Vector3)stream.ReceiveNext();
            //otherRotation = (Quaternion)stream.ReceiveNext();
            //otherPlayer = (Rigidbody)stream.ReceiveNext();
            //if (otherPlayer.isKinematic == false)
            //{
            //    otherPlayer.isKinematic = true;
            //}
            //need to get everyone in the room's health and other shared stats
            //health = (int)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (photonView.isMine)
        //{
        //    do nothing, this is my character
        //}
        //else
        //{
        //    transform.position = Vector3.Lerp(transform.position, otherPosition, Time.deltaTime * 5);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, otherRotation, Time.deltaTime * 5);
            //transform.position = Vector3.Lerp(transform.position, otherPosition, 0.1f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, otherRotation, 0.1f);
            //Debug.Log("pos: " + transform.position);
        //}
    }
    
}
