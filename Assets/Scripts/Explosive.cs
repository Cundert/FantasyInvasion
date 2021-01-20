using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Unit
{
    private bool wakingUp = false;
    private bool idleMode = false;

    public GameObject explosion;

    override public void makeAttack(Character enemy)
    {
        if (idleMode)
        {
            GetComponent<Animation>().Play("Anim_Attack");
            
            
            //playAttackSound();
            //die();
        }
    }

    protected override void playAttackSound()
    {
        SoundController.getInstance().play(SoundController.SoundId.explosion);
    }

    private void explotar()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] enemies = Physics.OverlapSphere(transform.position, rangeOfAggro * 1.5f, mask);
        for (int i = 0; i < enemies.Length; ++i) enemies[i].gameObject.GetComponent<Enemy>().overkill();
    }

    override protected void Start()
    {
		base.Start();
		attackTimeDiff = 0;
        Animation anim = GetComponent<Animation>();
    }

    protected override void Update()
    {
        attackTimeDiff += Time.deltaTime;
        if (!wakingUp && attackTimeDiff >= attackSpeed)
        {
            Animation anim = GetComponent<Animation>();
            anim["Anim_Death"].time = anim["Anim_Death"].length;
            anim["Anim_Death"].speed = -1;
            anim.Play("Anim_Death");
            wakingUp = true;
        }
        else if (wakingUp && !idleMode && GetComponent<Animation>()["Anim_Death"].time == 0)
        {
            GetComponent<Animation>().Play("Anim_Idle");
            idleMode = true;
        }
        if (idleMode) base.Update();
    }
}
