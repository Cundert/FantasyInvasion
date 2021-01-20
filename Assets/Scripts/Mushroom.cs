using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mushroom : Enemy
{
    private bool muriendo = false;
    // Update is called once per frame
    override protected void Update()
    {
        if (!muriendo)
        {
            delayedDeathBehaviour();
            if (target) makeAttack(target);
            else
            {
                RaycastHit hit;
                LayerMask mask = LayerMask.GetMask("Unit");
                Ray leftRay = new Ray(transform.position, new Vector3(-1, 0, 0));
                if (Physics.Raycast(leftRay, out hit, rangeOfAggro, mask))
                {
                    Character enem = hit.transform.gameObject.GetComponent<Character>();
                    changeFocus(enem);
                    attackTimeDiff = attackSpeed / 2.0f;
                }
                else
                {
                    GetComponent<Animation>().Play("Run");
                    float despl = Time.deltaTime * speed;
                    transform.Translate(new Vector3(0, 0, despl));
                }
            }
        }
    }

    override public void makeAttack(Character objective)
    {
        if (objective.isActiveAndEnabled)
        {
            if (attackTimeDiff >= attackSpeed)
            {
                attackTimeDiff -= attackSpeed;
                GetComponent<Animation>().Play("Attack");
                playAttackSound();
            }
            else GetComponent<Animation>().PlayQueued("Idle");
            attackTimeDiff += Time.deltaTime * speedModifier;
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
                //GetComponent<Animation>().RemoveClip("Run");
                GetComponent<Animation>().Play("Death");
            }
        }
    }

    override protected void setBaseModel()
    {
        // La seta es inmune a la congelacion, por lo que no necesita asignar ningun modelo
    }

    override protected void baseModel()
    {
        // La seta es inmune a la congelacion, por lo que no necesita modificar el modelo base
    }

    override protected void frostModel()
    {
        // La seta es inmune a la congelacion, por lo que no necesita modificar el modelo congelado
    }
}
