using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UISineView
{
    [SerializeField]
    RectTransform[] curves;

    Vector3[] origins;

    [SerializeField]
    RectTransform curvesKnob;

    [SerializeField]
    private SineStateSwitcher switcher;
    private Vector3 knobOrigin;
    private Dictionary<SineStateSwitcher.SineState, Vector2> stateLength;
    private Image knobSprite;

    [SerializeField]
    private Color high;
    [SerializeField]
    private Color low;

    public SineStateSwitcher Switcher
    {
        get { return switcher; }
        set { this.switcher = value; Init(); }
    }

    private void Init()
    {
        origins = new Vector3[curves.Length];
        for (int i = 0; i < curves.Length; i++)
        {
            origins[i] = curves[i].localPosition;
        }

        knobOrigin = curvesKnob.localPosition;

        stateLength = new Dictionary<SineStateSwitcher.SineState, Vector2>();
        stateLength.Add(SineStateSwitcher.SineState.High, new Vector2(0.07f, 0.36f));
        stateLength.Add(SineStateSwitcher.SineState.Falling, new Vector2(0.43f, 0.14f));
        stateLength.Add(SineStateSwitcher.SineState.Low, new Vector2(0.57f, 0.36f));
        stateLength.Add(SineStateSwitcher.SineState.Rising, new Vector2(0.93f, 0.14f));

        knobSprite = curvesKnob.GetComponent<Image>();
    }

    internal void Update()
    {
        float value = switcher.ConstantSine * stateLength[switcher.State].y + stateLength[switcher.State].x;

        for (int i = 0; i < curves.Length; i++)
        {
            curves[i].localPosition = origins[i] - new Vector3(value * curves[i].rect.width, 0, 0);
        }

        curvesKnob.localPosition = knobOrigin + new Vector3(0, Mathf.Sin(value * Mathf.PI * 2) * 35);
        switch (switcher.State)
        {
            case SineStateSwitcher.SineState.Rising:
                knobSprite.color = Color.Lerp(low, high, switcher.ConstantSine);
                break;
            case SineStateSwitcher.SineState.High:
                knobSprite.color = high;
                break;
            case SineStateSwitcher.SineState.Falling:
                knobSprite.color = Color.Lerp(high, low, switcher.ConstantSine);
                break;
            case SineStateSwitcher.SineState.Low:
                knobSprite.color = low;
                break;
        }
    }
}
