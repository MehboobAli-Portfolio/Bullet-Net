using UnityEngine;
using Photon.Pun;
using System.Collections;
public class WeaponPickup : MonoBehaviour
{
    private AudioSource audioPlayer;
    public float respawnTime = 5f;
    public int weaponType = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioPlayer=GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<PhotonView>().RPC("PlayPickupAudio", RpcTarget.All);
            this.GetComponent<PhotonView>().RPC("TurnOff", RpcTarget.All);
        }
    }
    [PunRPC]
    void PlayPickupAudio()
    {
        audioPlayer.Play();
    }
    [PunRPC]
    void TurnOff()
    {
        if (weaponType == 1)
        {
            this.transform.gameObject.GetComponent<Collider>().enabled = false;
            this.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;  
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.gameObject.GetComponent<Collider>().enabled = false;
        }
        StartCoroutine(WaitToRespawn());
    }

    private IEnumerator WaitToRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        this.GetComponent<PhotonView>().RPC("TurnOn", RpcTarget.All);
    }
    [PunRPC]
    void TurnOn()
    {
        if (weaponType == 1)
        {
            this.transform.gameObject.GetComponent<Collider>().enabled = true;
            this.transform.gameObject.GetComponent<MeshRenderer>().enabled = true;  
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}
