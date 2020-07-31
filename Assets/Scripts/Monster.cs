using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Monster : MonoBehaviour
{
    public GameObject target;
    ARSessionOrigin m_sessionOrigin;

    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;

    public AudioClip spawnClip;
    public AudioClip hitClip;
    public AudioClip dieClip;

    // Start is called before the first frame update
    void Start()
    {
        m_sessionOrigin = GetComponent<ARSessionOrigin>();
        target = GameObject.FindWithTag("Respawn");
        target.transform.position = new Vector3(0, 0, 0);
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnClip);
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }
}
