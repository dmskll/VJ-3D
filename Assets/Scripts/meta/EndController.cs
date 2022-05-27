using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerController pc1, pc2;
    void Start()
    {
        pc1 = ProgressController.instance.player_1;
        pc2 = ProgressController.instance.player_2;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("player1"))
        {
            ProgressController.instance.setEnd("player1");
        }
        else if (other.gameObject.tag.Equals("player2"))
        {
            ProgressController.instance.setEnd("player2");
        }
    }
}
