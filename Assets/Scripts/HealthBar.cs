using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private Player target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("ScriptManager").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(target.getHealth());
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
