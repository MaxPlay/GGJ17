using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        timer = new Timer(4);
        instance = this;
    }

    [SerializeField]
    private Control preferredControl;

    public Control PreferredControl
    {
        get { return preferredControl; }
        set { preferredControl = value; }
    }

    [SerializeField]
    private Transform enemiesBase;

    public Transform EnemiesBase
    {
        get { return enemiesBase; }
        set { enemiesBase = value; }
    }

    Timer timer;
    [SerializeField]
    private Spawner spawner;

    void Update()
    {
        GamePadManager.Update();
        if (timer.Percentage == 1)
        {
            timer = new Timer(8);
            spawner.Spawn(10);
        }

        timer.Update();
    }
}
