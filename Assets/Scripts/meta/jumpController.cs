using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpController : MonoBehaviour
{

    public Animator espatula1, espatula2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player1"))
        {
            espatula1.SetTrigger("trigger");
        }
        else if (other.CompareTag("player2"))
        {
            espatula2.SetTrigger("trigger");
        }
    }
}
