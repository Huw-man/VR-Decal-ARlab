using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Gun : MonoBehaviour
{
    AudioSource audioSource;
    private Animator animator;
    private ParticleSystem particleSystem;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = transform.Find("Model").GetComponent<Animator>();
        particleSystem = transform.Find("MuzzleFlashEffect").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        audioSource.PlayOneShot(audioSource.clip);
        animator.SetTrigger("Fire");
        particleSystem.Play();
    }
}
