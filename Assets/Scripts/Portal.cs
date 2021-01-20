using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            GameController.getInstance().lose();
            transform.GetChild(2).GetComponent<ParticleSystem>().Emit(1);
            other.gameObject.GetComponent<Enemy>().overkill();
        }
    }

}
