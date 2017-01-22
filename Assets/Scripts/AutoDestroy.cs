using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    Timer timer;
	// Use this for initialization
	void Start () {
        timer = new Timer(3);
	}
	
	// Update is called once per frame
	void Update () {
        if (timer.Update())
        {
            Destroy(transform.gameObject);
        }
	}
}
