using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class StartController : MonoBehaviour
{
    private PlayerController pc;
    public PathCreator path_start;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerController>();
        pc.SetPath(path_start, path_start.tag);
        pc.setCheckpoint();

        pc = GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerController>();
        if(pc != null)
        {
            pc.SetPath(path_start, path_start.tag);
            pc.setCheckpoint();
        }
    }
}
