using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Utils/CoordinateSystemHelper")]
public class CoordinateSystemHelper : MonoBehaviour
{
    [SerializeField]
    float length = 1;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * length);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * length);
    }
}
