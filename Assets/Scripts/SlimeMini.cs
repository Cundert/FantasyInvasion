using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMini : Enemy
{
    protected override void playAttackSound()
    {
        SoundController.getInstance().play(SoundController.SoundId.slimeAttack);
    }

    override protected void frostModel()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = frosted;
    }

    override protected void baseModel()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material = baseMat;
    }

    override protected void setBaseModel()
    {
        baseMat = transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material;
    }
}
