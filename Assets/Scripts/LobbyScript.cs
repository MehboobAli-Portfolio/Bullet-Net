using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScript : MonoBehaviourPunCallbacks
{
    TypedLobby KillCount = new TypedLobby("Kill Count", LobbyType.Default);
    TypedLobby teamBattle = new TypedLobby("Team Battle", LobbyType.Default);
    TypedLobby noRespawn = new TypedLobby("No Respawn", LobbyType.Default);

    public GameObject roomNumber;
    private void Start()
    {
        roomNumber.SetActive(false);
    }
    private string levelName = "";
    public void BackToMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }

    public void JoinKillCount()
    {
        levelName = "KillCount";
        PhotonNetwork.JoinLobby(KillCount);
    }

    public void JoinTeamBattle()
    {
        levelName = "TeamBattle";
        PhotonNetwork.JoinLobby(teamBattle);
    }

    public void JoinNoRespawn()
    {
        levelName = "NoRespawn";
        PhotonNetwork.JoinLobby(noRespawn);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Joined Random Failed , Creating a new Room");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;
        PhotonNetwork.CreateRoom("Arena" + Random.Range(1, 1000), roomOptions);
    }
    public override void OnJoinedRoom()
    {
        roomNumber.SetActive(true);
        Debug.Log("Joined Room");
        PhotonNetwork.LoadLevel(levelName);
    }
}
