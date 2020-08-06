using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public float attackRange;

    public int damage;
    private Player target;

    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private Animator animator;

    public AudioClip spawnClip;
    public AudioClip hitClip;
    public AudioClip dieClip;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnClip);
        animator = GetComponent<Animator>();
        target = GameObject.Find("ScriptManager").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(GameObject.Find("AR Camera").transform.position);

        Vector3 distanceVector = transform.position - GameObject.Find("AR Camera").transform.position;
        distanceVector.y = 0;
        float distance = distanceVector.magnitude;

        if (distance <= attackRange)
        {
            animator.SetBool("Attack", true);
        }
    }

    public void Attack()
    {
        target.Hurt(damage);
        Debug.Log(target.getHealth());
        audioSource.PlayOneShot(hitClip);
    }
}