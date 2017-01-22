using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSin : MonoBehaviour
{
    private new ParticleSystem.MainModule particleSystem;
    public float Sin;
    public float SinScale;
    // Use this for initialization
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>().main;
    }

    // Update is called once per frame
    void Update()
    {
        particleSystem.startSpeed = Sin * SinScale;
    }
}
