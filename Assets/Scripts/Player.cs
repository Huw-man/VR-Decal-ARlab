using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum State
    {
        ALIVE, DEAD
    }

    public State playerState = State.ALIVE;

    public int maxHealth;

    private int health;

    public int getHealth()
    {
        return health;
    }

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hurt(int damage)
    {
        if (playerState == State.ALIVE)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
