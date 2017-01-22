using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    private static PrefabManager instance;
    public static PrefabManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }

    [SerializeField]
    private GameObject[] powerups;

    public GameObject SpawnPowerup(PowerUps powerup, Vector3 position)
    {
        if (powerup == PowerUps.None)
            return null;

        if (powerups.Length <= (int)powerup)
            return null;

        return Instantiate(powerups[(int)powerup], position, Quaternion.identity);
    }
}
