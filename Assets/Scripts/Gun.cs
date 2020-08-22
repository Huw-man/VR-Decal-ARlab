using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Gun : MonoBehaviour
{
    AudioSource[] audioSources;
    private Animator animator;
    private ParticleSystem particleSystem;

    public int damage;
    public int maxAmmo;
    private int ammo;
    private bool reloading;

    private AmmoBar ammo_bar;

    AudioSource shoot_audio;
    AudioSource reload_audio;


    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        shoot_audio = audioSources[0];
        reload_audio = audioSources[1];
        animator = transform.Find("Model").GetComponent<Animator>();
        particleSystem = transform.Find("MuzzleFlashEffect").GetComponent<ParticleSystem>();
        ammo = maxAmmo;
        reloading = false;
        ammo_bar = GameObject.FindWithTag("AmmoBar").GetComponent<AmmoBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAmmo() && !reloading)
        {
            reload();
        }
    }

    public void Fire()
    {
        if (hasAmmo())
        {
            shoot_audio.PlayOneShot(shoot_audio.clip);
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
            ammo -= 1;
            ammo_bar.SetAmmo(ammo);
        }
    }

    private bool hasAmmo()
    {
        return ammo > 0;
    }

    private IEnumerator _reload()
    {
        if (ammo < maxAmmo)
        {
            reloading = true;
            // animation for 2 secs
            Debug.Log("Start reloading at: " + Time.time);
            reload_audio.PlayOneShot(reload_audio.clip);
            yield return new WaitForSeconds(2);
            Debug.Log("End reloading at: " + Time.time);
            ammo = maxAmmo;
            ammo_bar.SetAmmo(ammo);
            reloading = false;

        }
    }

    public void reload()
    {
        StartCoroutine(_reload());
    }
}
