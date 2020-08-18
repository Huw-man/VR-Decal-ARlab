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

    public int damage;


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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        
        audioSource.PlayOneShot(audioSource.clip);
        animator.SetTrigger("Fire");
        particleSystem.Play();

        RaycastHit hit;
        GameObject camera = GameObject.FindWithTag("MainCamera");
        Vector3 origin = camera.transform.position;
        Vector3 direction = camera.transform.forward;
        if (Physics.Raycast(origin, direction, out hit, 100f))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Monster"))
            {
                Monster monsterScript = hitObject.GetComponent<Monster>();
                monsterScript.Hurt(damage);
            }
        }
    }
}
