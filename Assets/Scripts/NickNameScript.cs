using UnityEngine;
using UnityEngine.UI;

public class NickNameScript : MonoBehaviour
{
    public Text[] name;
    public Image[] healthBar;
    private void Start()
    {
        for (int i = 0; i < name.Length; i++)
        {
            name[i].gameObject.SetActive(false);
            healthBar[i].gameObject.SetActive(false);
        }
    }
}
