using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // Enable automatic scene synchronization
        PhotonNetwork.ConnectUsingSettings(); // Connect to Photon Master Server
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        PhotonNetwork.JoinRandomRoom(); // Attempt to join a random room
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room successfully");
        PhotonNetwork.LoadLevel("Floor layout"); // Load the game scene when joined
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("Arena 1"); // Create a new room if joining a random room fails
    }
}
