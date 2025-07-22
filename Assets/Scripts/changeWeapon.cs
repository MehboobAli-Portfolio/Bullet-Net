using UnityEngine;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using Photon.Pun;
public class changeWeapon : MonoBehaviour
{
    public TwoBoneIKConstraint leftHand;
    public TwoBoneIKConstraint rightHand;
    public TwoBoneIKConstraint leftThumb;

    private CinemachineCamera cam;
    public GameObject camObject;

    public MultiAimConstraint[] aimObjects;
    private Transform aimTarget;

    public RigBuilder rig;

    public Transform[] leftHandTargetWeapons;
    public Transform[] rightHandTargetWeapons;
    public Transform[] leftThumbTargetWeapons;
    public GameObject[] weapons;
    private int weaponNumber = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camObject = GameObject.Find("PlayerCam");
        //aimTarget = GameObject.Find("AimReference").transform;
        if (this.gameObject.GetComponent<PhotonView>().IsMine == true)
        {
            cam = camObject.GetComponent<CinemachineCamera>();
            cam.Follow = this.gameObject.transform;
            cam.LookAt = this.gameObject.transform;
            //Invoke("SetLookAt", 0.1f);
        }
        else
        {
            this.gameObject.GetComponent<PlayerMovement>().enabled=false;
        }
    }
    /*void SetLookAt()
    {
        if (aimTarget != null)
        {
            for (int i = 0; i < aimObjects.Length; i++)
            {
                var sources = aimObjects[i].data.sourceObjects;
                sources.Clear();
                sources.Add(new WeightedTransform(aimTarget, 1f));
                aimObjects[i].data.sourceObjects = sources;
            }
            rig.Build();
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            weaponNumber++;
            if (weaponNumber >= weapons.Length)
            {
                weaponNumber = 0;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }
            weapons[weaponNumber].SetActive(true);
            leftHand.data.target = leftHandTargetWeapons[weaponNumber];
            rightHand.data.target = rightHandTargetWeapons[weaponNumber];
            leftThumb.data.target = leftThumbTargetWeapons[weaponNumber];
            rig.Build();
        }
    }
}
