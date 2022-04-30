using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstHeliceHorizontal : MonoBehaviour
{
    public GameObject barilla1, barilla2, gira;
    public float barilla_speed, spin_speed, mov_duration, stop_duration;

    private Transform b1_t, b2_t, g_t;
    private float stop_timer;
    // Start is called before the first frame update
    void Start()
    {
        b1_t = barilla1.GetComponent<Transform>();
        b2_t = barilla2.GetComponent<Transform>();
        g_t = gira.GetComponent<Transform>();
        mov_timer = mov_duration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (stop_timer < 0)
        {
            g_t.Rotate(new Vector3(0, spin_speed, 0), Space.Self);
            b1_t.Rotate(new Vector3(barilla_speed, 0, 0), Space.Self);
            b2_t.Rotate(new Vector3(barilla_speed, 0, 0), Space.Self);
  
            //es mas justo que el movimiento se vea determinado por el recorrido y no por un timer
            //es dificil que justo cuadre con el modulo de 180 
            if (g_t.rotation.eulerAngles.y % 180 < 1.3) 
            {
                stop_timer = stop_duration;
            }
        }
        else 
        {
            stop_timer -= 1;
            if(stop_timer < 0)
            {
                mov_timer = mov_duration;
            }
        }



        

    }
}
