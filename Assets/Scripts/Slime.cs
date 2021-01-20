using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public GameObject SlimeMini;
    public int offset;
    override public void die()
    {
        gameObject.layer = 0;
        Instantiate(SlimeMini, new Vector3(transform.position.x-0.1f, transform.position.y, transform.position.z + offset), Quaternion.AngleAxis(-90f, new Vector3(0, 1, 0)));
        Destroy(gameObject);
    }

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
