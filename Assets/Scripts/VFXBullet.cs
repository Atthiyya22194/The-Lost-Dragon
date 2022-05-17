using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXBullet : MonoBehaviour
{
    private bool collided;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            //Destroy(this.gameObject, 2f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().enemyHealth -= 20f;
            //other.gameObject.GetComponent<GruntBoss>().enemyHealth -= 20f;
        }
        Destroy(this.gameObject, 2f);
        //FindObjectOfType<SFXmanager>().PlayHurt();
    }
}
