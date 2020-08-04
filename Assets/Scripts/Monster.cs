using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Monster : MonoBehaviour
{
    public GameObject target;

    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;

    public AudioClip spawnClip;
    public AudioClip hitClip;
    public AudioClip dieClip;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera");
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnClip);

        navMeshAgent.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = target.transform.position;
        //Debug.Log(target.transform.position);
    }
}
