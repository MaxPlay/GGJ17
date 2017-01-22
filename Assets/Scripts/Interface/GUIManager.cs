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
    Sprite dummySprite;

    [SerializeField]
    Sprite[] powerUpSprites;

    [SerializeField]
    Sprite[] abilitySprites;

    [SerializeField]
    PlayerController locPlayerController;

    [SerializeField]
    RectTransform[] listCurves;

    [SerializeField]
    [Range (0,1)]
    float speed;



    // Use this for initialization
    void Start()
    {
        PowerUp1.color = Color.white;
        PowerUp2.color = Color.white;
        PowerUp3.color = Color.white;


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


        for (int i = 0; i < listCurves.Length; i++)
        {
            if (listCurves[i].localPosition.x < -200)
            {
                listCurves[i].localPosition = new Vector3(200, 0, 0);
            }


            listCurves[i].localPosition = new Vector3(listCurves[i].localPosition.x - (float)speed, 0, 0);
            
        }
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
