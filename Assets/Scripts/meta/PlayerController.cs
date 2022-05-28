using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public struct checkpoint
{
    public float distance;
    public PathCreator path;
    public float progress;
    public movement mov;
}

public enum movement { Run, ClimbUp, ClimbVert, Slide, Jump};
public class PlayerController : MonoBehaviour
{
    public float offset;

    public float speed = 1;
    float distanceTraveled = -1;
    float totaldistanceTraveled;
    public AudioSource boingSource;
    public AudioClip boingSound;
    PathCreator path;

    EndOfPathInstruction stop = EndOfPathInstruction.Stop;
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

    //transicion entre paths
    bool movement_transition = false;
    Vector3 end_position, start_position;
    Quaternion end_rotation, start_rotation;
    float t = 0, transition_speed; //elapsed time lerp

    public Animator rat_anim;
    public ParticleSystem stars, flysmoke;
    


    // Start is called before the first frame update
    void Start()
    {
        distanceTraveled = 0;
        totaldistanceTraveled = 0;
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

    public void setDance()
    {
        rat_anim.SetTrigger("dance");
    }

    public void setCheckpoint()
    {
        check_point.distance = distanceTraveled;
        check_point.path = path;
        check_point.progress = totaldistanceTraveled;
        check_point.mov = actual_movement;
    }

    public void SetUpTransition(movement past_movement)
    {
        movement_transition = true;
        start_position = transform.position;

        transform.position = path.path.GetPointAtDistance(0);
        transform.position += transform.TransformDirection(new Vector3(0, 0, offset));
        end_position = transform.position;
        transform.position = start_position;

        start_rotation = transform.rotation;


        if (past_movement == movement.Jump)
        {
            rat_anim.SetBool("Catapult", false);
            flysmoke.Stop();
        }
        

        transition_speed = 50;
        pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
        switch (actual_movement)
        {

            case (movement.Jump):
                rat_anim.SetBool("Catapult",true);
                flysmoke.Play();
                break;
            case (movement.Run):
                if(past_movement == movement.ClimbUp)
                    transition_speed = 10;
                transform.rotation = Quaternion.Euler(new Vector3(pathRotation.x, pathRotation.y - 90, 0));
                break;
            case (movement.Slide):
                transform.rotation = Quaternion.Euler(new Vector3(pathRotation.x, pathRotation.y, pathRotation.z));
                transform.Rotate(0, -90, 0);
                break;
            case (movement.ClimbUp):
                transition_speed = 10;
                transform.localRotation = Quaternion.Euler(new Vector3(pathRotation.x + 90, pathRotation.y + 180, 90));
                break;
        }
            
        end_rotation = transform.rotation;
        transform.rotation = start_rotation;
    }
    public void SetPath(PathCreator new_path, string s)
    {
        if (new_path != path) {
            path = new_path;
            distanceTraveled = 0;
            var past_movement = actual_movement;
            actual_movement = TagToMovement(s);
            if (actual_movement == movement.Slide)
            {
                cd_jump = 0.2f; //para que no salta nada mas entrar en el path
            }
            SetUpTransition(past_movement);
        }
    }

    public movement TagToMovement(string s)
    {
        switch(s)
        {
            case ("Slide"):
                return movement.Slide;
            case ("Run"):
                return movement.Run;
            case ("ClimbPath"):
                return movement.ClimbUp;
            case ("JumpPath"):
                return movement.Jump;
        }
        return movement.Run;
    }
    public void reSpawn()
    {
        rat_anim.enabled = true;
        if (actual_movement.Equals(movement.Jump))
            rat_anim.SetBool("Catapult", false);

        flysmoke.Stop();    

        path = check_point.path;
        distanceTraveled = check_point.distance;
        totaldistanceTraveled = check_point.progress;
        actual_movement = check_point.mov;

        updateMovement(true);
        rat.transform.localRotation = Quaternion.Euler(0, 0, 0);
        rat.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void UpdateDistance(float s)
    {
        distanceTraveled += s;
        totaldistanceTraveled += s;
        ProgressController.instance.SetProgress(totaldistanceTraveled, gameObject.tag);
    }

    private void updateRun(bool force)
    {
        if(moving || force)
        {
            UpdateDistance(speed);

            transform.position = path.path.GetPointAtDistance(distanceTraveled);
            pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(pathRotation.x, pathRotation.y - 90, 0));
            transform.position += transform.TransformDirection(new Vector3(0, 0, offset));
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
        if (jumping)
        {
            UpdateDistance(speed * 2f);
        }
        else
        {
            UpdateDistance(speed);
        }
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
        transform.position += transform.TransformDirection(new Vector3(0, 0, offset));
        pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(pathRotation.x, pathRotation.y, pathRotation.z));
        transform.Rotate(0, -90, 0);
    }

    private void updateClimb()
    {

        if (moving)
        {
            UpdateDistance(speed);

            transform.position = path.path.GetPointAtDistance(distanceTraveled, stop);
            pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
            transform.localRotation = Quaternion.Euler(new Vector3(pathRotation.x + 90, pathRotation.y + 180,90));

            transform.position += transform.TransformDirection(new Vector3(0, 0, offset));
        }
        else 
        {

            if(distanceTraveled > 0) UpdateDistance(-0.3f * speed);

            transform.position = path.path.GetPointAtDistance(distanceTraveled, stop);
            pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
            transform.localRotation = Quaternion.Euler(new Vector3(pathRotation.x + 90, pathRotation.y + 180, 90));

            transform.position += transform.TransformDirection(new Vector3(0, 0, offset));
        }
    }
    
    void updateJump()
    {
        UpdateDistance(speed * 5f);

        transform.position = path.path.GetPointAtDistance(distanceTraveled);
        pathRotation = path.path.GetRotationAtDistance(distanceTraveled).eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(0, pathRotation.y, pathRotation.z));
        transform.Rotate(0, -90, 0);
        transform.position += transform.TransformDirection(new Vector3(0, 0, offset));
    }

    private void updateMovement(bool force)
    {
        switch (actual_movement)
        {
            case (movement.Run):
                updateRun(force);
                break;
            case (movement.ClimbUp):
                updateClimb();
                break;
            case (movement.ClimbVert):
                break;
            case (movement.Slide):
                updateSlide();
                break;
            case (movement.Jump):
                updateJump();
                break;
        }
    }

    private void updateMovementTransition()
    {
        if(t < 1.0f)
        {
            t += speed * transition_speed * Time.deltaTime;
            transform.position = Vector3.Lerp(start_position, end_position, t);
            transform.rotation = Quaternion.Slerp(start_rotation, end_rotation, t);
        }
        else
        {
            t = 0;
            updateMovement(true);
            if(actual_movement != movement.Jump) setCheckpoint();
            movement_transition = false;
        }
    }

    private void FixedUpdate()
    {
        if (dying == -1 && path != null)
        {
            if (movement_transition)
            {
                updateMovementTransition();
            }
            else
            {
                updateMovement(false);
            }
        }
    }

    bool InteractionKeyDown()
    {
        if(gameObject.tag.Equals("player1"))
        {
            return Input.GetKey(KeyCode.Space);
        }
        else if (gameObject.tag.Equals("player2"))
        {
            return Input.GetKey(KeyCode.Return);
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        moving = InteractionKeyDown() && !end && ProgressController.instance.start;

        rat_anim.SetBool("Moving", moving);
        
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

    public void dieRat(bool respawn)
    {
        stars.Play();
        flysmoke.Play();
        rat_anim.enabled = false;


        RG.isKinematic = false;

        float randX = Random.Range(-300, 300);
        float randZ = Random.Range(-300, 300);

        RG.AddForce(deathJump + new Vector3(randX, 0, randZ));
        RG.AddTorque(new Vector3(Random.Range(-300, 300), Random.Range(-300, 300), Random.Range(-300, 300)));
        if(respawn) dying = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            if (!godMode && dying == -1) {
                boingSource.PlayOneShot(boingSound);
                dieRat(true);
            }
        }
    }
}
