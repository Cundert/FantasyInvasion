using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMainMenu : MonoBehaviour
{
	public float speed;

    // Update is called once per frame
    void Update()
    {
		float despl = Time.deltaTime*speed;
		transform.Translate(new Vector3(0, 0, despl));
	}
}
