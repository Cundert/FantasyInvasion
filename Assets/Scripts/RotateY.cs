using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
	public float speed;
    private bool pausa = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& GameController.getInstance().pausable) pausa = !pausa;
        if (!pausa)
        {
            transform.Rotate(new Vector3(0, 1, 0), speed);
        }
    }
}
