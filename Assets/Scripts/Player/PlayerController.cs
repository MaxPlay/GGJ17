using System.Collections.Generic;
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

    [SerializeField]
    private int activePowerup;

    private Collector collector;

    [SerializeField]
    private Dash dash = new Dash();

    [SerializeField]
    private float deathZone;

    [SerializeField]
    private List<EnemyBehaviour> enemiesInRange;

    private Timer powerupTimer;
    private new Rigidbody rigidbody;

    [SerializeField]
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

    #region Public Delegates

    public delegate void ItemRemovedEventHandler(int selectedItem);

    #endregion Public Delegates

    #region Public Events

    public event ItemRemovedEventHandler ItemRemoved;

    #endregion Public Events

    #region Public Properties

    public int ActivePowerup { get { return activePowerup; } }

    public float CameraDirection
    {
        get { return viewAngle; }
        set { viewAngle = value; }
    }

    public Dash Dash { get { return dash; } }
    public Timer PowerupTimer { get { return powerupTimer; } }
    public int SelectedItem { get { return selectedItem; } }
    public float SineValue { get { return switcher.Value; } }
    public Smash Smash { get { return smash; } }
    public SineStateSwitcher Switcher { get { return switcher; } }
    public Control UsedControl { get { return usedControl; } }
    public Wave Wave { get { return wave; } }

    #endregion Public Properties

    #region Public Methods

    public void DamagePlayer()
    {
        if (activePowerup > -1 && collector.Bag[activePowerup] == PowerUps.Invincibility)
            return;
    }

    public void OnTriggerEnter(Collider other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy != null)
            enemiesInRange.Add(enemy);
    }

    public void OnTriggerExit(Collider other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy != null)
            enemiesInRange.Remove(enemy);
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

    #endregion Public Methods

    #region Private Methods

    private void Awake()
    {
        usedControl = GamePadManager.IsConnected(PlayerIndex.One) ? Control.Controller : Control.Keyboard;
        GamePadManager.Connected += GamePadManager_Connected;
        GamePadManager.Disconnected += GamePadManager_Disconnected;
        switcher.StateChanged += SineStateChanged;
    }

    private void DropItem()
    {
        if (collector.Bag[selectedItem] == PowerUps.None)
            return;

        OnItemRemoved(selectedItem);
        GameObject newItem = PrefabManager.Instance.SpawnPowerup(collector.Bag[selectedItem], transform.position + transform.forward * 2);
        Rigidbody rigidbody = newItem.GetComponent<Rigidbody>();
        if (rigidbody != null)
            rigidbody.AddForce(transform.forward * 2);
        collector.Bag[selectedItem] = PowerUps.None;
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

    private void OnItemRemoved(int selectedItem)
    {
        if (ItemRemoved != null)
            ItemRemoved(selectedItem);
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

        if (GamePadManager.ButtonPressed(PlayerIndex.One, GamePadManager.ButtonType.A))
            UseItem();

        if (GamePadManager.ButtonPressed(PlayerIndex.One, GamePadManager.ButtonType.B))
            DropItem();

        if (activePowerup > -1)
            if (powerupTimer.Update())
            {
                OnItemRemoved(activePowerup);
                activePowerup = -1;
            }
    }

    private void UseItem()
    {
        if (collector.Bag[selectedItem] == PowerUps.None)
            return;

        switch (collector.Bag[selectedItem])
        {
            case PowerUps.Freeze:
                EnemyBehaviour[] enemies = GameManager.Instance.EnemiesBase.GetComponentsInChildren<EnemyBehaviour>();
                foreach (EnemyBehaviour enemy in enemies)
                {
                    enemy.Freeze();
                }
                powerupTimer = new Timer(5f);
                break;

            case PowerUps.Waveskip:
                switcher.Waveskip(5f);
                powerupTimer = new Timer(1f);
                break;

            case PowerUps.Invincibility:
            case PowerUps.DoubleDamage:
            case PowerUps.RageBurst:
                powerupTimer = new Timer(5f);
                break;

            case PowerUps.Raydestruct:
                RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, float.PositiveInfinity);
                for (int i = 0; i < hits.Length; i++)
                {
                    EnemyBehaviour enemy = hits[i].transform.GetComponent<EnemyBehaviour>();
                    if (enemy != null)
                        enemy.Kill();
                }
                powerupTimer = new Timer(2f);
                break;

            case PowerUps.Push:
                foreach (EnemyBehaviour enemy in enemiesInRange)
                {
                    enemy.Push(transform.position);
                }
                powerupTimer = new Timer(5f);
                break;
        }

        activePowerup = selectedItem;
    }

    #endregion Private Methods
}