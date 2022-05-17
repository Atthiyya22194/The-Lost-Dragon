using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSShooter : MonoBehaviour
{
    //[SerializeField]
    //private EnemyHealth enemyHealth;
    public float damage = 20f;
    public Camera cam;
    public GameObject projectile;
    public float projectileSpeed = 30f;
    public Transform fireShoot;
    public float fireRate = 4;
    private Vector3 destination;
    private float timeToFire;
    private float arcRange = 1;

    // Start is called before the first frame update
    void Start()
    {
        //EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 6 / fireRate;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            //enemyHealth.DeductHealth(damage);
            destination = hit.point;
            //Debug.Log(hit.transform.name);
        }
        else
        {
            destination = ray.GetPoint(1000);
            //Debug.Log(hit.transform.name);
        }

        InstantiateProjectile(fireShoot);
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2));
    }
}
