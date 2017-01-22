using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PickUpRotator : MonoBehaviour
{
    [SerializeField]
    Vector3 rotation;
    [SerializeField]
    private PowerUps type;

    public PowerUps Type
    {
        get { return type; }
    }
    
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
