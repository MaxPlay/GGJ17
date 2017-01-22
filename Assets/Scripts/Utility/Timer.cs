using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float value;

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    private float maxValue;

    public float MaxValue
    {
        get { return maxValue; }
        set { maxValue = value; }
    }

    public float Percentage { get { return 1 - (value / maxValue); } }

    public bool Update()
    {
        if (value > 0)
            value -= Time.deltaTime;
        else
        {
            value = 0;
            return true;
        }
        return false;
    }

    private Timer()
    {
        this.value = 0;
        maxValue = 0;
    }

    public Timer(float value)
    {
        this.value = value;
        maxValue = value;
    }
}
