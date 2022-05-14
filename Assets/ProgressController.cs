using System.Collections;
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
    float progress = 0;
    float percent;

    public RectTransform bar_end, bar_start, bar_rat;
    float distanceUI, preogressUI;

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

    public void SetProgress(float x)
    {
        progress = x;
    }

    public float GetProgress()
    {
        return progress;
    }

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
        
    }

    void DistanceToPercent()
    {
        percent = progress * 100 / totaldistance;
    }

    void PercentToUI()
    {
        preogressUI = percent * distanceUI / 100;
    }

    void UpdateUI()
    {
        bar_rat.position = new Vector3(bar_rat.position.x, bar_start.position.y + preogressUI, bar_rat.position.z);
    }

    void FixedUpdate()
    {
        DistanceToPercent();
        PercentToUI();
        UpdateUI();
    }
}
