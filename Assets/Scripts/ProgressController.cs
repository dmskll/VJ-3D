﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class ProgressController : MonoBehaviour
{
   // public static ProgressController instance = null;
    public static ProgressController instance { get; private set; }

    public PathCreator[] paths = new PathCreator[10];
    EndOfPathInstruction stop = EndOfPathInstruction.Stop;
    float totaldistance = 0;
    float p1_progress = 0;
    float p2_progress = 0;

    public RectTransform bar_end, bar_start, bar_p1, bar_p2;
    float distanceUI, preogressUI;

    public bool start = false;
    bool readyp1, readyp2 = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CalculeTotalDistance()
    {
        float distance;
        foreach (PathCreator i in paths)
        {
            if(i != null)
            {
                Vector3 pos, posAux;
                distance = 0;
                bool end = false;

                distance++;
                pos = i.path.GetPointAtDistance(distance, stop);
                posAux = pos;


                while (!end)
                {
                    distance++;
                    pos = i.path.GetPointAtDistance(distance, stop);
                    if(pos == posAux)
                    {
                        end = true;
                    }
                    else
                    {
                        posAux = pos;
                    }
                }
                //Debug.Log(distance);
                totaldistance += distance;
            }
        }
    }

    public void SetProgress(float x, string player_tag)
    {
        if(player_tag.Equals("player1"))
        {
            p1_progress = x;
        }
        else
        {
            p2_progress = x;
        }
    }
/*
    public float GetProgress()
    {
        return null;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        CalculeTotalDistance();
        distanceUI = bar_end.position.y - bar_start.position.y;
        //Debug.Log(totaldistance);
    }

    // Update is called once per frame
    void Update()
    {
        if(!start)
        {
            readyp1 = Input.GetKey(KeyCode.Space);
            readyp2 = Input.GetKey(KeyCode.Return);
            start = readyp1 & readyp2;
        }
        else
        {
            Debug.Log("deb");
        }
    }

    void setReady(string p)
    {

    }

    void FixedUpdate()
    {

        float percent = p1_progress * 100 / totaldistance;
        float preogressUI = percent * distanceUI / 100;
        bar_p1.position = new Vector3(bar_p1.position.x, bar_start.position.y + preogressUI, bar_p1.position.z);

        percent = p2_progress * 100 / totaldistance;
        preogressUI = percent * distanceUI / 100;
        bar_p2.position = new Vector3(bar_p2.position.x, bar_start.position.y + preogressUI, bar_p2.position.z);

    }
}
