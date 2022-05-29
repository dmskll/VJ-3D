using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class circleController : MonoBehaviour
{
    public Image circulo, centro;
    public Text text;

    bool ready;
    bool start;

    // Start is called before the first frame update
    void Start()
    {
        centro.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setReady(bool b)
    {
        if (b)
        {
            centro.enabled = true;
            text.enabled = false;

        }
        else
        {
            centro.enabled = false;
            text.enabled = true;
        }
    }

    public void setStart()
    {
        gameObject.SetActive(false);
    }

}
