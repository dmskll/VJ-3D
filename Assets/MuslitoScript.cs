using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuslitoScript : MonoBehaviour
{
    public Vector3 posInicial, posFinal;
    public float velocidad;
    private Vector3 DirectionNormalized;
    private Vector3 RotationAngle;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = posInicial;
        DirectionNormalized = Vector3.Normalize(posFinal - posInicial);

        RotationAngle = new Vector3(0, Vector3.Angle(DirectionNormalized, new Vector3(1, 0, 0)), 0);
        gameObject.transform.eulerAngles = RotationAngle;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += DirectionNormalized * velocidad * Time.deltaTime;
        RotationAngle.z += Time.deltaTime * velocidad * -360;
        gameObject.transform.eulerAngles = RotationAngle;
    }
}
