using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCircleBar : MonoBehaviour
{
    private int hpMax;
    private Character chara;
    private Image circle;

    private void Start()
    {
        chara = transform.parent.transform.parent.transform.parent.GetComponent<Character>();
        hpMax = chara.hp;
        circle = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        circle.fillAmount = (float)chara.hp / (float)hpMax;
    }
}
