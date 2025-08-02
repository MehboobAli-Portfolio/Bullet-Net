using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections;
public class DisplayColor : MonoBehaviourPunCallbacks
{
    public int[] buttonNumbers; // Array to hold button numbers
    public int[] viewID;
    public Color32[] colors; // Array to hold colors corresponding to button numbers
    private GameObject nameObjects;
    private GameObject waitForPlayers;
    private void Start()
    {
        nameObjects = GameObject.Find("NicknameBG");
        waitForPlayers = GameObject.Find("WaitingBG");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GetComponent<PhotonView>().IsMine && waitForPlayers.activeInHierarchy == false)
            {
                RemoveData();
                RoomExit();
            }
        }
    }
    void RemoveData()
    {
        GetComponent<PhotonView>().RPC("RemoveMe", RpcTarget.AllBuffered);
    }
    void RoomExit()
    {
        StartCoroutine(GetReadyToLeave());
    }
    public void ChooseColor()
    {
        GetComponent<PhotonView>().RPC("AssignColors", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void AssignColors()
    {
        for (int i = 0; i < viewID.Length; i++)
        {
            if (this.GetComponent<PhotonView>().ViewID == viewID[i])
            {
                this.transform.GetChild(1).GetComponent<Renderer>().material.color = colors[i];
                nameObjects.GetComponent<NickNameScript>().name[i].gameObject.SetActive(true);
                nameObjects.GetComponent<NickNameScript>().healthBar[i].gameObject.SetActive(true);
                nameObjects.GetComponent<NickNameScript>().name[i].text = this.GetComponent<PhotonView>().Owner.NickName;
            }
        }
    }
    [PunRPC]
    public void RemoveMe()
    {
        for (int i = 0; i < nameObjects.gameObject.GetComponent<NickNameScript>().name.Length; i++)
        {
            if (this.GetComponent<PhotonView>().Owner.NickName == nameObjects.GetComponent<NickNameScript>().name[i].text)
            {
                nameObjects.GetComponent<NickNameScript>().name[i].gameObject.SetActive(false);
                nameObjects.GetComponent<NickNameScript>().healthBar[i].gameObject.SetActive(false);
            }
        }
    }
    IEnumerator GetReadyToLeave()
    {
        yield return new WaitForSeconds(1f);
        nameObjects.GetComponent<NickNameScript>().Leaving();
        Cursor.visible = true;
        PhotonNetwork.LeaveRoom();
    }
}
