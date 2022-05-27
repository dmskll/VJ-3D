using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathChanger : MonoBehaviour
{

    //gestiona el cambio de paths
    public CameraScript.CameraState cam_state;
    public PathCreator path_in, path_out;
    private PlayerController pc1, pc2;

    // Start is called before the first frame update
    void Start()
    {
        pc1 = GameObject.FindGameObjectWithTag("player1").GetComponent<PlayerController>();
        pc2 = GameObject.FindGameObjectWithTag("player2").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("player1"))
        {
            pc1.SetPath(path_in, path_in.tag);
            ProgressController.instance.camera_p1.setState(cam_state);
        }
        else if(other.gameObject.tag.Equals("player2"))
        {
            pc2.SetPath(path_in, path_in.tag);
            ProgressController.instance.camera_p2.setState(cam_state);
        }
    }
}
