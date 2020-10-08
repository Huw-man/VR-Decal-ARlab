using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider slider;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        // slider = GetComponent<Slider>();
        // player = GameObject.Find("ScriptManager").GetComponent<Player>();
        // slider.maxValue = player.maxShield;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetShield(int health)
    {
        slider.value = health;
    }
}
