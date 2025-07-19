using UnityEngine;
using UnityEngine.Animations.Rigging;

public class weaponChange : MonoBehaviour
{
    public TwoBoneIKConstraint leftHand;
    public TwoBoneIKConstraint rightHand;
    public RigBuilder rig;
    public Transform leftHandTargetWeapon1;
    public Transform leftHandTargetWeapon2;
    public Transform leftHandTargetWeapon3;
    public Transform rightHandTargetWeapon1;
    public Transform rightHandTargetWeapon2;
    public Transform rightHandTargetWeapon3;
    public GameObject weapon1;
    public GameObject weapon2;  
    public GameObject weapon3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            leftHand.data.target = leftHandTargetWeapon1;
            rightHand.data.target = rightHandTargetWeapon1;
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            rig.Build();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            leftHand.data.target = leftHandTargetWeapon2;
            rightHand.data.target = rightHandTargetWeapon2;
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
            rig.Build();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            leftHand.data.target = leftHandTargetWeapon3;
            rightHand.data.target = rightHandTargetWeapon3;
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
            rig.Build();
        }
    }
}
