using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Unit
{
	public GameController controller;

	public GameObject chestHead;
    public GameObject diamond;
    public GameObject diamond_50;
    public GameObject diamond_75;

	public bool caveChest = false;
	public float cooldown;
	public float animationTime;
    public float randomTimeLimit;
    private float cooldownRandomized;
	private float animationTimeDiff;
	private float timeDiff;
    private int contador = 1;

    void spawnDiamond()
    {
        GameObject diam;
        // Instanciamos el diamante en el interior del cofre, intentando centrarlo. El Start y Update del propio diamante se encargan de su funcionamiento.
        if (contador == 4)
        {
            contador++;
            diam = Instantiate(diamond_50, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z - 0.2f), diamond_50.transform.rotation);
        }
        else if (contador == 8)
        {
            contador = 1;
            diam = Instantiate(diamond_75, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z - 0.2f), diamond_75.transform.rotation);
        }
        else
        {
            contador++;
            diam = Instantiate(diamond, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z - 0.2f), diamond.transform.rotation);
        }
		diam.GetComponent<Diamond>().fromCave=caveChest;
    }

	// Start is called before the first frame update
	override protected void Start()
    {
		base.Start();
		timeDiff=0.0f;
		animationTimeDiff=0.0f;
		controller=GameController.getInstance();
        cooldownRandomized = cooldown + Random.Range(0, randomTimeLimit);
	}
    private void sonido()
    {
        if (!caveChest) SoundController.getInstance().play(SoundController.SoundId.chestOpening);
    }
    // Update is called once per frame
    override protected void Update()
    {
		float time = Time.deltaTime;
		timeDiff+=time;
        if ((animationTimeDiff + time) >= animationTime + cooldownRandomized)
        {
            gameObject.GetComponent<Animator>().SetTrigger("rotate");
            animationTimeDiff = 0.0f;
			
            cooldownRandomized = cooldown + Random.Range(0, randomTimeLimit);
        }
        else
        {
            animationTimeDiff += time;
        }
    }
}
