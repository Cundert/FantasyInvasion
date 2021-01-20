using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OroInsuficiente : MonoBehaviour
{

    public float duration;
    public float desplazamiento;

    private float timeDiff;
    private float desplPerSecond;

    // Start is called before the first frame update
    public void Start()
    {
        timeDiff = 0.0f;
        desplPerSecond = desplazamiento / duration;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        float lastTime = timeDiff;
        timeDiff += time;
        if (timeDiff > duration) timeDiff = duration;
        transform.Translate(new Vector3(0,desplPerSecond * (timeDiff - lastTime)));
        if (timeDiff >= duration)
        {
            timeDiff = 0;
            gameObject.SetActive(false);
        }
    }
}
