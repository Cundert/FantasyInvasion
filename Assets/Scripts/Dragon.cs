using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Unit
{
	public GameObject fireball;

	private float animDelay = 0.5f;
	private float animDiffTime = 0.0f;
	private bool attacking = false;
    private bool muriendo = false;

	void spawnFireball() {
		GameObject fireB = Instantiate(fireball, new Vector3(transform.position.x+0.5f,transform.position.y+0.2f,transform.position.z), Quaternion.AngleAxis(90f, new Vector3(0, 0, 1)));
		fireB.transform.parent=GameController.getInstance().transform;
        fireB.GetComponent<Fireball>().attack = attack;
	}

	override public void makeAttack(Character objective) {
		if (attackTimeDiff>=attackSpeed) {
			attackTimeDiff-=attackSpeed;
			GetComponent<Animation>().Play("sj001_skill2");
			if (transform.name=="IceDragon(Clone)") SoundController.getInstance().play(SoundController.SoundId.iceballSummon);
			else SoundController.getInstance().play(SoundController.SoundId.fireballSummon);
			GetComponent<Animation>().PlayQueued("sj001_wait");
			attacking=true;
		}
		attackTimeDiff+=Time.deltaTime;
	}

	override public void endOfAttack() {
		spawnFireball();
	}

	override protected void Update() {
        if (!muriendo)
        {
            base.Update();
            if (attacking)
            {
                animDiffTime += Time.deltaTime;
                if (animDiffTime >= animDelay)
                {
                    animDiffTime = 0.0f;
                    attacking = false;
                    spawnFireball();
                }
            }
        }
	}

    override public void takeDamage(int damage)
    {
        if (hp > 0)
        {
            if (damage < 0) damage = 0;
            hp -= damage;
            if (hp <= 0)
            {
                muriendo = true;
                GetComponent<Animation>().Play("sj001_die");
            }
        }
    }

}
