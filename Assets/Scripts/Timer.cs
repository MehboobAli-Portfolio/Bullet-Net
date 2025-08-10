using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text minutesText;
    public Text secondsText;
    public int minutes = 4;
    public int seconds = 59;
    public GameObject Canvas;
    [HideInInspector]
    public bool timeStop = false;
    public void BeginTimer()
    {
        GetComponent<PhotonView>().RPC("Count", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void Count()
    {
        BeginCounting();
    }
    private void BeginCounting()
    {
        CancelInvoke("TimeCountDown"); // Ensure no previous invocations are running
        InvokeRepeating("TimeCountDown", 1f, 1f); // Call this method every 1 second
    }
    private void TimeCountDown()
    {
        if (!this.gameObject.GetComponent<NickNameScript>().noRespawn)
        {
            if (seconds > 10)
            {
                seconds -= 1;
                secondsText.text = seconds.ToString();
            }
            else if (seconds > 0 && seconds < 11)
            {
                seconds -= 1;
                secondsText.text = "0" + seconds.ToString();
            }
            else if (seconds == 0 && minutes > 0)
            {
                secondsText.text = "0" + seconds.ToString();
                minutes -= 1;
                seconds = 59;
                minutesText.text = minutes.ToString();
                secondsText.text = seconds.ToString();
            }
            else if (seconds == 0 && minutes <= 0)
            {
                if (this.gameObject.GetComponent<NickNameScript>().teamMode == false)
                {
                    secondsText.text = "00";
                    minutesText.text = "00";
                    timeStop = true;
                    Canvas.GetComponent<KillCount>().countDown = false;
                    Canvas.GetComponent<KillCount>().TimeOver();
                }
                else if (this.gameObject.GetComponent<NickNameScript>().teamMode == true)
                {
                    secondsText.text = "00";
                    minutesText.text = "00";
                    timeStop = true;
                    Canvas.GetComponent<TeamKillCount>().countDown = false;
                    Canvas.GetComponent<TeamKillCount>().TimeOver();
                }
            }
        }
        else
        {
            minutesText.text = "";
            secondsText.text = "";
        }

    }
}