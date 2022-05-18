using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCutterScript : MonoBehaviour
{


    public float gravity;
    public float initialVelocity;
    private float angle;
    private float velocity;
    public float start_delay;

    // Start is called before the first frame update
    void Start()
    {
        velocity = initialVelocity;
        angle = transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (start_delay > 0) start_delay -= Time.deltaTime;
        else
        {
            angle += velocity * Time.deltaTime;
            if (angle < 0) velocity += gravity * Time.deltaTime;
            else velocity -= gravity * Time.deltaTime;

            transform.Rotate(new Vector3(velocity * Time.deltaTime, 0, 0));
        }
    }
}
