using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SineStateSwitcher
{
    public enum SineState
    {
        Rising,
        High,
        Falling,
        Low
    }

    [SerializeField]
    private float switchTime;

    public float SwitchTime
    {
        get { return switchTime; }
        set { switchTime = value; }
    }

    [SerializeField]
    private float highTime;

    public float HighTime
    {
        get { return highTime; }
        set { highTime = value; }
    }

    [SerializeField]
    private float lowTime;

    public float LowTime
    {
        get { return lowTime; }
        set { lowTime = value; }
    }

    [SerializeField]
    private float value;

    public float Value
    {
        get { return value; }
    }

    public float Update()
    {
        timer += Time.deltaTime;
        switch (state)
        {
            case SineState.Rising:
                if (timer >= switchTime)
                {
                    timer -= switchTime;
                    state = SineState.High;
                    value = 1;
                    break;
                }
                value = Mathf.Cos(Mathf.PI - timer / switchTime * Mathf.PI);
                break;
            case SineState.High:
                if (timer >= highTime)
                {
                    timer -= highTime;
                    state = SineState.Falling;
                }
                break;
            case SineState.Falling:
                if (timer >= switchTime)
                {
                    timer -= switchTime;
                    state = SineState.Low;
                    value = -1;
                    break;
                }
                value = Mathf.Cos(timer / switchTime * Mathf.PI);
                break;
            case SineState.Low:
                if (timer >= lowTime)
                {
                    timer -= lowTime;
                    state = SineState.Rising;
                }
                break;
        }

        return value;
    }

    [SerializeField]
    private SineState state;
    private float timer;

    public SineState State
    {
        get { return state; }
        set { state = value; }
    }
}
