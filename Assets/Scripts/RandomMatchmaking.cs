﻿using UnityEngine;
using System.Collections;
using System;

public class RandomMatchmaking : Photon.PunBehaviour {

    private const string roomName = "Flast";
    private RoomInfo[] roomsList;

    // Use this for initialization
    void Start()
    {   
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
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

    void OnReceivedRoomListUpdate()
    {
        roomsList = PhotonNetwork.GetRoomList();
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        //PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            GameObject enemySpawner = GameObject.Find("Enemies");

            GameObject enemy = PhotonNetwork.Instantiate("Prefabs/Enemy", enemySpawner.transform.position, Quaternion.identity, 0);
            enemy.transform.SetParent(enemySpawner.transform);
            enemy.SetActive(true);
        }        
        GameObject player = PhotonNetwork.Instantiate("Prefabs/Player", new Vector3(UnityEngine.Random.Range(500.0f, 510.0f), 505.0f, UnityEngine.Random.Range(15.0f, -5.0f)), Quaternion.identity, 0);
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.enabled = true;
        Camera mainCamera = player.GetComponentInChildren<Camera>();
        mainCamera.enabled = true;
    }

}
