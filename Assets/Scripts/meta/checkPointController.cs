﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointController : MonoBehaviour
{
    private PlayerController pc1, pc2;
    public CameraScript.CameraState cam_state;
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
            pc1.setCheckpoint();
            ProgressController.instance.camera_p1.setState(cam_state);
        }
        if (other.gameObject.tag.Equals("player2"))
        {
            pc2.setCheckpoint();
            ProgressController.instance.camera_p2.setState(cam_state);
        }
    }
}
