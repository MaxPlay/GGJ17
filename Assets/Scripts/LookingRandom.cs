using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingRandom : MonoBehaviour
{
    public Transform Focus;
    public float MaxDistance;
    public float MinDistance;
    public float MaxAngle;
    public float HeadMovementSpeed = 10;
    private Vector3 horizontalFocus;
    private Vector3 horizontalPosition;
    private Ray ray;
    private RaycastHit hit;
    private Quaternion startingRotation;
    public LayerMask layerMask;
    void Start()
    {
        Debug.Log("LookingRandom does not work completly!");
        startingRotation = transform.rotation;
    }

    void CalcHorizontalValues()
    {
        horizontalFocus = Focus.position;
        horizontalFocus.y = 0;
        horizontalPosition = transform.position;
        horizontalPosition.y = 0;
    }

    float GetAngleToFocus()
    {
        return Vector3.Angle(horizontalFocus - transform.parent.position, transform.parent.forward);
    }


    void FindFocus()
    {

        ray = new Ray(transform.position, transform.position + transform.forward * MaxDistance);
        if (Physics.Raycast(ray, out hit, MaxDistance, layerMask))
        {
            Focus = hit.collider.transform;
        }
    }
    void Update()
    {
        FindFocus();

        if (Focus != null)
        {
            CalcHorizontalValues();
            if (GetAngleToFocus() <= 90 - MaxAngle && Vector3.Distance(horizontalFocus, horizontalPosition) > MinDistance)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Focus.position - transform.position), Time.deltaTime * HeadMovementSpeed);
            }
            else
            {
                Focus = null;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, startingRotation, Time.deltaTime * HeadMovementSpeed);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * MaxDistance);
    }

}
