using System;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Wave wave = new Wave();

    public Wave Wave { get { return wave; } }

    [SerializeField]
    Smash smash = new Smash();

    public Smash Smash { get { return smash; } }

    [SerializeField]
    Dash dash = new Dash();

    public Dash Dash { get { return dash; } }

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

    public Control UsedControl { get { return usedControl; } }

    [SerializeField]
    private float viewAngle;

    [SerializeField]
    private float deathZone;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        usedControl = GamePadManager.IsConnected(PlayerIndex.One) ? Control.Controller : Control.Keyboard;
        GamePadManager.Connected += GamePadManager_Connected;
        GamePadManager.Disconnected += GamePadManager_Disconnected;
        switcher.StateChanged += SineStateChanged;
    }

    private void SineStateChanged(SineStateSwitcher.SineState state)
    {
        if(state == SineStateSwitcher.SineState.Low)
        {
            smash.Reset();
            wave.Reset();
        }
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
        Vector2 velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);

        if (velocity.magnitude > deathZone)
        {
            float x = Mathf.Rad2Deg * Mathf.Cos(viewAngle);
            float z = Mathf.Rad2Deg * Mathf.Sin(viewAngle);
            transform.LookAt(transform.position + new Vector3(x, transform.position.y, z));
        }

        Vector2 movement = (usedControl == Control.Controller) ? GamePadManager.ThumbLeft(PlayerIndex.One) : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 move = transform.forward * -movement.y + transform.right * -movement.x;

        rigidbody.AddForce(move * acceleration);


        if (velocity.magnitude > speed)
        {
            rigidbody.velocity = rigidbody.velocity.normalized * speed;
        }

        if (velocity.magnitude < deathZone)
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
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

    public void UseDash()
    {
        if (dash.Usable)
            dash.Use();
    }

    public void UseSmash()
    {
        if (smash.Usable && !smash.Used && !wave.Used && switcher.State == SineStateSwitcher.SineState.High)
            smash.Use();
    }

    public void UseWave()
    {
        if (wave.Usable && !wave.Used && !smash.Used && switcher.State == SineStateSwitcher.SineState.High)
            wave.Use();
    }
}

public enum Control
{
    Controller,
    Keyboard
}