using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class KillCount : MonoBehaviour
{
    public List<Kills> highestKills = new List<Kills>();
    public Text[] names;
    public Text[] killAmount;
    private GameObject killCountPanel;
    private GameObject namesObject;
    private bool killCountPanelActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        killCountPanel = GameObject.Find("KillCountPanel");
        namesObject = GameObject.Find("NicknameBG");
        killCountPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!killCountPanelActive)
            {
                killCountPanel.SetActive(true);
                killCountPanelActive = true;
                highestKills.Clear();
                for (int i = 0; i < names.Length; i++)
                {
                    highestKills.Add(new Kills(namesObject.GetComponent<NickNameScript>().name[i].text, Random.Range(0, 10))); // Simulating player names and kills
                }
                highestKills.Sort();
                for (int i = 0; i < names.Length; i++)
                {
                    if (i < highestKills.Count)
                    {
                        names[i].text = highestKills[i].playerName;
                        killAmount[i].text = highestKills[i].playerKills.ToString();
                    }
                }
                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i].text == "Name")
                    {
                        names[i].text = "";
                        killAmount[i].text = "";
                    }
                }
            }
            else if (killCountPanelActive)
            {
                killCountPanel.SetActive(false);
                killCountPanelActive = false;
            }
        }
    }
}
