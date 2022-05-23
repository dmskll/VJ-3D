using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PaellaScript : MonoBehaviour
{
    public float fallSpeed, liftSpeed;
    public AudioSource bonkSource;
    public AudioClip bonkSound;
    private bool falling;
    private float rotating_progress;
    public float start_delay;
    
    // Start is called before the first frame update
    void Start()
    {
        falling = false;
        rotating_progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (start_delay > 0) start_delay -= Time.deltaTime;
        else
        {
            if (falling)
            {
                if (rotating_progress <= 0) {
                    falling = false;
                    bonkSource.PlayOneShot(bonkSound);
                }
                else
                {
                    transform.Rotate(new Vector3(-Time.deltaTime * fallSpeed, 0, 0));
                    rotating_progress -= Time.deltaTime * fallSpeed;
                }
            }
            else
            {

                if (rotating_progress >= 90) falling = true;
                else
                {
                    transform.Rotate(new Vector3(Time.deltaTime * liftSpeed, 0, 0));
                    rotating_progress += Time.deltaTime * liftSpeed;
                }
            }
        }

    }
}
