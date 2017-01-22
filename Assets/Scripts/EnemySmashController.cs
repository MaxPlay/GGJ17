using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmashController : MonoBehaviour {

    private List<EnemyBehaviour> list_enemies = new List<EnemyBehaviour>();

    private void OnTriggerEnter(Collider other)
    {
        EnemyBehaviour enemy =  other.GetComponent<EnemyBehaviour>();

        if (enemy != null)
        {
            list_enemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();

        if (enemy != null)
        {
            list_enemies.Remove(enemy);
        }
    }

    private void Update()
    {
        if(list_enemies.Count > 0)
        {
            attackAllEnemies();
        }
    }

    public void attackAllEnemies()
    {
        //winkel richtig: hauen
        foreach (EnemyBehaviour enemy in list_enemies)
        {
            Vector3 targetDir = enemy.getPosition() - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            if (angle > 45 || angle < 135)
            {
                enemy.beingHitDamage();

            }
        }
    }



}
