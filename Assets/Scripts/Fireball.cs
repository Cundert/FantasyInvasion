using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	public float speed;
    public bool ralentiza;
    public int attack;
    public GameObject fireballExplosion;
    private bool morir = false;
    private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag=="Enemy") {
            if (!morir)
            {
				Instantiate(fireballExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                other.gameObject.GetComponent<Enemy>().takeDamage(attack);
				if (ralentiza) {
					other.gameObject.GetComponent<Enemy>().slowmo(7, 0.75f);
					SoundController.getInstance().play(SoundController.SoundId.iceballExplosion);
				} else SoundController.getInstance().play(SoundController.SoundId.fireballExplosion);
				morir = true;
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (!morir)
        {
            float despl = Time.deltaTime * speed;
            transform.Translate(new Vector3(0, -despl, 0));
            if (transform.position.x >= 15) Destroy(gameObject);
        }
        else
        {
            float tam = Time.deltaTime * 1.5f;
            transform.localScale += new Vector3(-tam, -tam, -tam);
            transform.GetChild(1).transform.localScale += new Vector3(-1 * tam, -1 * tam, -1 * tam);
            transform.GetChild(2).transform.localScale += new Vector3(-1 * tam, -1 * tam, -1 * tam);
            transform.GetChild(3).transform.localScale += new Vector3(-1 * tam, -1 * tam, -1 * tam);
            if (transform.localScale.x < 0) Destroy(gameObject);
        }
	}
}
