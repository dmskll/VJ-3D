using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstSartenGiratoria : MonoBehaviour
{
    public GameObject pivot1, pivot2, pivot_actual, root, altura;
    public float speed;
    Transform tp1, tp2, tpa, troot, ta;
    float angle_stage, angle_sum;
    int num_pivot;

    // Start is called before the first frame update
    void Start()
    {
        tp1 = pivot1.GetComponent<Transform>();
        tp2 = pivot2.GetComponent<Transform>();
        tpa = pivot_actual.GetComponent<Transform>();
        troot = root.GetComponent<Transform>();
        ta = altura.GetComponent<Transform>();
        angle_sum = 0;

        angle_stage = 90;
        tpa.position = new Vector3(tpa.position.x, ta.position.y, tpa.position.z);


    }

    void UpdatePivot()
    {
        switch(angle_stage)
        {
            case 0:
                tpa.position = new Vector3(tpa.position.x, ta.position.y, tpa.position.z);
                transform.parent = troot;
                tpa.position = tp1.position;
                transform.parent = tpa;
                break;
            case 90:
                tpa.position = new Vector3(tpa.position.x, ta.position.y, tpa.position.z);
                transform.parent = troot;
                tpa.position = tp2.position;
                transform.parent = tpa;
                break;
            case 180:
                tpa.position = new Vector3(tpa.position.x, ta.position.y, tpa.position.z);
                transform.parent = troot;
                tpa.position = tp2.position;
                transform.parent = tpa;
                break;
            case 270:
                tpa.position = new Vector3(tpa.position.x, ta.position.y, tpa.position.z);
                transform.parent = troot;
                tpa.position = tp1.position;
                transform.parent = tpa;
                break;
        }
    }

    void UpdateAngleStage()
    {
        if (angle_sum > 90)
        {
            angle_sum = 0;
            switch (angle_stage)
            {
                case 0:
                    tpa.rotation = Quaternion.Euler(90, 0, 0);
                    angle_stage = 90;
                    break;
                case 90:
                    tpa.rotation = Quaternion.Euler(180, 0, 0);
                    angle_stage = 180;
                    break;
                case 180:
                    tpa.rotation = Quaternion.Euler(270, 0, 0);
                    angle_stage = 270;
                    break;
                case 270:
                    tpa.rotation = Quaternion.Euler(0, 0, 0);
                    angle_stage = 0;
                    break;
            }
            UpdatePivot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        angle_sum += speed;
        tpa.Rotate(new Vector3(speed, 0, 0));
        UpdateAngleStage();
    }
}
