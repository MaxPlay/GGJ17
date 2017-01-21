using UnityEngine;

[RequireComponent(typeof(Light))]
[AddComponentMenu("Utils/LightSwitch")]
public class LightSwitch : MonoBehaviour
{
    #region Private Fields

    private new Light light;

    #endregion Private Fields

    #region Public Methods

    public void SetOff()
    {
        light.enabled = false;
    }

    public void SetOn()
    {
        light.enabled = true;
    }

    public void Toggle()
    {
        light.enabled = !light.enabled;
    }

    #endregion Public Methods

    #region Private Methods

    private void Start()
    {
        light = GetComponent<Light>();
    }

    #endregion Private Methods
}