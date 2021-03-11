using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusProjectileScript : MonoBehaviour
{

    void Update()
    {
        transform.Translate(new Vector3(0, -5 * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.gameObject.tag =="Player")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
