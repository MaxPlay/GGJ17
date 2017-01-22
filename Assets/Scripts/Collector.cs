using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    [SerializeField]
    private int limit;

    [SerializeField]
    private PowerUps[] bag;

    public PowerUps[] Bag { get { return bag; } }

    void Start()
    {
        bag = new PowerUps[limit];
        for (int i = 0; i < bag.Length; i++)
            bag[i] = PowerUps.None;
    }

    public event EventHandler Collect;

    private void OnTriggerEnter(Collider other)
    {
        PickUpRotator collectable = other.GetComponent<PickUpRotator>();
        if (collectable != null)
        {
            for (int i = 0; i < bag.Length; i++)
                if (bag[i] == PowerUps.None)
                {
                    bag[i] = collectable.Type;
                    Destroy(collectable.gameObject);
                    OnCollect();
                    return;
                }
        }
    }

    private void OnCollect()
    {
        if (Collect != null)
            Collect(this, new EventArgs());
    }
}
