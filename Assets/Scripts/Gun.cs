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
    AudioSource error_audio;


    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        shoot_audio = audioSources[0];
        reload_audio = audioSources[1];
        error_audio = audioSources[2];
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
            Reload();
        }
    }

    public void Fire()
    {
        if (hasAmmo() && !reloading)
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
        } else {
            Error();
        }
    }

    private bool hasAmmo()
    {
        return ammo > 0;
    }

    private IEnumerator _reload()
    {
        reloading = true;
        // 2 sec animation
        Debug.Log("Start reloading at: " + Time.time);
        reload_audio.PlayOneShot(reload_audio.clip);
        yield return new WaitForSeconds(2);
        Debug.Log("End reloading at: " + Time.time);
        ammo = maxAmmo;
        ammo_bar.SetAmmo(ammo);
        reloading = false;

    }

    public void Reload()
    {
        // second reloading if check required due to reload button
        if (!reloading && ammo < maxAmmo)
        {
            StartCoroutine(_reload());
        } else {
            Error();
        }
    }

    public void Error()
    {
        error_audio.PlayOneShot(error_audio.clip);
    }
}
