﻿using System.Collections;
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

    private Score ScoreBoard;

    public enum State
    {
        ALIVE, DYING, SINKING
    }

    public State monsterState = State.ALIVE;
    public int maxHealth;
    private int currHealth;

    public float sinkSpeed;

    Collider hitbox;

    private string difficulty;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnClip);
        animator = GetComponent<Animator>();
        target = GameObject.Find("ScriptManager").GetComponent<Player>();
        currHealth = maxHealth;
        ScoreBoard = GameObject.Find("Score").GetComponent<Score>();
        hitbox = GetComponent<Collider>();
        difficulty = PlayerPrefs.GetString("difficulty", "medium");
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterState == State.ALIVE)
        {
            Vector3 targetPos = GameObject.FindWithTag("MainCamera").transform.position;
            navMeshAgent.SetDestination(targetPos);
            Vector3 distanceVector = transform.position - targetPos;
            distanceVector.y = 0;
            float distance = distanceVector.magnitude;

            if (distance <= attackRange)
            {
                animator.SetBool("Attack", true);
            }
        } else if (monsterState == State.SINKING)
        {
            float sinkDistance = sinkSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, -sinkDistance, 0));
        }
    }

    public void Attack()
    {
        target.Hurt(damage);
        Debug.Log(target.getHealth());
        audioSource.PlayOneShot(hitClip);
    }

    public void Hurt(int damage)
    {
        if (monsterState == State.ALIVE)
        {
            animator.SetTrigger("Hurt");
            currHealth -= damage;
            if (currHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        monsterState = State.DYING;
        audioSource.PlayOneShot(dieClip);
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Dead");
        // Can base # of points gained off of difficulty multipliers
        if (difficulty == "easy")
        {
            ScoreBoard.AddPoints(1);
        }
        else if (difficulty == "medium")
        {
            ScoreBoard.AddPoints(2);
        }
        else if (difficulty == "hard")
        {
            ScoreBoard.AddPoints(4);
        }
        hitbox.enabled = false;
    }

    public void StartSinking()
    {
        monsterState = State.SINKING;
        navMeshAgent.enabled = false;
        Destroy(gameObject, 5);
    }
}