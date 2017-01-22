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

    public delegate void SineStateChangedEventHandler(SineState state);

    public event SineStateChangedEventHandler StateChanged;

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

    public float Update(bool skipTimer = false)
    {
        if(!skipTimer) timer += Time.deltaTime;
        switch (state)
        {
            case SineState.Rising:
                if (timer >= switchTime)
                {
                    if (skipTimer)
                        timer = 0;
                    else
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
                    if (skipTimer)
                        timer = 0;
                    else
                        timer -= highTime;
                    state = SineState.Falling;
                }
                break;
            case SineState.Falling:
                if (timer >= switchTime)
                {
                    if (skipTimer)
                        timer = 0;
                    else
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
                    if (skipTimer)
                        timer = 0;
                    else
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

    public void Waveskip(float forward)
    {
        value += forward;
        Update(true);
    }
}
