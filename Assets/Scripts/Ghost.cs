using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{

    // Update is called once per frame
    protected override void Update()
    {
        checkFrost();
        if (target) makeAttack(target);
        else
        {
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Unit");
            Ray leftRay = new Ray(transform.position, new Vector3(-1, 0, 0));
            if (Physics.Raycast(leftRay, out hit, rangeOfAggro, mask) && hit.transform.gameObject.name != "Knight(Clone)" && hit.transform.gameObject.name != "Treasure(Clone)")
            {
                Character enem = hit.transform.gameObject.GetComponent<Character>();
                changeFocus(enem);
                attackTimeDiff = attackSpeed / 2.0f;
            }
            else
            {
                float despl = Time.deltaTime * speed * speedModifier;
                transform.Translate(new Vector3(0, 0, despl));
            }
        }
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
