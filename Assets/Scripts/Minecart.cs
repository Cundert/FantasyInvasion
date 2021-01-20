using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{

    public GameObject route;

    public bool[] visited;
    private bool moving = false;
    public float speed;
    public float rotationSpeed;
    private float timeDiff;

    // Start is called before the first frame update
    void Start()
    {
        System.Array.Resize(ref visited, route.transform.childCount);
        visited[0] = true;
        for (int i = 1; i < visited.Length; ++i) visited[i] = false;
        transform.position = route.transform.GetChild(0).transform.position;
        timeDiff = 0.0f;
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (visited[visited.Length - 1]) Destroy(gameObject);
        else if (moving)
        {
            timeDiff += Time.deltaTime * speed;
            int i = visited.Length - 1;
            while (!visited[i]) --i;
            if (transform.position == route.transform.GetChild(i + 1).transform.position)
            {
                visited[i + 1] = true;
                timeDiff = 0.0f;
            }
            else
            {
                transform.position = Vector3.Lerp(route.transform.GetChild(i).transform.position, route.transform.GetChild(i + 1).transform.position, timeDiff / Vector3.Distance(route.transform.GetChild(i).transform.position, route.transform.GetChild(i + 1).transform.position));
                Vector3 _direction = (new Vector3(route.transform.GetChild(i + 1).transform.position.x,transform.position.y, route.transform.GetChild(i + 1).transform.position.z) - transform.position).normalized;
                if (_direction != Vector3.zero) {
                    Quaternion _lookRotation = Quaternion.LookRotation(_direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.transform.position.x <= transform.position.x) other.gameObject.GetComponent<Enemy>().fly(-3.0f);
            else other.gameObject.GetComponent<Enemy>().fly(3.0f);
        }
    }
}
