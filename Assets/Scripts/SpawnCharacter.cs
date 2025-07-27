using UnityEngine;
using Photon.Pun;
public class SpawnCharacter : MonoBehaviour
{
    public GameObject character;
    public Transform[] spawnPoints;
    public GameObject[] weapons;
    public Transform[] weaponSpawnPoints;
    private float weaponRespawnTime=10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(character.name,spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount -1].position,spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount -1].rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnWeaponsStart()
    {
        for(int i=0;i<weapons.Length;i++)
        {
            PhotonNetwork.Instantiate(weapons[i].name,weaponSpawnPoints[i].position,weaponSpawnPoints[i].rotation);
        }
    }
}
