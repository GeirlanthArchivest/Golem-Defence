using System.Collections;
using System.Collections.Generic;
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
    public int currentHealth;
    public int maxHealth;
    public Healthbar Healthbar;


    float horizontal;
    float vertical;

    private float nextFireTime = 0f;
    public float fireRate = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Healthbar.SetMaxHealth(maxHealth);

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

            if (Time.time >= nextFireTime && isBlocking != true)
            {
                if (Input.GetKey(KeyCode.J))
                {
                    shootBullet();
                    nextFireTime = Time.time + fireRate;
                }
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                block.SetActive(true);
                isBlocking = true;
            }

            else if (Input.GetKeyUp(KeyCode.L))
            {
                block.SetActive(false);
                isBlocking = false;
            }
        }
        else if (isPlayer1 == false)
        {
            horizontal = Input.GetAxis("Horizontal Arrow Keys");
            vertical = Input.GetAxis("Vertical Arrow Keys");

            if (Time.time >= nextFireTime && isBlocking != true)
            {
                if (Input.GetKey(KeyCode.Keypad5))
                {
                    shootBullet();
                    nextFireTime = Time.time + fireRate;
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                block.SetActive(true);
                isBlocking = true;
            }

            else if (Input.GetKeyUp(KeyCode.Keypad6))
            {
                block.SetActive(false);
                isBlocking = false;
            }
        }


        Healthbar.SetHealth(currentHealth);

        /* if (Time.time >= nextFireTime)
         {
             if (Input.GetKey(KeyCode.K))
             {
                 shootBullet2();
                 nextFireTime = Time.time + fireRate;
             }
         }*/


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
