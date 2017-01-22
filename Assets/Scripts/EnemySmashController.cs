using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmashController : MonoBehaviour {

    [SerializeField]
    private int Damage;

    [SerializeField]
    private List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();

    private void OnTriggerEnter(Collider other)
    {
        EnemyBehaviour enemy =  other.GetComponent<EnemyBehaviour>();

        if (enemy != null)
        {
            enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();

        if (enemy != null)
        {
            enemies.Remove(enemy);
        }
    }

    //private void Update()
    //{
    //    if(list_enemies.Count > 0)
    //    {
    //        attackAllEnemies();
    //    }
    //}

    public void attackAllEnemies()
    {
        //winkel richtig: hauen
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Vector3 targetDir = enemies[i].transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            // if (angle > 45 || angle < 135)
            {
                if (enemies[i].Damage(Damage))
                    enemies.RemoveAt(i);

            }
        }
    }



}
