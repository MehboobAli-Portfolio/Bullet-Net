using UnityEngine;
using Photon.Pun;

public class ButtonScript : MonoBehaviour
{
    private GameObject[] Players;
    private int myID;
    private GameObject panel;
    private void Start()
    {
        Cursor.visible = true; // Show the cursor
        panel = GameObject.Find("ChoosePanel");
    }

    public void SelectButton(int buttonNumber)
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].GetComponent<PhotonView>().IsMine)
            {
                myID = Players[i].GetComponent<PhotonView>().ViewID;
                break;
            }
        }
        GetComponent<PhotonView>().RPC("SelectedColor", RpcTarget.AllBuffered, buttonNumber, myID);
        panel.SetActive(false); // Hide the panel after selection
        Cursor.visible = false; // Hide the cursor after selection
    }
    [PunRPC]
    public void SelectedColor(int buttonNumber, int myID)
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Players.Length; i++)
        {
            var displayColor = Players[i].GetComponent<DisplayColor>();
            if (displayColor == null)
            {
                Debug.LogError($"Player {Players[i].name} does not have a DisplayColor component.");
                continue;
            }
            if (displayColor.viewID == null)
            {
                Debug.LogError($"DisplayColor.viewID is null on {Players[i].name}.");
                continue;
            }
            if (buttonNumber < 0 || buttonNumber >= displayColor.viewID.Length)
            {
                Debug.LogError($"buttonNumber {buttonNumber} is out of bounds for viewID array on {Players[i].name}.");
                continue;
            }
            displayColor.viewID[buttonNumber] = myID;
            displayColor.ChooseColor();
        }
        this.transform.gameObject.SetActive(false);
    }
}
