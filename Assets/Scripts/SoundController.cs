using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {


    public AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = gameObject.AddComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //audio.PlayOneShot((AudioClip)Resources.Load("powerup"));
            audio.Play();
            print("SoundController: Audio played ");
        }
	}
}
