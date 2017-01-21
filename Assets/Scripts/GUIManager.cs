using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    [SerializeField]
    Image Ability1;
    [SerializeField]
    Image Ability2;
    [SerializeField]
    Sprite[] sprites;

    // Use this for initialization
    void Start () {
        sprites = new Sprite[(int)PowerUps.Raydestruct];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
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
