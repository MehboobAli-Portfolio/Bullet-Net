using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class lookAt : MonoBehaviour
{
    private Vector3 screenPosition;
    private Vector3 worldPosition;
    public GameObject crossHead; // The target object to look at
    public Text nicknameText; // The text component to display the nickname
    private void Start()
    {
        Cursor.visible = false; // Hide the cursor
        nicknameText.text = PhotonNetwork.LocalPlayer.NickName; // Set the nickname text
    }
    void FixedUpdate()
    {
        screenPosition = Input.mousePosition;
        // Set a fixed distance from the camera (e.g., 3 units)
        screenPosition.z = 3f;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // Move only on XZ plane, keep current Y
        transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
        crossHead.transform.position = new Vector3(screenPosition.x, screenPosition.y, 0f);
    }
}
