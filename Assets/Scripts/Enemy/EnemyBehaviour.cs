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
    [Range(1, 20)]
    private float healthPoints;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private EnemeyanimaAnimator animatorInterface;

    private Timer forcedMovementTimer;
    [SerializeField]
    private Rigidbody rig;

    private SineWaves targetSinWaves;

    // Use this for initialization
    void Start()
    {
        //animatorInterface = GetComponent<EnemeyanimaAnimator>();
        target = GameObject.Find("Sine");
        targetSinWaves = target.GetComponent<SineWaves>();
        agent = GetComponent<NavMeshAgent>();
        localRadius = GetComponent<CapsuleCollider>().radius;
        targetRadius = target.GetComponent<CapsuleCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        //Moves Actor to Target, but NOT into the Target ( -(loc and targ radius) )    
        if (agent.enabled)
            agent.SetDestination(Vector3.MoveTowards(transform.position, target.transform.position, distanceToTarget - (localRadius + targetRadius)));

        if (distanceToTarget > viewRange)
        {
            //agent.speed = 1;
        }

        if (distanceToTarget <= attackRange)
        {
            if (targetSinWaves.Sin > 0.9)
            {
                animatorInterface.OnStomp();
            }
            else
            {
                animatorInterface.OnPunch();
            }

        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z)), 1.5f * Time.deltaTime);
        if (healthPoints < 0)
            Kill();

        agent.speed = 10;

        animatorInterface.Movement = 1.5f * agent.velocity.magnitude / agent.speed;

        if (forcedMovementTimer != null)
        {
            if (forcedMovementTimer.Update())
            {
                agent.enabled = true;
                rig.isKinematic = false;

            }
            else
            {
                agent.enabled = false;
                rig.isKinematic = true;
            }
        }
    }


    PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            player = null;
    }
    void EventStomp()
    {
        if (player != null)
            player.Damage(damage);
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
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            var child= transform.GetChild(i);



            child.parent = null;
            child.gameObject.AddComponent<AutoDestroy>();
            var child_rigrid = child.gameObject.AddComponent<Rigidbody>();
            var collider= child.gameObject.AddComponent<MeshCollider>();

            var meshfilter = child.GetComponent<SkinnedMeshRenderer>();
            if(meshfilter!=null)
            {
                collider.convex = true;
                collider.sharedMesh = child.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            }
            child_rigrid.AddForce((UnityEngine.Random.insideUnitSphere) * 500.0f);
           
        }
        Destroy(transform.gameObject);
    }

    public bool Damage(float damage)
    {
        //Fake
        forcedMovementTimer = new Timer(3);
        rig.AddForce((target.transform.position - transform.position).normalized * damage);
        animatorInterface.OnHit();
        healthPoints -= damage;
        return healthPoints < 0;
    }
}
