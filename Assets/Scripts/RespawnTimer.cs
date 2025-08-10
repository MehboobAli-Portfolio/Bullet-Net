using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class RespawnTimer : MonoBehaviour
{
    public Text spawnTime;

    // This function is called when the object becomes enabled and active
    void OnEnable()
    {
        StartCoroutine(SpawnStarting());
    }
    IEnumerator SpawnStarting()
    {
        spawnTime.text = "3";
        yield return new WaitForSeconds(1f);
        spawnTime.text = "2";
        yield return new WaitForSeconds(1f);
        spawnTime.text = "1";
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
