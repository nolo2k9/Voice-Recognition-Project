using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusProjectileScript : MonoBehaviour
{

    Vector3 respawn = new Vector3(0,-3,0);
    void Update()    {
        transform.Translate(new Vector3(0, -5 * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag =="Player")
        {
            Destroy(this.gameObject);
            GameManager.lives --;
            if(GameManager.lives < 1)
            {
                Destroy(collision.gameObject);
            }
           
           
        }
    }
}
