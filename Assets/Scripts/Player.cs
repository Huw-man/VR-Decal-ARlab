using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    public enum State
    {
        ALIVE, DEAD
    }

    public State playerState = State.ALIVE;

    public int maxHealth;

    private int health;

    public int maxShield;

    public int rechargeDelay;

    private int shield;


    private HealthBar healthbar;
    private ShieldBar shieldbar;

    private float start;
    private float now;

    private bool recharging;

    private IEnumerator coroutine;

    private AudioSource[] audioSources;
    private AudioSource audioSingle;
    private AudioSource audioLoop;

    public AudioClip shieldHit;
    public AudioClip healthHit;
    public AudioClip shieldLow;
    public AudioClip shieldDepleted;
    public AudioClip shieldRecharge;

    public int getHealth()
    {
        return health;
    }

    void Start()
    {
        health = maxHealth;
        shield = maxShield;
        healthbar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        shieldbar = GameObject.Find("Shield Bar").GetComponent<ShieldBar>();
        start = Time.time;

        audioSources = GetComponents<AudioSource>();
        audioSingle = audioSources[0];
        audioLoop = audioSources[1];
        audioLoop.loop = true;

    }

    // Update is called once per frame
    void Update()
    {
        now = Time.time;
        if ((now - start) > rechargeDelay && !recharging)
        {
            start = now;
            coroutine = _recharge();
            StartCoroutine(coroutine);
        }
    }

    public void Hurt(int damage)
    {
        if (playerState == State.ALIVE)
        {
            start = Time.time;
            if (recharging)
            {
                StopCoroutine(coroutine);
                recharging = false;
                audioLoop.Stop();
            }
            if (shield > 0)
            {
                shield = Math.Max(0, shield - damage);
                shieldbar.SetShield(shield);
                audioSingle.PlayOneShot(shieldHit);
                if (shield == 0)
                {
                    audioLoop.clip = shieldDepleted;
                    audioLoop.Play();
                } else if (shield <= maxShield / 3)
                {
                    audioLoop.clip = shieldLow;
                    audioLoop.Play();
                }

            } else {
                health -= damage;
                healthbar.SetHealth(health);
                audioSingle.PlayOneShot(healthHit);
                if (health <= 0)
                {
                    Die();
                }
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private IEnumerator _recharge()
    {
        recharging = true;
        audioLoop.clip = shieldRecharge;
        audioLoop.Play();
        for (int s = shield; s <= maxShield; s += 1)
        {
            shield = s;
            shieldbar.SetShield(s);
            yield return new WaitForSeconds(0.25f);
        }
        audioLoop.Stop();
        recharging = false;
    }
}
