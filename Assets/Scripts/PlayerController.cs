using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerController : MonoBehaviour
{
    public PathCreator path;
    public float speed = 1;
    float distanceTraveled;
    Vector3 pathRotation;
    // Start is called before the first frame update
    void Start()
    {
        distanceTraveled = 0;
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
        if(Input.GetKey(KeyCode.Space))
        {
            distanceTraveled += speed;
            updatePathMovement();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPath(PathCreator new_path)
    {
        path = new_path;
        distanceTraveled = 0;
    }
}
