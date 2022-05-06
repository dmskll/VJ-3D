using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MuslitoScript : MonoBehaviour
{
    public GameObject gameObjInicio, gameObjFinal;
    public float velocidad;
    private Vector3 DirectionNormalized;
    public enum MuslitoType { Tp_al_inicio, boomerang };
    public MuslitoType mus;
    private Vector3 posIni, posFin;
    private float distance, distance_traveled;

    private bool direction;

    // Start is called before the first frame update
    void Start()
    {

        posIni = gameObjInicio.GetComponent<Transform>().position;
        posFin = gameObjFinal.GetComponent<Transform>().position;
        gameObject.transform.position = posIni;
        DirectionNormalized = Vector3.Normalize(posFin - posIni);


        distance = Vector3.Distance(posFin,posIni);
        distance_traveled = 0;
        // Convert to -180 to +180 degrees
        transform.LookAt(gameObjFinal.GetComponent<Transform>());
        transform.Rotate(new Vector3(0, 90,0));

        gameObjInicio.GetComponent<MeshRenderer>().enabled = false;
        gameObjFinal.GetComponent<MeshRenderer>().enabled = false;

        direction = true;

    }

    // Update is called once per frame
    void Update()
    {



        if (mus == MuslitoType.boomerang) {
            if (direction)
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * velocidad * 360));
                gameObject.transform.position += DirectionNormalized * velocidad * Time.deltaTime;
                distance_traveled += velocidad * Time.deltaTime;
                if (distance_traveled > distance)
                {
                    direction = !direction;
                }
            }
            else {


                transform.Rotate(new Vector3(0, 0, Time.deltaTime * velocidad * -360));
                gameObject.transform.position -= DirectionNormalized * velocidad * Time.deltaTime;
                distance_traveled -= velocidad * Time.deltaTime;

                if (distance_traveled < 0)
                {
                    direction = !direction;
                }
            }
        }
        else {

            transform.Rotate(new Vector3(0, 0, Time.deltaTime * velocidad * 360));
            gameObject.transform.position += DirectionNormalized * velocidad * Time.deltaTime;
            distance_traveled += velocidad * Time.deltaTime;
            if (distance_traveled > distance) {
                distance_traveled = 0;
                transform.position = posIni;
            }
            
        }



    }
}
