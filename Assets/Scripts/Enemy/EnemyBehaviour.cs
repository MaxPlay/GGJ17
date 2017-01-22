using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{


    private float distanceToTarget;
    private float localRadius;
    private float targetRadius;
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
        localRadius = GetComponent<SphereCollider>().radius;
        targetRadius = target.GetComponent<CapsuleCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        
        //Moves Actor to Target, but NOT into the Target ( -(loc and targ radius) )    
        agent.SetDestination(Vector3.MoveTowards(transform.position, target.transform.position, distanceToTarget - (localRadius + targetRadius)));
        
        if (distanceToTarget > viewRange)
        {
            agent.speed = 0;
        }
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, target.transform.position.z));


    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRange);
        Gizmos.color = Color.red;
        Ray r = new Ray();
        r.direction = target.transform.position - transform.position;
        r.origin = transform.position;
        Gizmos.DrawRay(r);
    }
    
    public Vector3 getPosition()
    {
        return transform.position;
    }

    public void Freeze()
    {

    }

    public void Push(Vector3 origin)
    {

    }

    public void Kill()
    {

    }

    public void Damage(float damage)
    {
        
    }
}
