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

public enum movment { Run, ClimbUp, ClimbVert, Slide};
public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    float distanceTraveled;
    PathCreator path;
    Vector3 pathRotation;
    public checkpoint check_point;
    bool end;
    private bool godMode, moving;
    public Vector3 deathJump;
    private float dying;
    public float deathTime;
    private Rigidbody RG;
    public movment actual_movement;
    public GameObject rat;


    // Start is called before the first frame update
    void Start()
    {
        distanceTraveled = 0;
        end = false;
        updatePathMovement();
        godMode = false;
        dying = -1;
        RG = rat.GetComponent<Rigidbody>();

        actual_movement = movment.Run;
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
        rat.transform.localRotation = Quaternion.Euler(0, 0, 0);
        rat.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void updatePathMovement()
    {
        if (dying == -1)
        {
            transform.position = path.path.GetPointAtDistance(distanceTraveled);
            pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(pathRotation.x, pathRotation.y - 90, 0));
        }
    }


    private void FixedUpdate()
    {
        if (moving)
        {
            switch(actual_movement)
            {
                case (movment.Run):
                    distanceTraveled += speed;
                    updatePathMovement();
                    break;
                case (movment.ClimbUp):
                    break;
                case (movment.ClimbVert):
                    break;
                case (movment.Slide):
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moving = Input.GetKey(KeyCode.Space) && !end;
        if (Input.GetKey(KeyCode.R))
        {
            reSpawn();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = !godMode;
        }
        if (dying != -1)
        {
            dying += Time.deltaTime;
            if (dying > deathTime) {
                dying = -1;
                RG.isKinematic = true;
                
                reSpawn();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            if (!godMode && dying == -1) {
                RG.isKinematic = false;

                float randX = Random.Range(-300, 300);
                float randZ = Random.Range(-300, 300);

                RG.AddForce(deathJump + new Vector3 (randX,0,randZ));
                RG.AddTorque(new Vector3(Random.Range(-300, 300), Random.Range(-300, 300), Random.Range(-300, 300)));
                dying = 0;
            }
        }
        else Debug.Log("no es obstaculo");
    }
}
