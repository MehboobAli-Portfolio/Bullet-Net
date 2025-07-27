using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class DisplayColor : MonoBehaviour
{
    public int[] buttonNumbers; // Array to hold button numbers
    public int[] viewID;
    public Color32[] colors; // Array to hold colors corresponding to button numbers
    private GameObject nameObject;
    private void Start()
    {
        nameObject = GameObject.Find("NicknameBG");

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
                nameObject.GetComponent<NickNameScript>().name[i].gameObject.SetActive(true);
                nameObject.GetComponent<NickNameScript>().healthBar[i].gameObject.SetActive(true);
                nameObject.GetComponent<NickNameScript>().name[i].text = this.GetComponent<PhotonView>().Owner.NickName;
            }
        }
    }
}
