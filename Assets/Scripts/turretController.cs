using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretController : MonoBehaviour
{
    public GameObject bulletPrefab;

    private float timeToShoot = 0;
    public float fireRate = 2000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;
        bool canShoot = false;
        timeToShoot += delta;
        if (timeToShoot > fireRate)
        {
            canShoot = true;
            timeToShoot = 0;
        }

        if (canShoot)
        {
            timeToShoot = 0;
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x - 1, transform.position.y, 1), transform.rotation);
            Destroy(bullet, 3);
        }
    }
}
