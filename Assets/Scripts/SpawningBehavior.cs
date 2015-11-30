using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawningBehavior : MonoBehaviour {

    void Awake()
    {
        
    }
	// Use this for initialization
	void Start () 
	{
        
	}

    void OnJoinedRoom()
    {
		if(PhotonNetwork.isMasterClient)
		SpawnEnemy ();
			
    }
	void OnJoinedLobby ()
	{
	}

	
	// Update is called once per frame
	void Update () {
	
	}
	[PunRPC]
	void SpawnEnemy()
	{
		
		GameObject enemySpawner = this.gameObject;
		GameObject enemy = PhotonNetwork.InstantiateSceneObject("Prefabs/Enemy", enemySpawner.transform.position, Quaternion.identity, 0, null);
  		Debug.Log ("Enemy instantiation ID" + enemy.GetComponent<PhotonView> ().instantiationId);
		enemy.transform.SetParent (enemySpawner.transform);
		enemy.SetActive (true);
	}


   
       
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
