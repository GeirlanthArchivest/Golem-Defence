using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int runSpeed = 1;
    public int bulletSpeed;
    public static int punchDamage = 1;
    public static int slamDamage = 3;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public GameObject block;
    public bool isBlocking = false;
    public int currentHealth;
    public int maxHealth;
    public Healthbar Healthbar;
    public bool isRotated = false;
    public string newTag;
    public TextMeshProUGUI scoreText;
    public static int score = 0;

    float horizontal;
    float vertical;

    private float nextFireTime = 0f;
    public float fireRate = 0.5f;
    private float nextSlamTime = 0f;
    public float slamRate = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal Arrow Keys");
        vertical = Input.GetAxis("Vertical Arrow Keys");

        scoreText.text = "Score: " + score;

        if (horizontal < 0)
        {
            RotateObject(); // Rotate left
        }
        else if (horizontal > 0)
        {
            ResetRotation(); // Reset rotation
        }

        if (Time.time >= nextFireTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.Keypad5))
            {
                newTag = "Bullet";
                shootBullet();
                nextFireTime = Time.time + fireRate;
            }
        }

        if (Time.time >= nextSlamTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                newTag = "player1Slam";
                shootBullet();
                nextSlamTime = Time.time + slamRate;
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
    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("EnemyBullet"))
        {
            TakeDamage(Enemy.damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Healthbar.SetHealth(currentHealth);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontal * runSpeed, vertical * runSpeed, 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;
    }

    void RotateObject()
    {
        if (!isRotated)
        {
            transform.Rotate(0, 180, 0); // Rotate by 180 degrees on the Y-axis
            isRotated = true; // Set the flag to true
        }
    }

    void ResetRotation()
    {
        if (isRotated)
        {
            transform.Rotate(0, -180, 0); // Reset rotation by rotating back by 180 degrees on the Y-axis
            isRotated = false; // Reset the flag to false
        }
    }

    private void shootBullet()
    {
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed;
        bullet.tag = newTag;
    }

    /*private void shootBullet2()
    {
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed;
    }*/
}