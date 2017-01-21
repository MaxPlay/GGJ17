using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraPosition
{
    [SerializeField]
    private float upperAngle;

    public float UpperAngle
    {
        get { return upperAngle; }
        set { upperAngle = value; }
    }

    [SerializeField]
    private float lowerAngle;

    public float LowerAngle
    {
        get { return lowerAngle; }
        set { lowerAngle = value; }
    }

    [SerializeField]
    private float upperDistance;

    public float UpperDistance
    {
        get { return upperDistance; }
        set { upperDistance = value; }
    }

    [SerializeField]
    private float lowerDistance;

    public float LowerDistance
    {
        get { return lowerDistance; }
        set { lowerDistance = value; }
    }

    public void DebugDraw(Vector3 center, Color debugColor)
    {
        Gizmos.color = debugColor;
        DrawCircle(center, upperAngle, upperDistance);
        DrawCircle(center, lowerAngle, lowerDistance);
    }

    public Vector3 GetPointOnCircle(float angle, float height)
    {
        float correctedAngle = angle % (Mathf.PI * 2);
        float correctedHeight = Mathf.Clamp01(height);

        float x = Mathf.Cos(correctedAngle) * Mathf.Lerp(lowerDistance, lowerDistance, correctedHeight);
        float y = Mathf.Lerp(lowerAngle, upperAngle, correctedHeight);
        float z = Mathf.Sin(correctedAngle) * Mathf.Lerp(lowerDistance, lowerDistance, correctedHeight);

        return new Vector3(x, y, z);
    }

    private void DrawCircle(Vector3 center, float heightOffset, float distance)
    {
        for (int i = 0; i < 20; i++)
        {
            float x = Mathf.Cos(Mathf.PI * i / 10f) * distance;
            float y = Mathf.Sin(Mathf.PI * i / 10f) * distance;

            float x2 = Mathf.Cos(Mathf.PI * (i + 1) / 10f) * distance;
            float y2 = Mathf.Sin(Mathf.PI * (i + 1) / 10f) * distance;
            Gizmos.DrawLine(center + new Vector3(x, heightOffset, y), center + new Vector3(x2, heightOffset, y2));
        }
    }
}

[SerializeField]
public class CameraController : MonoBehaviour
{
    new Camera camera;
    [SerializeField]
    PlayerController controller;

    [SerializeField]
    CameraPosition chibi = new CameraPosition();
    [SerializeField]
    CameraPosition monster = new CameraPosition();

    [SerializeField]
    private float angle;

    public float Angle { get { return angle; } }

    [SerializeField]
    [Range(0, 1)]
    private float height;

    [SerializeField]
    [Range(float.Epsilon, 1)]
    private float cameraMoveSpeedX;

    [SerializeField]
    [Range(float.Epsilon, 1)]
    private float cameraMoveSpeedY;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        Vector2 input = controller.UsedControl == Control.Controller ? GamePadManager.ThumbRight(XInputDotNetPure.PlayerIndex.One) : new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        height += input.y * cameraMoveSpeedY;
        height = Mathf.Clamp01(height);
        angle -= input.x * cameraMoveSpeedX;

        transform.position = controller.transform.position;
        camera.transform.localPosition = GetPosition(controller.SineValue);
        camera.transform.LookAt(transform);

        controller.CameraDirection = angle;
    }

    private Vector3 GetPosition(float value)
    {
        if (value == 1)
            return monster.GetPointOnCircle(angle, height);

        if (value == -1)
            return chibi.GetPointOnCircle(angle, height);

        return Vector3.Lerp(chibi.GetPointOnCircle(angle, height), monster.GetPointOnCircle(angle, height), (value + 1 / 2f));
    }

    public void OnDrawGizmos()
    {
        chibi.DebugDraw(transform.position, Color.blue);
        monster.DebugDraw(transform.position, Color.red);
    }
}
