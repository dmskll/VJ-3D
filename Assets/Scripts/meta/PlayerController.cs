using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public struct checkpoint
{
    public float distance;
    public PathCreator path;
    //el tipo de camino??
}
public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    float distanceTraveled;
    PathCreator path;
    Vector3 pathRotation;
    public checkpoint check_point;
    bool end;

    // Start is called before the first frame update
    void Start()
    {
        distanceTraveled = 0;
        end = false;
        updatePathMovement();
    }

    public void setEnd()
    {
        end = true;
    }

    public void setCheckpoint()
    {
        check_point.distance = distanceTraveled;
        check_point.path = path;
    }
    public void SetPath(PathCreator new_path)
    {
        path = new_path;
        distanceTraveled = 0;
        setCheckpoint();
    }

    public void reSpawn()
    {
        path = check_point.path;
        distanceTraveled = check_point.distance;
        updatePathMovement();
    }

    private void updatePathMovement()
    { 
        transform.position = path.path.GetPointAtDistance(distanceTraveled);
        pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(pathRotation.x, pathRotation.y - 90, 0));
    }


    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space) && !end)
        {
            distanceTraveled += speed;
            updatePathMovement();
        }
        if (Input.GetKey(KeyCode.R))
        {
            reSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
