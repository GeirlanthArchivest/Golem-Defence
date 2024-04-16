using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int runSpeed = 1;
    public int bulletSpeed;
    public static int damage = 1;
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    float horizontal;
    float vertical;

    private float nextFireTime = 0f;
    public float fireRate = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Time.time >= nextFireTime)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                shootBullet();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontal * runSpeed, vertical * runSpeed, 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;
    }

    private void shootBullet()
    {
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed;
    }
}
