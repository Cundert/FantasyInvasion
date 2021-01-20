using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private float timeDiff = 0.0f;
    private float timeRIP = 0.0f;
    private float timeShine = 0.0f;
    private float timeWait = 0.0f;
    private float intensity = 64.0f;
    public float cooldown;
    public GameObject explosionParticulas;
    public int valor;

    private Color emiColor = new Color(0, 0, 0, 0);

	public bool fromCave = false;

    private Rigidbody rb;
    private bool rip = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // El cofre esta en la cueva, por lo que el desplazamiento del diamante es distinto.
        //if (transform.position.x > 6)
		if (fromCave)
        {
            intensity = intensity * 2;
            rb.AddForce(new Vector3(-intensity, intensity, Random.Range(-2, 0) * Random.Range(intensity / 2, intensity)));
        }

        // Desplazamos el diamante verticalmente y a la derecha. Ademas, aplicamos un movimiento aleatorio en el eje Z, para que no caiga siempre en el mismo sitio.
        else rb.AddForce(new Vector3(intensity, intensity, Random.Range(-1, 2) * Random.Range(intensity / 2, intensity)));
    }

    public int die()
    {
        gameObject.layer = 15;
        GameObject particula = Instantiate(explosionParticulas, new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), transform.rotation);
        particula.GetComponent<ParticleSystem>().Emit(10);
        rip = true;
        return valor;
    }

    // Update is called once per frame
    void Update()
    {
		if (timeDiff>=cooldown && !rip) {
			die();
			GameController.getInstance().saldo+=valor;
			SoundController.getInstance().play(SoundController.SoundId.diamond);
		}
		timeDiff += Time.deltaTime;
        if (rip)
        {
            emiColor = new Color(0, 0, 0, 0);
            transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
            if (timeRIP <= 1)
            {
                Color c = transform.GetChild(0).GetComponent<Renderer>().material.color;
                if (c.a > 0.1) transform.GetChild(0).GetComponent<Renderer>().material.color -= new Color(0, 0, 0, timeRIP / 10);
                timeRIP += Time.deltaTime;
            }
            else
            {
                gameObject.layer = 0;
                Destroy(gameObject);
            }
        }
        else
        {
            if (timeWait <= 1)
            {
                timeWait += Time.deltaTime;
            }
            else
            {
                if (timeShine <= 1)
                {
                    float value = Mathf.Clamp(timeShine, 0, 1);
                    if (valor == 25) emiColor = new Color(value, 0, value / 2, 0);
                    else if (valor == 50) emiColor = new Color(value, value, value, 0);
                    else emiColor = new Color(0, value, value, 0);
                    transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
                    timeShine += Time.deltaTime;
                }
                else if (timeShine <= 2)
                {
                    float value = Mathf.Clamp(timeShine - 1, 0, 1);
                    if (valor == 25) emiColor = new Color(1 - value, 0, 1 - (value / 2), 0);
                    else if (valor == 50 ) emiColor = new Color(1 - value, 1 - value, 1 - value, 0);
                    else emiColor = new Color(0, 1 - value, 1 - value, 0);
                    transform.GetChild(0).GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                    transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
                    timeShine += Time.deltaTime;
                }
                else
                {
                    timeShine = 0;
                    timeWait = 0;
                }
            }
        }
    }
}
