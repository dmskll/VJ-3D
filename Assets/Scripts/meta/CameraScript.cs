using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{

    public enum CameraState { Back, Front, SideLeft, SideRight,  NearBack};
    public CameraState start_state;
    CameraState actual_state;
    CinemachineVirtualCamera back, left, right, front;
    // Start is called before the first frame update
    void Start()
    {
        back = transform.Find("vcam back").gameObject.GetComponent<CinemachineVirtualCamera>();
        front = transform.Find("vcam front").gameObject.GetComponent<CinemachineVirtualCamera>();
        left = transform.Find("vcam left").gameObject.GetComponent<CinemachineVirtualCamera>();
        right = transform.Find("vcam right").gameObject.GetComponent<CinemachineVirtualCamera>();

        setState(start_state);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setState(CameraState s)
    {
        switch(actual_state)
        {
            case(CameraState.Back):
                back.enabled = false;
                break;
            case (CameraState.Front):
                front.enabled = false;
                break;
            case (CameraState.SideLeft):
                left.enabled = false;
                break;
            case (CameraState.SideRight):
                right.enabled = false;
                break;
            case (CameraState.NearBack):
                back.enabled = false;
                break;
        }

        switch (s)
        {
            case (CameraState.Back):
                back.enabled = true;
                break;
            case (CameraState.Front):
                front.enabled = true;
                break;
            case (CameraState.SideLeft):
                left.enabled = true;
                break;
            case (CameraState.SideRight):
                right.enabled = true;
                break;
            case (CameraState.NearBack):
                back.enabled = true;
                break;
        }
        actual_state = s;
    }
}
