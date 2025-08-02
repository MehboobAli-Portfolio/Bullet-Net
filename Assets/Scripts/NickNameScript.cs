using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class NickNameScript : MonoBehaviourPunCallbacks
{
    public Text[] name;
    public Image[] healthBar;
    private GameObject waitingObject;
    private void Start()
    {
        for (int i = 0; i < name.Length; i++)
        {
            name[i].gameObject.SetActive(false);
            healthBar[i].gameObject.SetActive(false);
        }
        waitingObject = GameObject.Find("WaitingBG");
    }
    public void Leaving()
    {
        StartCoroutine("BackToLobby");
    }

    IEnumerator BackToLobby()
    {
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.LoadLevel("Lobby");
    }
    //This is for the waiting screen
    public void ReturnToLobby()
    {
        waitingObject.SetActive(false);
        roomExit();
    }
    void roomExit()
    {
        StartCoroutine("ToLobby");
    }
    IEnumerator ToLobby()
    {
        yield return new WaitForSeconds(0.5f);
        Cursor.visible = true;
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
