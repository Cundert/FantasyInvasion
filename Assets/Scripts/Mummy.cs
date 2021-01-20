using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : Enemy
{
    override protected void frostModel()
    {
        transform.GetChild(1).GetComponent<Renderer>().material = frosted;
        transform.GetChild(2).GetComponent<Renderer>().material = frosted;
        transform.GetChild(3).GetComponent<Renderer>().material = frosted;
        transform.GetChild(4).GetComponent<Renderer>().material = frosted;
    }

    override protected void baseModel()
    {
        transform.GetChild(1).GetComponent<Renderer>().material = baseMat;
        transform.GetChild(2).GetComponent<Renderer>().material = baseMat;
        transform.GetChild(3).GetComponent<Renderer>().material = baseMat;
        transform.GetChild(4).GetComponent<Renderer>().material = baseMat;
    }

    override protected void setBaseModel()
    {
        baseMat = transform.GetChild(1).GetComponent<Renderer>().material;
    }
}
