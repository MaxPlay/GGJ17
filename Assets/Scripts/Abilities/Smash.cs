using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Smash : Ability
{
    [SerializeField]
    private float area;

    public float Area
    {
        get { return area; }
    }

    [SerializeField]
    private float pushbackDistance;

    public float PushbackDistance
    {
        get { return pushbackDistance; }
    }

}
