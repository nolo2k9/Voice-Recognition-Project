using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //projectile direction
    void Update()
    {
        transform.Translate(new Vector3(0, 5 * Time.deltaTime, 0));
    }
    //handle collissions
    private void OnCollisionEnter2D(Collision2D collision){
        //collision with object tags
        if (collision.gameObject.tag =="Covid" || collision.gameObject.tag =="Virus" )
        {
            if(collision.gameObject.tag =="Covid")
            {
                GameManager.points +=5;// give 5 points
            }
            if(collision.gameObject.tag =="Virus")
            {
                GameManager.points +=1;//give 1 point
            }
            //destroy colliding object and projectile
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
           
           
        }
    }
}
