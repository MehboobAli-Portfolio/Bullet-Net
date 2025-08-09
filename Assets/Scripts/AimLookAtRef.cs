using UnityEngine;
using Photon.Pun;
public class AimLookAtRef : MonoBehaviour
{
   private GameObject lookAtObject;
   public bool isDeath = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookAtObject = GameObject.Find("AimReference");  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDeath)
        {
            if (this.gameObject.GetComponentInParent<PhotonView>().IsMine)
            {
                this.transform.position = lookAtObject.transform.position;
            }
        }
    }
}
