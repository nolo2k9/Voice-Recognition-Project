using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    float timer = 0;
    float timeToMove = 0.5f;
    int numberOfMovements = 0;

    float speed = 0.25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToMove && numberOfMovements < 14)
        {
            transform.Translate(new Vector3(speed, 0, 0));
            timer = 0;
            numberOfMovements ++;

        }
        //make random 
        if(numberOfMovements == 14)
        {
            transform.Translate(new Vector3(0.5f, -1, 0));
            numberOfMovements = -1;
            speed = -speed;
            timer = 0;

        }
    }
}
