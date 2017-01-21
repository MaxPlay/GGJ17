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
    Sprite[] powerUpSprites;

    [SerializeField]
    Sprite[] abilitySprites;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
    Raydestruct
}
