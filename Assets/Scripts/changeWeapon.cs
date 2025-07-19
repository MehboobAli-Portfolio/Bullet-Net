using UnityEngine;
using UnityEngine.Animations.Rigging;
public class changeWeapon : MonoBehaviour
{
    public TwoBoneIKConstraint leftHand;
    public TwoBoneIKConstraint rightHand;
    public TwoBoneIKConstraint leftThumb;
    public RigBuilder rig;
    public Transform[] leftHandTargetWeapons;
    public Transform[] rightHandTargetWeapons;
    public Transform[] leftThumbTargetWeapons;
    public GameObject[] weapons;
    private int weaponNumber = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
