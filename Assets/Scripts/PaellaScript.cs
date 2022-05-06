﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PaellaScript : MonoBehaviour
{
    public float fallSpeed, liftSpeed;
    private bool falling;
    private float rotating_progress;
    
    // Start is called before the first frame update
    void Start()
    {
        falling = false;
        rotating_progress = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (falling)
        {
            if (rotating_progress <= 0) falling = false;
            else {
                transform.Rotate(new Vector3(-Time.deltaTime * fallSpeed, 0, 0));
                rotating_progress -= Time.deltaTime * fallSpeed;
            }
        }
        else 
        { 
            
            if (rotating_progress >= 90) falling = true;
            else {
                transform.Rotate(new Vector3(Time.deltaTime * liftSpeed, 0, 0));
                rotating_progress += Time.deltaTime * liftSpeed;
            }
        }

    }
}
