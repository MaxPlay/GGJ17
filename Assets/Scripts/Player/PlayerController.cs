using UnityEngine;
using XInputDotNetPure;

public enum Control
{
    Controller,
    Keyboard
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collector))]
public class PlayerController : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private float acceleration;

    private Collector collector;

    [SerializeField]
    private Dash dash = new Dash();

    [SerializeField]
    private float deathZone;

    private new Rigidbody rigidbody;

    private int selectedItem;

    [SerializeField]
    private Smash smash = new Smash();

    [SerializeField]
    private float speed;

    [SerializeField]
    private SineStateSwitcher switcher = new SineStateSwitcher();

    [SerializeField]
    private Control usedControl;

    [SerializeField]
    private float viewAngle;

    [SerializeField]
    private Wave wave = new Wave();

    #endregion Private Fields

    #region Public Properties

    public float CameraDirection
    {
        get { return viewAngle; }
        set { viewAngle = value; }
    }

    public Dash Dash { get { return dash; } }
    public float SineValue { get { return switcher.Value; } }
    public Smash Smash { get { return smash; } }
    public Control UsedControl { get { return usedControl; } }
    public Wave Wave { get { return wave; } }
    public SineStateSwitcher Switcher { get { return switcher; } }
    public int SelectedItem { get { return selectedItem; } }

    #endregion Public Properties

    #region Public Methods

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

    #endregion Public Methods

    #region Private Methods

    private void Awake()
    {
        usedControl = GamePadManager.IsConnected(PlayerIndex.One) ? Control.Controller : Control.Keyboard;
        GamePadManager.Connected += GamePadManager_Connected;
        GamePadManager.Disconnected += GamePadManager_Disconnected;
        switcher.StateChanged += SineStateChanged;
    }

    private void FixedUpdate()
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
            rigidbody.velocity = rigidbody.velocity.normalized * speed;

        if (velocity.magnitude < deathZone)
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
    }

    private void GamePadManager_Connected(GamePadManager.GamePadEventArgs e)
    {
        usedControl = GameManager.Instance.PreferredControl;
    }

    private void GamePadManager_Disconnected(GamePadManager.GamePadEventArgs e)
    {
        usedControl = Control.Keyboard;
    }

    private void SineStateChanged(SineStateSwitcher.SineState state)
    {
        if (state == SineStateSwitcher.SineState.Low)
        {
            smash.Reset();
            wave.Reset();
        }
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collector = GetComponent<Collector>();
    }

    private void Update()
    {
        switcher.Update();

        if (GamePadManager.ButtonPressed(PlayerIndex.One, GamePadManager.ButtonType.LeftShoulder))
            selectedItem = selectedItem == collector.Bag.Length ? 0 : selectedItem + 1;

        if (GamePadManager.ButtonPressed(PlayerIndex.One, GamePadManager.ButtonType.LeftShoulder))
            selectedItem = selectedItem == 0 ? collector.Bag.Length - 1 : selectedItem - 1;
    }

    #endregion Private Methods
}