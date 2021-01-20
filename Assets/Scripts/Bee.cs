using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{

    override protected void frostModel()
    {
        transform.GetChild(1).GetComponent<Renderer>().material = frosted;
    }

    override protected void baseModel()
    {
        transform.GetChild(1).GetComponent<Renderer>().material = baseMat;
    }

    override protected void setBaseModel()
    {
        baseMat = transform.GetChild(1).GetComponent<Renderer>().material;
    }
}
