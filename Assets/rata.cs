using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rata : MonoBehaviour
{

    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
 
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);

    }
}
