using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusProjectileScript : MonoBehaviour
{   
    Vector3 respawn = new Vector3(0,-3,0);
    //projectile direction
    void Update()    {
        transform.Translate(new Vector3(0, -5 * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision){
        //if collides with player
        if (collision.gameObject.tag =="Player")
        {   //destroy projectile
            Destroy(this.gameObject);
            //take a life
            GameManager.lives --;
   
        }
    }
}
