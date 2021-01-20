using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Unit
{
    protected override void playAttackSound()
    {
        SoundController.getInstance().play(SoundController.SoundId.swordAttack);
    }

    override protected void Update()
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
                if (hit.transform.gameObject.name != "GhostViolet(Clone)")
                {
                    changeFocus(enem);
                    attackTimeDiff = attackSpeed / 2.0f;
                }
            }
        }
    }
}
