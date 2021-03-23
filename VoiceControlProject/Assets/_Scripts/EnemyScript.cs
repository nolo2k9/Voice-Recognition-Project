using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // timer
    float timer = 0;
    //variable to control when eenemy moves
    float timeToMove = 0.5f;
    //Number of movements
    int numberOfMovements = 0;
    //Movement speed
    float speed = 0.75f;
    public GameObject virusProjectile;
    public GameObject virusProjectileClone;
    public GameObject virus;

    // Update is called once per frame
    void Update()
    {   // Set timer
        timer += Time.deltaTime;
        //if within constraints move
        if (timer > timeToMove && numberOfMovements < 14)
        {
            transform.Translate(new Vector3(speed, 0, 0));
            //set timer
            timer = 0;
            //add movements eeach time they move
            numberOfMovements ++;

        }
        //14 movements
        if(numberOfMovements == 14)
        {   //move opposite direction
            transform.Translate(new Vector3(0.5f, -1, 0));
            //reset
            numberOfMovements = -1;
            //set speed
            speed = -speed;
            //set timer
            timer = 0;

        }
        //attack
        EnemyAttack();
    }

    void EnemyAttack()
    {   //Attack at random intervals
        if(Random.Range(0f, 500f) < 1)
        {   //instatniate projectile
            virusProjectileClone = Instantiate(virusProjectile, new Vector3(virus.transform.position.x, virus.transform.position.y - 0.4f, 0), virus.transform.rotation) as GameObject;

        }
    }
    //if collision is with player
    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag =="Player")
        {
            //destory object
            Destroy(this.gameObject);
            //take life
            GameManager.lives --;
           
           
        }
    }
}
