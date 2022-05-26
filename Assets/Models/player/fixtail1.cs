using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class fixtail1 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Mesh mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
