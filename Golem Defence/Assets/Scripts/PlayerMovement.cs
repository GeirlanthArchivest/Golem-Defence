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
    public GameObject block;
    public bool isBlocking = false;
    public bool isPlayer1 = false;

    float horizontal;
    float vertical;

    private float nextFireTime = 0f;
    public float fireRate = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            isPlayer1 = true;
        }
        else
        {
            isPlayer1 = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1 == true)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else if (isPlayer1 == false)
        {
            horizontal = Input.GetAxis("Horizontal Arrow Keys");
            vertical = Input.GetAxis("Vertical Arrow Keys");
        }


        if (Time.time >= nextFireTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                shootBullet();
                nextFireTime = Time.time + fireRate;
            }
        }

       /* if (Time.time >= nextFireTime)
        {
            if (Input.GetKey(KeyCode.K))
            {
                shootBullet2();
                nextFireTime = Time.time + fireRate;
            }
        }*/

        if (Input.GetKeyDown(KeyCode.L))
        {
            // Activate the object
            block.SetActive(true);
            isBlocking = true;
        }

        // Check if the activation key is released
        else if (Input.GetKeyUp(KeyCode.L))
        {
            // Deactivate the object
            block.SetActive(false);
            isBlocking = false;
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

    /*private void shootBullet2()
    {
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed;
    }*/
}
