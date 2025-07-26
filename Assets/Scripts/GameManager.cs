using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviourPunCallbacks
{
    public InputField playerNickname;
    private string setName = "";
    public GameObject connecting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        connecting.SetActive(false);
        //PhotonNetwork.AutomaticallySyncScene = true; // Enable automatic scene synchronization
        //PhotonNetwork.ConnectUsingSettings(); // Connect to Photon Master Server
    }

    // Update is called once per frame
    public void UpdateText()
    {
        setName = playerNickname.text;
        PhotonNetwork.LocalPlayer.NickName = setName; // Set the player's nickname
    }
    public void EnterButton()
    {
        if (setName != "")
        {
            connecting.SetActive(true); // Show connecting UI
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings(); // Connect to Photon Master Server
        }
        else
        {
            Debug.Log("Please enter a nickname before proceeding.");
        }
    }
    public void ExitButton()
    {
        Application.Quit(); // Exit the application
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        SceneManager.LoadScene("Lobby"); // Load the Lobby scene
    }
    
}
