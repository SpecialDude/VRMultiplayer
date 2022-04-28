using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int sceneIndex;
    public int maxPlayer;
}


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject roomUI;
    /*/ Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }*/

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to the server");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }

    public void InitializeRoom( int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];


        // LOAD SCENE
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);
        /*
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("VR Room1", roomOptions, TypedLobby.Default);
        */

        // Create The Room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom(roomSettings.Name, roomOptions, TypedLobby.Default);
  
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room VR Room1");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New Player Joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        base.OnJoinedLobby();
        roomUI.SetActive(true);
    }
}
