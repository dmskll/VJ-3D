using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstSalchichas : MonoBehaviour
{
    public float speed;
    public GameObject inicio, final;
    Transform t_ini, t_final;
    // Start is called before the first frame update
    void Start()
    {
        t_ini = inicio.GetComponent<Transform>();
        t_final = inicio.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
}
