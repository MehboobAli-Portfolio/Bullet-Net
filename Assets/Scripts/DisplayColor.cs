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
    public AudioClip[] gunshotSounds; // Array to hold gunshot sounds
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
    if (this.GetComponent<Animator>().GetBool("Hit"))
        {
            StartCoroutine(HitEffect());
        }
    }
    public void DeliverDamage(string shooterName,string name, float damageAmts)
    {
        GetComponent<PhotonView>().RPC("GunDamage", RpcTarget.All, shooterName, name, damageAmts);
    }
    [PunRPC]
    public void GunDamage(string shooterName, string name, float damageAmts)
    {
        for (int i = 0; i < nameObjects.GetComponent<NickNameScript>().name.Length; i++)
        {
            if (nameObjects.GetComponent<NickNameScript>().name[i].text == name)
            {
                if (nameObjects.GetComponent<NickNameScript>().healthBar[i].gameObject.GetComponent<Image>().fillAmount > 0.1f)
                {
                    this.GetComponent<Animator>().SetBool("Hit", true);
                    nameObjects.GetComponent<NickNameScript>().healthBar[i].gameObject.GetComponent<Image>().fillAmount -= damageAmts;
                }
                else
                {
                    nameObjects.GetComponent<NickNameScript>().healthBar[i].gameObject.GetComponent<Image>().fillAmount = 0f;
                    this.GetComponent<Animator>().SetBool("Death", true);
                    this.gameObject.GetComponent<changeWeapon>().isDeath = true;
                    this.gameObject.GetComponent<PlayerMovement>().enabled = true;
                    this.gameObject.GetComponentInChildren<AimLookAtRef>().isDeath = true;
                    nameObjects.GetComponent<NickNameScript>().RunMessage(shooterName, name);
                    this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                }
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
    public void PalyGunshot(string name, int weaponNumber)
    {
        GetComponent<PhotonView>().RPC("PlaySound", RpcTarget.All, name, weaponNumber);
    }
    [PunRPC]
    public void PlaySound(string name, int weaponNumber)
    {
        for (int i = 0; i < nameObjects.GetComponent<NickNameScript>().name.Length; i++)
        {
            if (nameObjects.GetComponent<NickNameScript>().name[i].text == name)
            {
                GetComponent<AudioSource>().clip = gunshotSounds[weaponNumber];
                GetComponent<AudioSource>().Play();
            }
        }
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
    IEnumerator HitEffect()
    {
        yield return new WaitForSeconds(0.03f);
        this.GetComponent<Animator>().SetBool("Hit", false);
    }
}
