using System;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Wave wave = new Wave();

    [SerializeField]
    Smash smash = new Smash();

    [SerializeField]
    Dash dash = new Dash();

    [SerializeField]
    private float speed;

    [SerializeField]
    private float acceleration;

    private new Rigidbody rigidbody;

    [SerializeField]
    private SineStateSwitcher switcher = new SineStateSwitcher();

    public float SineValue { get { return switcher.Value; } }

    [SerializeField]
    private Control usedControl;
    private float viewAngle;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        usedControl = GamePadManager.IsConnected(PlayerIndex.One) ? Control.Controller : Control.Keyboard;
        GamePadManager.Connected += GamePadManager_Connected;
        GamePadManager.Disconnected += GamePadManager_Disconnected;
    }

    private void GamePadManager_Disconnected(GamePadManager.GamePadEventArgs e)
    {
        usedControl = Control.Keyboard;
    }

    private void GamePadManager_Connected(GamePadManager.GamePadEventArgs e)
    {
        usedControl = GameManager.Instance.PreferredControl;
    }

    void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > 0)
        {
            float x = Mathf.Cos(viewAngle);
            float z = Mathf.Sin(viewAngle);
            Quaternion targetRotation = Quaternion.Euler(x, 0, z);
        }

        Vector2 movement = usedControl == Control.Controller ? GamePadManager.ThumbLeft(PlayerIndex.One) : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 move = transform.forward * movement.x + transform.right * movement.y;

        rigidbody.AddForce(move * acceleration);

        if (rigidbody.velocity.magnitude > speed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * speed;
        }
    }

    void Update()
    {
        switcher.Update();
    }

    public float CameraDirection
    {
        get { return viewAngle; }
        set { viewAngle = value; }
    }
}

public enum Control
{
    Controller,
    Keyboard
}