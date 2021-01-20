using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{

    public GameObject[] enemies;

	private float timeDiff;
    private int spawnedMinions;
    private int lastLine = 0;
    private bool pausa = false;

    [System.Serializable]
    public class SpawnData
    {
        public int enemyID;
        public float timeToSpawn;
    }

    public SpawnData[] spawns;

	public GameObject[] explosionParticulas;

	// Start is called before the first frame update
	void Start()
    {
		timeDiff=0.0f;
        spawnedMinions = 0;
        GameController.getInstance().enemiesRemaining = spawns.Length;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&GameController.getInstance().pausable) pausa = !pausa;
        if (!pausa)
        {
            if (spawnedMinions < spawns.Length)
            {
                timeDiff += Time.deltaTime;
                if (timeDiff >= spawns[spawnedMinions].timeToSpawn)
                {
                    timeDiff -= spawns[spawnedMinions].timeToSpawn;
                    int spawner = Random.Range(0, 5);
                    while (spawner == lastLine) spawner = Random.Range(0, 5);
                    spawn(spawns[spawnedMinions].enemyID, spawner);
                }
            }
        }
	}

	void spawn(int type, int spawner) {
        lastLine = spawner;
        if (enemies[type].name!= "SlimeGreen") Instantiate(enemies[type], new Vector3(transform.position.x, transform.position.y-0.25f, transform.position.z-spawner), Quaternion.AngleAxis(-90f,new Vector3(0,1,0)));
        else
        {
            int offset;
            if (spawner == 0) offset = -1;
            else if (spawner == 4) offset = 1;
            else
            {
                offset = Random.Range(0, 2);
                if (offset == 0) offset = -1;
            }
            GameObject slime = Instantiate(enemies[type], new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z - spawner), Quaternion.AngleAxis(-90f, new Vector3(0, 1, 0)));
            slime.GetComponent<Slime>().offset = offset;
        }
		explosionParticulas[spawner].GetComponent<ParticleSystem>().Emit(1);
        spawnedMinions++;
	}
}
