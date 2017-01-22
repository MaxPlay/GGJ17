using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyanimaAnimator : MonoBehaviour {

    private Animator animator;
    public float Movement;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    public void OnPunch()
    {
        animator.SetTrigger("Punch");
    }

    public void OnStomp()
    {
        animator.SetTrigger("Stomp");
    }

    public void OnHit()
    {
        animator.SetTrigger("Hit");
    }
	
	// Update is called once per frame
	void Update () {

        animator.SetFloat("Movement", Movement);
    }
}
