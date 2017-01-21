using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    SineStateSwitcher switcher;

    private float distanceToPlayer;
    NavMeshAgent agent;

    [SerializeField]
    [Range(0, 10)]
    private float attackRange;

    [SerializeField]
    [Range(0, 10)]
    private float viewRange;

    [SerializeField]
    [Range(1, 10)]
    private float damage;

    [SerializeField]
    [Range(1, 10)]
    private float healthPoints;

    [SerializeField]
    private GameObject target;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switcher.Update();

        agent.SetDestination(target.transform.position);

        distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

        while (distanceToPlayer > viewRange)
        {
            agent.speed = 0;
        }
    }
}
