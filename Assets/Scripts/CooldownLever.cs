using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownLever : MonoBehaviour
{
    private float hpMax;
    private Lever lever;
    private Image circle;

    private void Start()
    {
        lever = transform.parent.transform.parent.GetComponent<Lever>();
        hpMax = lever.cooldown;
        circle = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        circle.fillAmount = (float)lever.timeDiff / hpMax;
    }
}
