using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour {



    private int maxBag;
    public Text MaxBagText;

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

        //Maximmale Anzahl an Elementen, dich in meiner Taschen haben darf:
        maxBag = 3;

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
        if (other.gameObject.CompareTag("PUInvincibility") && maxBag > 0)
        {
            other.gameObject.SetActive(false);
            countPUInvincibility++;
            
            maxBag--;
            setCountPowerUpsText();
        }

        //Freeze:
        if (other.gameObject.CompareTag("PUFreeze") && maxBag > 0)
        {
            other.gameObject.SetActive(false);
            countPUFreeze++;

            maxBag--;
            setCountPowerUpsText();
        }


        //Waveskip
        if (other.gameObject.CompareTag("PUWaveskip") && maxBag > 0)
        {
            other.gameObject.SetActive(false);
            countPUWaveskip++;

            maxBag--;
            setCountPowerUpsText();
        }

        //Double Damage:
        if (other.gameObject.CompareTag("PUDoubleDamage") && maxBag > 0)
        {
            other.gameObject.SetActive(false);
            countPUDoubleDamage++;

            maxBag--;
            setCountPowerUpsText();
        }

        //Push
        if (other.gameObject.CompareTag("PUPush") && maxBag > 0)
        {
            other.gameObject.SetActive(false);
            countPUPush++;

            maxBag--;
            setCountPowerUpsText();
        }

        //Rage Burst:
        if (other.gameObject.CompareTag("PURageBurst") && maxBag > 0)
        {
            other.gameObject.SetActive(false);
            countPURageBurst++;

            maxBag--;
            setCountPowerUpsText();
        }

        //Ray Destruct
        if (other.gameObject.CompareTag("PURayDestruct") && maxBag > 0)
        {
            other.gameObject.SetActive(false);
            countPURaydestruct++;

            maxBag--;
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
        MaxBagText.text = "Platz im Bag: " + maxBag.ToString();

        CountPUInvincibilityText.text = "PowerUp Invincibility: " + countPUInvincibility.ToString();
        CountPUFrezeText.text = "PowerUp Freeze: " + countPUFreeze.ToString();
        CountPUWaveskipText.text = "PowerUp Waveskip: " + countPUWaveskip.ToString();
        CountPUDoubleDamageText.text = "PowerUp Double Damage: " + countPUDoubleDamage.ToString();
        CountPUPushText.text = "PowerUp Push: " + countPUPush.ToString();
        CountPURageBurstText.text = "PowerUp Rageburst: " + countPURageBurst.ToString();
        CountPURaydestructText.text = "PowerUp Raydestruct: " + countPURaydestruct.ToString();

    }

   
}
