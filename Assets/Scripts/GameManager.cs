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

    void Update()
    {
        GamePadManager.Update();
    }
}
