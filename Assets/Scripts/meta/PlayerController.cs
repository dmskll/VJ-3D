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

public enum movement { Run, ClimbUp, ClimbVert, Slide};
public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    float distanceTraveled = -1;
    PathCreator path;
    Vector3 pathRotation;
    public checkpoint check_point;
    bool end;
    private bool godMode, moving;
    public Vector3 deathJump;
    private float dying;
    public float deathTime;
    private Rigidbody RG;
    public movement actual_movement;
    public GameObject rat;

    public float jump_speed, jump_gravity;
    float jump_actual_speed;
    float cd_jump = -1;
    bool jumping = false;


    // Start is called before the first frame update
    void Start()
    {
        distanceTraveled = 0;
        end = false;
        godMode = false;
        dying = -1;
        //updateMovement();
        RG = rat.GetComponent<Rigidbody>();

        //actual_movement = movment.Run;
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
    public void SetPath(PathCreator new_path, string s)
    {
        path = new_path;
        distanceTraveled = 0;
        actual_movement = TagToMovement(s);
        if(actual_movement == movement.Slide)
        {
            cd_jump = 0.4f; //para que no salta nada mas entrar en el path
        }
        
        updateMovement(true);
        setCheckpoint();
    }

    public movement TagToMovement(string s)
    {
        switch(s)
        {
            case ("Slide"):
                return movement.Slide;
            case ("Run"):
                return movement.Run;
            case ("ClimbUp"):
                return movement.ClimbUp;
        }
        return movement.Run;
    }
    public void reSpawn()
    {
        path = check_point.path;
        distanceTraveled = check_point.distance;

        updateMovement(true);
        rat.transform.localRotation = Quaternion.Euler(0, 0, 0);
        rat.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void updateRun(bool force)
    {
        if(moving || force)
        {
            distanceTraveled += speed;
            transform.position = path.path.GetPointAtDistance(distanceTraveled);
            pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(pathRotation.x, pathRotation.y - 90, 0));    
        }
    }

    private void updateSlide()
    {
        if(moving && !jumping)
        {
            if (cd_jump == -1)
            {
                jumping = true;
                jump_actual_speed = jump_speed;
            }
            else
            {
                cd_jump -= Time.deltaTime;
                if (cd_jump < 0) cd_jump = -1;
            }
               
        }
        if (jumping) distanceTraveled += speed * 2f;
        else distanceTraveled += speed;
        Vector3 position = path.path.GetPointAtDistance(distanceTraveled);
        if (jumping)
        {
            jump_actual_speed -= jump_gravity;
            float auxY = transform.position.y + jump_actual_speed;
            if(auxY < position.y)
            {
                jumping = false;
                cd_jump = 0.06f;
            }
            else
            {
                position.y = auxY;
            }
        }
        transform.position = position;
        pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(pathRotation.x, pathRotation.y, pathRotation.z));
        transform.Rotate(0, -90, 0);
    }


    
    private void updateMovement(bool force)
    {
        switch (actual_movement)
        {
            case (movement.Run):
                updateRun(force);
                break;
            case (movement.ClimbUp):
                break;
            case (movement.ClimbVert):
                break;
            case (movement.Slide):
                updateSlide();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (dying == -1 && path != null)
        {
            updateMovement(false);
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
