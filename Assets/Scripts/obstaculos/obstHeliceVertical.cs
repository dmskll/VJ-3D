using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstHeliceVertical : MonoBehaviour
{
    public float speed, start_delay;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (start_delay > 0) start_delay -= Time.deltaTime;
        else
        {
            transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
}
