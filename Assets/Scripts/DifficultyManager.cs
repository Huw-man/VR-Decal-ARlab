using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DifficultyManager : MonoBehaviour
{

    public float easyMultiplier;

    public float mediumMultiplier;

    public float hardMultiplier;

    // Could be based on difficulty
    public float ramp;

    SpawnManager spawns;

    public GameObject monsterPrefab;

    NavMeshAgent monsterNav;

    Monster monsterStats;

    private float start;
    private float now;

    // Could be configurable public field or based on difficulty
    private float interval;

    private int maxRamps = 3;
    private int countRamps;

    string difficulty;

    // Start is called before the first frame update
    void Start()
    {
        start = Time.time;
        interval = 120f;
        spawns = GameObject.Find("ScriptManager").GetComponent<SpawnManager>();
        monsterNav = monsterPrefab.GetComponent<NavMeshAgent>();
        monsterStats = monsterPrefab.GetComponent<Monster>();
        difficulty = PlayerPrefs.GetString("difficulty", "medium");
        AdjustSpawnRate();
        AdjustSpeed();
        AdjustHealth();
        AdjustDamage();
        countRamps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        now = Time.time;
        if ((now - start) >= interval && countRamps < maxRamps) {
            start = now;
            spawns.spawnTime /= ramp;
            monsterNav.speed *= ramp;
            monsterStats.maxHealth = (int) (monsterStats.maxHealth * ramp);
            monsterStats.damage *= (int) (monsterStats.damage * ramp);
            countRamps += 1;
        }
    }

    // Should generalize these

    private void AdjustSpawnRate()
    {
        if (difficulty == "easy")
        {
            spawns.spawnTime /= easyMultiplier;
        }
        else if (difficulty == "medium")
        {
            spawns.spawnTime /= mediumMultiplier;
        }
        else if (difficulty == "hard")
        {
            spawns.spawnTime /= hardMultiplier;
        }
    }

    private void AdjustSpeed()
    {
        if (difficulty == "easy")
        {
            monsterNav.speed *= easyMultiplier;
        }
        else if (difficulty == "medium")
        {
            monsterNav.speed *= mediumMultiplier;
        }
        else if (difficulty == "hard")
        {
            monsterNav.speed *= hardMultiplier;
        }
    }

    private void AdjustHealth()
    {
        if (difficulty == "easy")
        {
            monsterStats.maxHealth = (int) (monsterStats.maxHealth * easyMultiplier);
        }
        else if (difficulty == "medium")
        {
            monsterStats.maxHealth = (int) (monsterStats.maxHealth * mediumMultiplier);
        }
        else if (difficulty == "hard")
        {
            monsterStats.maxHealth = (int) (monsterStats.maxHealth * hardMultiplier);
        }
    }

    private void AdjustDamage()
    {
        if (difficulty == "easy")
        {
            monsterStats.damage = (int) (monsterStats.damage * easyMultiplier);
        }
        else if (difficulty == "medium")
        {
            monsterStats.damage = (int) (monsterStats.damage * mediumMultiplier);
        }
        else if (difficulty == "hard")
        {
            monsterStats.damage = (int) (monsterStats.damage * hardMultiplier);
        }
    }

}
