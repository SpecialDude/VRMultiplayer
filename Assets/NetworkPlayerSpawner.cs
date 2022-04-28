using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject avatarPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        avatarPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);

    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(avatarPrefab); 
    }
}
