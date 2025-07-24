using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    public float speed=20f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime,0);
    }
}
