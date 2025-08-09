using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

public class NickNameScript : MonoBehaviourPunCallbacks
{
    public Text[] name;
    public Image[] healthBar;
    private GameObject waitingObject;
    public GameObject displayPanel;
    public Text message;
    public int[] kills;
    private void Start()
    {
        displayPanel.SetActive(false);
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
    public void RunMessage(string win, string lose)
    {
        this.GetComponent<PhotonView>().RPC("DisplayMessage", RpcTarget.All, win, lose);
        UpdateKills(win);
    }
    public void UpdateKills(string win)
    {
        for (int i = 0; i < name.Length; i++)
        {
            if (name[i].text == win)
            {
                kills[i]++;
            }
        }
    }
    [PunRPC]
    public void DisplayMessage(string win, string lose)
    {
        displayPanel.SetActive(true);
        message.text = win + " Killed " + lose;
        StartCoroutine("HideMessage");
    }
    IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(3f);
        this.GetComponent<PhotonView>().RPC("HideMessageRPC", RpcTarget.All);
    }
    [PunRPC]
    public void HideMessageRPC()
    {
        displayPanel.SetActive(false);
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
