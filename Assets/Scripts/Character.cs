using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int hp;
    public int attack;
    public float attackSpeed;
    public float rangeOfAggro;
    public bool alive = true;
    protected float attackTimeDiff;
    protected Character target = null;
    protected float speedModifier = 1.0f;

    public virtual void die()
    {
        Debug.Log("Comportamiento de funcion die no definido");
    }

    virtual public void takeDamage(int damage)
    {
        if (hp > 0)
        {
            if (damage < 0) damage = 0;
            hp -= damage;
            if (hp <= 0)
            {
                if (gameObject.GetComponent<Animator>() != null) gameObject.GetComponent<Animator>().SetTrigger("die");
                else die();
            }
        }
    }

    protected void unbond()
    {
        gameObject.layer = 0;
        alive = false;
    }

    public virtual void changeFocus(Character e)
    {
        if (!target) {
            target = e;

            }
    }

    virtual public void makeAttack(Character objective)
    {
        if (objective.isActiveAndEnabled)
        {
            if (attackTimeDiff >= attackSpeed)
            {
                attackTimeDiff -= attackSpeed;
                gameObject.GetComponent<Animator>().SetTrigger("attack");
                playAttackSound();
            }
            attackTimeDiff += Time.deltaTime * speedModifier;
            
        }
    }

    virtual public void endOfAttack()
    {
        if (target)
        {
            target.takeDamage(attack);
        }
    }

    virtual protected void playAttackSound()
    {
        SoundController.getInstance().play(SoundController.SoundId.attack);
    }

    protected virtual void Update()
    {
        if (target && target.alive) makeAttack(target);
        else
        {
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Enemy");
            Ray leftRay = new Ray(transform.position, new Vector3(1, 0, 0));
            if (Physics.Raycast(leftRay, out hit, rangeOfAggro, mask))
            {
                Character enem = hit.transform.gameObject.GetComponent<Character>();
                changeFocus(enem);
                attackTimeDiff = attackSpeed / 2.0f;
            }
        }
    }
}
