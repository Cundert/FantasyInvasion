using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

	public float speed;
    private float timeSlowmoDiff;
    private bool delayedDeathInProgress = false;
    private float timeForDeath = -1;
    public Material frosted;
    protected Material baseMat;

    private Vector3 flyValue = new Vector3(0f,0f,0f);

    public void slowmo(float seconds, float speedMod)
    {
        timeSlowmoDiff = seconds;
        speedModifier = speedMod;
        frostModel();
    }

    virtual protected void frostModel()
    {
        Debug.Log("Error: comportamiento de cambio de modelo congelado no definido");
    }

    virtual protected void baseModel()
    {
        Debug.Log("Error: comportamiento de cambio de modelo base no definido");
    }

    virtual protected void setBaseModel()
    {
        Debug.Log("Error: comportamiento de seteo de modelo base no definido");
    }

    public void fly(float x)
    {
        flyValue = new Vector3(x, 12);
        timeForDeath = 2.5f;
        delayedDeathInProgress = true;
    }

    public void overkill()
    {
        // Utilizada por el rayo, explosion, etc. Evita conductas de muerte como el spawn de un slime pequeño
        gameObject.layer = 0;
        GameController.getInstance().enemiesRemaining--;
        if (GameController.getInstance().enemiesRemaining <= 0) GameController.getInstance().win();
        Destroy(gameObject);
    }

    override public void die()
    {
        gameObject.layer = 0;
        GameController.getInstance().enemiesRemaining--;
        if (GameController.getInstance().enemiesRemaining <= 0) GameController.getInstance().win();
        Destroy(gameObject);
    }

    protected void checkFrost()
    {
        if (speedModifier != 1.0f)
        {
            timeSlowmoDiff -= Time.deltaTime;
            if (timeSlowmoDiff <= 0)
            {
                speedModifier = 1.0f;
                if (frosted) baseModel();
            }
        }
    }

    protected void delayedDeathBehaviour()
    {
        if (delayedDeathInProgress)
        {
            if (timeForDeath <= 0.0f)
            {
                delayedDeathInProgress = false;
                overkill();
            }
            else timeForDeath -= Time.deltaTime;
        }
        transform.Translate(flyValue * Time.deltaTime);
    }

    override protected void Update()
    {
        delayedDeathBehaviour();
        checkFrost();
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
                gameObject.GetComponent<Animator>().SetTrigger("move");
                float despl = Time.deltaTime * speed * speedModifier;
                transform.Translate(new Vector3(0, 0, despl));
            }
        }
    }

    override public void makeAttack(Character objective)
    {
        base.makeAttack(objective);
        gameObject.GetComponent<Animator>().SetTrigger("idle");
    }

    // Start is called before the first frame update
    void Start() {
		attackTimeDiff=0.0f;
        setBaseModel();
	}
}
