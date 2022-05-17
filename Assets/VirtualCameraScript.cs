using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraScript : MonoBehaviour
{
    public enum player { player1, player2 };
    public player follow;
    // Start is called before the first frame update
    void Start()
    {
        SetFollowObject();
    }

    void SetFollowObject()
    {
        GameObject obj;
        if (follow.Equals(player.player1))
        {
            obj = GameObject.FindGameObjectWithTag("player1");
        }
        else
        {
            obj = GameObject.FindGameObjectWithTag("player2");
        }
        
        var vcam = gameObject.GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = obj.transform;
        vcam.LookAt = obj.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
