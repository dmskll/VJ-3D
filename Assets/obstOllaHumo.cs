using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class obstOllaHumo : MonoBehaviour
{
    public Animator anim;
    public ParticleSystem columna_humo, humo_idle;
    public float stop_duration;
    float stop_timer;
    // Start is called before the first frame update
    void Start()
    {
        stop_timer = stop_duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop_timer > 0)
        {
            stop_timer -= Time.deltaTime;

            if(stop_timer < 0)
            {
                columna_humo.Play();
                humo_idle.Stop();
                anim.SetBool("humo", true);
            }
        }
    }

    public void EndHumo()
    {
        humo_idle.Play();
        anim.SetBool("humo", false);
        stop_timer = stop_duration;
    }
}
