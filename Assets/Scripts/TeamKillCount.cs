using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class TeamKillCount : MonoBehaviour
{
    public List<Kills> highestKills = new List<Kills>();
    public Text[] killAmount;
    private GameObject killCountPanel;
    private GameObject namesObject;
    private bool killCountPanelActive = false;
    public bool countDown = true;
    public GameObject WinnerPanel;
    public Text winnerText;
    private int RedTeamKills;
    private int GreenTeamKills;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        killCountPanel = GameObject.Find("KillCountPanel");
        namesObject = GameObject.Find("NicknameBG");
        killCountPanel.SetActive(false);
        WinnerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && countDown)
        {
            if (!killCountPanelActive)
            {
                killCountPanel.SetActive(true);
                killCountPanelActive = true;
                highestKills.Clear();
                for (int i = 0; i < 6; i++)
                {
                    highestKills.Add(new Kills(namesObject.GetComponent<NickNameScript>().name[i].text, namesObject.GetComponent<NickNameScript>().kills[i])); // Store player names and kills
                }
                RedTeamKills = highestKills[0].playerKills + highestKills[1].playerKills + highestKills[2].playerKills;
                GreenTeamKills = highestKills[3].playerKills + highestKills[4].playerKills + highestKills[5].playerKills;
                killAmount[0].text = RedTeamKills.ToString();
                killAmount[1].text = GreenTeamKills.ToString();
            }
            else if (killCountPanelActive)
            {
                killCountPanel.SetActive(false);
                killCountPanelActive = false;
            }
        }
    }
    public void TimeOver()
    {
        killCountPanel.SetActive(true);
        WinnerPanel.SetActive(true);
        killCountPanelActive = true;
        highestKills.Clear();
        for (int i = 0; i < 6; i++)
        {
            highestKills.Add(new Kills(namesObject.GetComponent<NickNameScript>().name[i].text, namesObject.GetComponent<NickNameScript>().kills[i])); // Store player names and kills
        }
        RedTeamKills = highestKills[0].playerKills + highestKills[1].playerKills + highestKills[2].playerKills;
        GreenTeamKills = highestKills[3].playerKills + highestKills[4].playerKills + highestKills[5].playerKills;
        killAmount[0].text = RedTeamKills.ToString();
        killAmount[1].text = GreenTeamKills.ToString();
        if (RedTeamKills > GreenTeamKills)
        {
            winnerText.text = "Red Team Wins!";
        }
        else if (GreenTeamKills > RedTeamKills)
        {
            winnerText.text = "Green Team Wins!";
        }
        else
        {
            winnerText.text = "It's a Tie!";
        }
    }
}
