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

    private Control preferredControl;

    public Control PreferredControl
    {
        get { return preferredControl; }
        set { preferredControl = value; }
    }

    // Update is called once per frame
    void Update()
    {
        GamePadManager.Update();
    }
}
