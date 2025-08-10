using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using Photon.Pun;
using UnityEngine.UI;
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

    private GameObject testForWeapons;

    private Image WeaponIcon;
    private Text ammoAmountText;
    public Sprite[] weaponIcons;
    public int[] ammoAmounts;

    public GameObject[] muzzleFlash;
    private string shooterName;
    private string gotShotName;
    public float[] damageAmts;
    public bool isDeath=false;
    private GameObject choosePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        choosePanel = GameObject.Find("ChoosePanel");
        ammoAmountText.text = ammoAmounts[0].ToString();
        WeaponIcon = GameObject.Find("WeaponUi").GetComponent<Image>();
        ammoAmountText = GameObject.Find("AmmoAmount").GetComponent<Text>();
        camObject = GameObject.Find("PlayerCam");
        ammoAmounts[0]=60;
        ammoAmounts[1]=0;
        ammoAmounts[2]=0;
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
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
        testForWeapons = GameObject.Find("Weapon1PickUp(Clone)");
        if (testForWeapons == null)
        {
            if(this.gameObject.GetComponent<PhotonView>().Owner.IsMasterClient)
            {
                var spawner = GameObject.Find("SpawnScript");
                spawner.GetComponent<SpawnCharacter>().SpawnWeaponsStart();
            }
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
        if (!isDeath)
        {
            if (Input.GetMouseButtonDown(0)  && choosePanel.activeInHierarchy == false)
            {
                if (this.gameObject.GetComponent<PhotonView>().IsMine && ammoAmounts[weaponNumber] > 0)
                {
                    ammoAmounts[weaponNumber]--;
                    ammoAmountText.text = ammoAmounts[weaponNumber].ToString();
                    if (muzzleFlash[weaponNumber] != null)
                    {
                        GetComponent<DisplayColor>().PalyGunshot(this.GetComponent<PhotonView>().Owner.NickName, weaponNumber);
                        this.GetComponent<PhotonView>().RPC("GunMuzzleFlash", RpcTarget.All);
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                        if (Physics.Raycast(ray, out hit, 500))
                        {
                            if (hit.transform.gameObject.GetComponent<PhotonView>() != null)
                            {
                                gotShotName = hit.transform.gameObject.GetComponent<PhotonView>().Owner.NickName;
                            }
                            if (hit.transform.gameObject.GetComponent<DisplayColor>() != null)
                            {
                                hit.transform.gameObject.GetComponent<DisplayColor>().DeliverDamage(this.GetComponent<PhotonView>().Owner.NickName, hit.transform.gameObject.GetComponent<PhotonView>().Owner.NickName, damageAmts[weaponNumber]);
                            }
                            shooterName = GetComponent<PhotonView>().Owner.NickName;
                            Debug.Log(gotShotName + " got hit by " + shooterName);
                        }
                        this.gameObject.layer = LayerMask.NameToLayer("Default");

                        //muzzleFlash[weaponNumber].SetActive(true);
                        //StartCoroutine(MuzzleOff());
                    }
                }
            }
            if (Input.GetMouseButtonDown(1) && this.gameObject.GetComponent<PhotonView>().IsMine)
            {
                this.GetComponent<PhotonView>().RPC("Change", RpcTarget.AllBuffered);
                if (weaponNumber > weapons.Length - 1)
                {
                    WeaponIcon.sprite = weaponIcons[0];
                    ammoAmountText.text = ammoAmounts[0].ToString();
                    weaponNumber = 0;
                }
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].SetActive(false);
                }
                weapons[weaponNumber].SetActive(true);
                WeaponIcon.sprite = weaponIcons[weaponNumber];
                ammoAmountText.text = ammoAmounts[weaponNumber].ToString();
                leftHand.data.target = leftHandTargetWeapons[weaponNumber];
                rightHand.data.target = rightHandTargetWeapons[weaponNumber];
                leftThumb.data.target = leftThumbTargetWeapons[weaponNumber];
                rig.Build();

            }
        }
        
    }
    public void UpdatePickup()
    {
        ammoAmountText.text = ammoAmounts[weaponNumber].ToString();
        // Update any other UI elements related to the weapon pickup
    }
    [PunRPC]
    public void GunMuzzleFlash()
    {
        muzzleFlash[weaponNumber].SetActive(true);
        StartCoroutine(MuzzleOff());
    }
    [PunRPC]
    public void Change()
    {
        weaponNumber++;
        if (weaponNumber > weapons.Length - 1)
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
    IEnumerator MuzzleOff()
    {
        yield return new WaitForSeconds(0.03f);
        this.GetComponent<PhotonView>().RPC("MuzzleOffRPC", RpcTarget.All);
    }
    [PunRPC]
    public void MuzzleOffRPC()
    {
        if (muzzleFlash[weaponNumber] != null)
        {
            muzzleFlash[weaponNumber].SetActive(false);
        }
    }
}
