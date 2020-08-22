using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // private Player player;

    // Start is called before the first frame update
    void Start()
    {
        // slider = GetComponent<Slider>();
        // player = GameObject.Find("ScriptManager").GetComponent<Player>();
        // player = ((GameObject)Resources.Load("../Prefabs/Environment", typeof(GameObject))).GetComponent<Player>();
        // slider.maxValue = player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
