using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXShooter : MonoBehaviour
{    
    //public float damage = 20f;
    public Camera cam;
    public GameObject projectile;
    public Transform fireShoot;
    public float projectileSpeed = 30f;
    public float fireRate = 4;
    private float arcRange = 1;
    private Vector3 destination;
    private float timeToFire;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 5/fireRate;
            StartCoroutine(ShootProjectile());
            //ShootProjectile();
        }
    }

    IEnumerator ShootProjectile()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        //if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
            //Debug.Log(hit.transform.name);
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            enemy.enemyHealth -= 20f;
            //enemyHealth.DeductHealth(damage);
        }
        else
        {
            //Debug.Log(hit.transform.name);
            destination = ray.GetPoint(1000);
        }

        yield return new WaitForSeconds(1f);
        InstantiateProjectile(fireShoot);
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2));
    }
}
