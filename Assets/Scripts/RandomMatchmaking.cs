﻿using UnityEngine;
using System.Collections;
using System;

public class RandomMatchmaking : Photon.PunBehaviour {
	//This sets the room name
    private const string roomName = "Flast";
    private RoomInfo[] roomsList;
    private PhotonView myNetworkView;
	//This tells the game what prefab to spawn
    private GameObject player;
	private

    // Use this for initialization
    void Start()
    {   
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
		PhotonNetwork.automaticallySyncScene = true;
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        if (!PhotonNetwork.connected)
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        }
        else if (PhotonNetwork.room == null)
        {
            // Create Room
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                PhotonNetwork.CreateRoom(roomName);

            // Join Room
            if (roomsList != null)
            {
                for (int i = 0; i<roomsList.Length; i++)
            {
                    if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name))
                        PhotonNetwork.JoinRoom(roomsList[i].name);
                }
            }
        }
    }
	//See what rooms are in the lobby
    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }

  //  public override void OnJoinedLobby()
  // {
  //  }

	// This Throws error of not joining a room
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
    }
   //Spawns player when they join 
    public override void OnJoinedRoom()
	{
        SpawnPlayer();
        myNetworkView = player.GetComponent<PhotonView>();   
        //if (PhotonNetwork.isMasterClient)
        //{
        //    SpawnEnemy();
        //} 
        
    }
	//Instansiates player and enables
    [PunRPC]
    void SpawnPlayer()
    {
        player = PhotonNetwork.Instantiate("Prefabs/Player", new Vector3(UnityEngine.Random.Range(500.0f, 510.0f), 505.0f, UnityEngine.Random.Range(15.0f, -5.0f)), Quaternion.identity, 0);
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.enabled = true;
        Camera mainCamera = player.GetComponentInChildren<Camera>();
        mainCamera.enabled = true;
    }

    

}
