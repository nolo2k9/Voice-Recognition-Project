using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -15f, 15f),
        Mathf.Clamp(transform.position.y, -4f, 4f), transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag =="Wall")
        {
           
        }
    }
}
