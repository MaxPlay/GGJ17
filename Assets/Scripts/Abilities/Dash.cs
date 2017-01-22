using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dash : Ability
{
    [SerializeField]
    private float pushbackDistance;

    public float PushbackDistance
    {
        get { return pushbackDistance; }
    }

    [SerializeField]
    private float radius;

    public float Radius
    {
        get { return radius; }
    }

}
