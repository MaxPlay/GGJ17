using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave : Ability
{
    [SerializeField]
    private float distance;

    public float Distance
    {
        get { return distance; }
    }
}
