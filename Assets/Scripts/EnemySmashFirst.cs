using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmashFirst : MonoBehaviour {


    List<GameObject> list_enemies = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            list_enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        list_enemies.Remove(other.gameObject);
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (list_enemies.Count > 0)
        {
            attackAllEnemies();
        }
    }

    public void attackAllEnemies()
    {
        //winkel richtig: hauen
        foreach (GameObject enemy in list_enemies)
        {
            Vector3 targetDir = enemy.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            print("angle: " + angle);

            if(angle > 45 || angle < 135)
            {
                enemy.SetActive(false);
               
            }
        }

        

    }
}
