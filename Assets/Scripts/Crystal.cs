using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Unit {
    public GameObject lightning;
    private float timeRIP = 0.0f;
    private bool rip = false;
    public Color color;

    void spawnLightning()
    {
        lightning = Instantiate(lightning, transform.position, Quaternion.identity);
        lightning.GetComponent<LightningBoltScript>().StartObject = gameObject;
        lightning.GetComponent<LightningBoltScript>().StartPosition = new Vector3(0, 1, 0);
        lightning.GetComponent<LightningBoltScript>().EndPosition =  new Vector3(7, -1, 0);
        lightning.GetComponent<LineRenderer>().startColor = gameObject.GetComponent<Crystal>().color;
        lightning.GetComponent<LineRenderer>().endColor = gameObject.GetComponent<Crystal>().color;
        GetComponent<AudioSource>().Play();

    }


    public override void die()
    {
        Destroy(lightning);
        gameObject.layer = 0;
        //Destroy(gameObject);
        rip = true;
    }

    override protected void Update()
    {

        if (rip)
        {
            if (timeRIP <= 1)
            {
                for (int i=0; i < transform.childCount - 1; i++)
                {
                    float t = transform.GetChild(i).GetComponent<Renderer>().material.GetFloat("_Transparency");
                    if (t > 0.1)
                    {
                        t -= Time.deltaTime;
                        transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Transparency", t);
                    }
                }
                timeRIP += Time.deltaTime;
            }
            else Destroy(gameObject);
        }
    }
}
