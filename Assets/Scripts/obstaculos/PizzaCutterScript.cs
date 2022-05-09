using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCutterScript : MonoBehaviour
{


    public float gravity;
    public float initialVelocity;
    private float angle;
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = initialVelocity;
        angle = transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        angle += velocity * Time.deltaTime;
        if (angle < 0) velocity += gravity * Time.deltaTime;
        else velocity -= gravity * Time.deltaTime;

        transform.Rotate(new Vector3(velocity*Time.deltaTime , 0, 0));
    }
}
