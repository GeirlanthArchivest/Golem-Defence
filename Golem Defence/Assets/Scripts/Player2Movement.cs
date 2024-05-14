using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player2Movement : MonoBehaviour
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
    public GameObject BossSpawn;
    public Animator animator;

    float horizontal;
    float vertical;

    private float nextFireTime = 0f;
    public float fireRate = 0.5f;
    private float nextSlamTime = 0f;
    public float slamRate = 0.5f;



    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        currentHealth = maxHealth;
        Healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        scoreText.text = "Score: " + score;


        if (horizontal < 0)
        {
            animator.SetBool("Moving", true);
            RotateObject(); // Rotate left
        }
        else if (horizontal > 0)
        {
            animator.SetBool("Moving", true);
            ResetRotation(); // Reset rotation
        }
        else if (vertical > 0 || vertical < 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        if (Time.time >= nextFireTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.K))
            {
                newTag = "Player2Bullet";
                shootBullet();
                nextFireTime = Time.time + fireRate;
            }
        }

        if (Time.time >= nextSlamTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.O))
            {
                newTag = "Player2Slam";
                shootBullet();
                nextSlamTime = Time.time + slamRate;
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

    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("EnemyBullet"))
        {
            TakeDamage(Enemy.damage);
        }
        else if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("BossBullet"))
        {
            TakeDamage(Boss.damage);
        }
        else if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("Respawn"))
        {
            BossSpawn.SetActive(false);
            EnemySpawner.bossSpawn = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PlayerPrefs.SetInt("Player1Score", score);
            Healthbar.SetHealth(currentHealth);
            Destroy(gameObject);
        }
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
        animator.SetBool("Punching", true);
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed;
        bullet.tag = newTag;
        StartCoroutine(ResetPunchAnimation());
    }

    // Coroutine to reset the punching animation after a short delay
    private IEnumerator ResetPunchAnimation()
    {
        // Wait for a short duration
        yield return new WaitForSeconds(0.2f); // Adjust the duration as needed

        // Reset the punching animation
        animator.SetBool("Punching", false);
    }
}