using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerController pc1, pc2;
    void Start()
    {
        pc1 = GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerController>();
        pc2 = GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("player1"))
        {
            pc1.setEnd();
        }
        else if (other.gameObject.tag.Equals("player2"))
        {
            pc2.setEnd();
        }
    }
}
