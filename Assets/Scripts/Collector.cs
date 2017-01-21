using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour {

   

    private int count;
    public Text CountText;    
    public Text WinText;

    //PowerUPs Text und Counter:
    private int countPUInvincibility;
    public Text CountPUInvincibilityText;

    private int countPUFreeze;
    public Text CountPUFrezeText;

    private int countPUWaveskip;
    public Text CountPUWaveskipText;

    private int countPUDoubleDamage;
    public Text CountPUDoubleDamageText;

    private int countPUPush;
    public Text CountPUPushText;

    private int countPURageBurst;
    public Text CountPURageBurstText;

    private int countPURaydestruct;
    public Text CountPURaydestructText;


    // Use this for initialization
    void Start () {
       
        count = 0;
        countPUInvincibility = 0;
        countPUFreeze = 0;
        countPUWaveskip = 0;
        countPUDoubleDamage = 0;
        countPUPush = 0;
        countPURageBurst = 0;
        countPURaydestruct = 0;

        setCountText();
        setCountPowerUpsText();
        WinText.text = "";
	}

    


    //ein PowerUP wird (zerstört)
    //deaktiviert:
    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
        }

        //POWERUPS:

        //Invincibility:
        if (other.gameObject.CompareTag("PUInvincibility"))
        {
            other.gameObject.SetActive(false);
            countPUInvincibility++;
            setCountPowerUpsText();
        }

        //Freeze:
        if (other.gameObject.CompareTag("PUFreeze"))
        {
            other.gameObject.SetActive(false);
            countPUFreeze++;
            setCountPowerUpsText();
        }


        //Waveskip
        if (other.gameObject.CompareTag("PUWaveskip"))
        {
            other.gameObject.SetActive(false);
            countPUWaveskip++;
            setCountPowerUpsText();
        }

        //Double Damage:
        if (other.gameObject.CompareTag("PUDoubleDamage"))
        {
            other.gameObject.SetActive(false);
            countPUDoubleDamage++;
            setCountPowerUpsText();
        }

        //Push
        if (other.gameObject.CompareTag("PUPush"))
        {
            other.gameObject.SetActive(false);
            countPUPush++;
            setCountPowerUpsText();
        }

        //Rage Burst:
        if (other.gameObject.CompareTag("PURageBurst"))
        {
            other.gameObject.SetActive(false);
            countPURageBurst++;
            setCountPowerUpsText();
        }

        //Ray Destruct
        if (other.gameObject.CompareTag("PURayDestruct"))
        {
            other.gameObject.SetActive(false);
            countPURaydestruct++;
            setCountPowerUpsText();
        }

    }

    void setCountText()
    {
        CountText.text = "Count: " + count.ToString();
        if(count >= 5)
        {
            WinText.text = "You are great!";
        }
    }
    
    void setCountPowerUpsText()
    {
        CountPUInvincibilityText.text = "PowerUp Invincibility: " + countPUInvincibility.ToString();
        CountPUFrezeText.text = "PowerUp Freeze: " + countPUFreeze.ToString();
        CountPUWaveskipText.text = "PowerUp Waveskip: " + countPUWaveskip.ToString();
        CountPUDoubleDamageText.text = "PowerUp Double Damage: " + countPUDoubleDamage.ToString();
        CountPUPushText.text = "PowerUp Push: " + countPUPush.ToString();
        CountPURageBurstText.text = "PowerUp Rageburst: " + countPURageBurst.ToString();
        CountPURaydestructText.text = "PowerUp Raydestruct: " + countPURaydestruct.ToString();

    }

   
}
