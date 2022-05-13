using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class PathChanger : MonoBehaviour
{
    //gestiona el cambio de paths
    public PathCreator path_in, path_out;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("rat"))
        {
            pc.SetPath(path_in, path_in.tag);
        }
    }
}
