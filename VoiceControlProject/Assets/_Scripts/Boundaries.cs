using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{

    
    void Update()
    {
        //Stay within given boundaries
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -18f, 16f),
        Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);
    }

}
