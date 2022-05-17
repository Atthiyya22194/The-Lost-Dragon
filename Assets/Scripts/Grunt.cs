using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    public float lookRadius = 10f;
    private FirstPersonController player;
    private Rigidbody rb;
    bool isDead = false;
    Animator anim;
    Transform target;
    NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius && !isDead)
        {
            agent.updatePosition = true;
            agent.SetDestination(target.position);
            anim.SetFloat("Speed", 0f);
            if (distance <= agent.stoppingDistance)
            {
                Kill();
                FaceTarget();
            }
            if (distance >= agent.stoppingDistance)
            {
                FaceTarget();
                anim.SetFloat("Speed", 0.5f);
                anim.SetBool("isAttacking", false);
            }
            else 
            {
                anim.SetFloat("Speed", 0);
            }
        }
    }

    void Kill()
    {
        anim.SetBool("isAttacking", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.gameObject.GetComponent<FirstPersonController>().currentHealth -= 20;
        }
        //FindObjectOfType<SFXmanager>().PlayHurt();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void EnemyDeathAnim()
    {
        anim.SetTrigger("isDead");
    }

    /*
    public void EnemyGetHit()
    {
        anim.SetTrigger("isHit");
    }
    */
}

