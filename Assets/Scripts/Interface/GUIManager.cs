using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    Image PowerUp1;
    [SerializeField]
    Image PowerUp2;
    [SerializeField]
    Image PowerUp3;

    [SerializeField]
    Image Ability1;
    [SerializeField]
    Image Ability2;
    [SerializeField]
    Image Ability3;

    [SerializeField]
    Sprite dummySprite;

    [SerializeField]
    Sprite[] powerUpSprites;

    [SerializeField]
    Sprite[] abilitySprites;

    [SerializeField]
    PlayerController locPlayerController;

    [SerializeField]
    UISineView sineView = new UISineView();

    [SerializeField]
    [Range(0, 1)]
    float speed;



    // Use this for initialization
    void Start()
    {
        PowerUp1.color = Color.white;
        PowerUp2.color = Color.white;
        PowerUp3.color = Color.white;

        sineView.Switcher = locPlayerController.Switcher;
        locPlayerController.ItemAdded += LocPlayerController_ItemAdded;
        locPlayerController.ItemRemoved += LocPlayerController_ItemRemoved;
        locPlayerController.UsedItem += LocPlayerController_UsedItem;
    }

    private void LocPlayerController_UsedItem(int selectedItem)
    {
        SetPowerupWatch(selectedItem);
    }

    [SerializeField]
    private Image powerupWatch;
    [SerializeField]
    private Image abilityWatch1;
    [SerializeField]
    private Image abilityWatch2;
    [SerializeField]
    private Image abilityWatch3;

    private void SetPowerupWatch(int selectedItem)
    {
        switch (selectedItem)
        {
            case 0:
                powerupWatch.transform.position = PowerUp1.transform.position;
                break;
            case 1:
                powerupWatch.transform.position = PowerUp2.transform.position;
                break;
            case 2:
                powerupWatch.transform.position = PowerUp3.transform.position;
                break;
        }
    }

    private void LocPlayerController_ItemRemoved(int selectedItem)
    {
        switch (selectedItem)
        {
            case 0:
                PowerUp1.sprite = dummySprite;
                break;
            case 1:
                PowerUp2.sprite = dummySprite;
                break;
            case 2:
                PowerUp3.sprite = dummySprite;
                break;
        }
    }

    private void LocPlayerController_ItemAdded(object sender, System.EventArgs e)
    {
        for (int i = 0; i < locPlayerController.Collector.Bag.Length; i++)
        {
            switch (i)
            {
                case 0:
                    if (locPlayerController.Collector.Bag[i] == PowerUps.None)
                        PowerUp1.sprite = dummySprite;
                    else
                        PowerUp1.sprite = powerUpSprites[(int)locPlayerController.Collector.Bag[i]];
                    break;
                case 1:
                    if (locPlayerController.Collector.Bag[i] == PowerUps.None)
                        PowerUp2.sprite = dummySprite;
                    else
                        PowerUp2.sprite = powerUpSprites[(int)locPlayerController.Collector.Bag[i]];
                    break;
                case 2:
                    if (locPlayerController.Collector.Bag[i] == PowerUps.None)
                        PowerUp3.sprite = dummySprite;
                    else
                        PowerUp3.sprite = powerUpSprites[(int)locPlayerController.Collector.Bag[i]];
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PowerUp1.sprite = dummySprite;

        // Insert Conditions for assigning pictures

        //Doing the Sinus-thing
        // Window is sizing from 0 -> 240
        // Each curve has a size of 160
        // => @ pos.X == -200 set pos.X to 200

        sineView.Update();

        powerupWatch.fillAmount = 1 - locPlayerController.PowerupTimer.Percentage;
        abilityWatch1.fillAmount = 1 - locPlayerController.Dash.CooldownPercentage;
        abilityWatch2.fillAmount = 1 - locPlayerController.Smash.CooldownPercentage;
        abilityWatch3.fillAmount = 1 - locPlayerController.Wave.CooldownPercentage;

        PowerUp1.transform.parent.GetComponent<Image>().color = locPlayerController.SelectedItem == 0 ? new Color(0.9f, 0.9f, 1f) : Color.white;
        PowerUp2.transform.parent.GetComponent<Image>().color = locPlayerController.SelectedItem == 1 ? new Color(0.9f, 0.9f, 1f) : Color.white;
        PowerUp3.transform.parent.GetComponent<Image>().color = locPlayerController.SelectedItem == 2 ? new Color(0.9f, 0.9f, 1f) : Color.white;
    }
}


enum Abilities
{
    Dash,
    PushWave,
    Hammerwave
}

public enum PowerUps
{
    None = -1,
    Invincibility,
    Freeze,
    Waveskip,
    DoubleDamage,
    RageBurst,
    Raydestruct,
    Push
}
