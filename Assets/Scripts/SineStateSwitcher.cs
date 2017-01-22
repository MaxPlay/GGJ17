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

    private void OnStateChanged(SineState state)
    {
        if (StateChanged != null)
            StateChanged(state);
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

    public float Update(bool skipTimer = false)
    {
        if (!skipTimer) timer += Time.deltaTime;
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
                    OnStateChanged(SineState.High);
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
                    OnStateChanged(SineState.Falling);
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
                    OnStateChanged(SineState.Low);
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
                    OnStateChanged(SineState.Rising);
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

    public float ConstantSine
    {
        get
        {
            switch (state)
            {
                case SineState.Rising:
                case SineState.Falling:
                    return timer / switchTime;
                case SineState.High:
                    return timer / highTime;
                case SineState.Low:
                    return timer / lowTime;
            }
            return 0;
        }
    }

    public void Waveskip(float forward)
    {
        value += forward;
        Update(true);
    }
}
