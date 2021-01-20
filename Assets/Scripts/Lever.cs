using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject minecart;
    public float cooldown;
    public float timeDiff;
    private bool active;
    private bool reactivating;
    private float timeActivate = 0.0f;
    private float timeWait = 0.0f;
    private Color emiColor = new Color (0,0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        timeDiff = 0;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            timeDiff -= Time.deltaTime;
            if (!reactivating && timeDiff < 0)
            {
                reactivating = true;
                GetComponent<Animator>().SetTrigger("done");
            }
            emiColor = new Color(0, 0, 0, 0);
            transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
            transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
            transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
        }
        else
        {
            if (timeWait <= 3)
            {
                timeWait += Time.deltaTime;
            }
            else
            {
                if (timeActivate <= 1)
                {
                    float verde = Mathf.Clamp(timeActivate, 0, 1);
                    //Debug.Log("Estoy en el primer if");
                    emiColor = new Color(0, verde, 0, 0);
                    //Debug.Log(emiColor);
                    transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
                    transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
                    transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
                    timeActivate += Time.deltaTime;
                }
                else if (timeActivate <= 2)
                {
                    float verde = Mathf.Clamp(timeActivate - 1, 0, 1);
                    //Debug.Log("Estoy en el else if");
                    emiColor = new Color(0, 1 - verde, 0, 0);
                    //Debug.Log(emiColor);
                    transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
                    transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
                    transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
                    timeActivate += Time.deltaTime;
                }
                else
                {
                    timeActivate = 0;
                    timeWait = 0;
                }
            }
        }
    }

    private void reactivate()
    {
        active = true;
        reactivating = false;
		SoundController.getInstance().play(SoundController.SoundId.leverReady);
	}

    public void activate()
    {
        if (active)
        {
            timeWait = 3;
            timeActivate = 0;
            Instantiate(minecart);
			SoundController.getInstance().play(SoundController.SoundId.minecart);
			SoundController.getInstance().play(SoundController.SoundId.lever);
            active = false;
            timeDiff = cooldown;
            GetComponent<Animator>().SetTrigger("activate");
        }
    }
}
