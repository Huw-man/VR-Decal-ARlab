using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public Slider slider;

    private Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<Gun>();
        slider.maxValue = gun.maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetAmmo(int ammo)
    {
        slider.value = ammo;
    }
}
