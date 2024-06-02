using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    // Movement speed of the player
    public int runSpeed = 1;

    // Speed of the bullets fired by the player
    public int bulletSpeed;

    // Damage inflicted by player's punch
    public static int punchDamage = 1;

    // Damage inflicted by player's slam
    public static int slamDamage = 3;

    // Transform representing the point from which bullets are fired
    public Transform shootingPoint;

    // Prefab of the bullet
    public GameObject bulletPrefab;

    // GameObject representing the blocking object
    public GameObject block;

    // Boolean indicating whether the player is currently blocking
    public bool isBlocking = false;

    // Current health of the player
    public int currentHealth;

    // Maximum health of the player
    public int maxHealth;

    // Reference to the health bar UI element
    public Healthbar Healthbar;

    // Boolean indicating whether the player is rotated
    public bool isRotated = false;

    // Tag to assign to the bullets fired by the player
    public string newTag;

    // Reference to the score text UI element
    public TextMeshProUGUI scoreText;

    // Current score of the player
    public static int score = 0;

    // GameObject representing the boss spawn point
    public GameObject BossSpawn;

    // Reference to the animator component
    public Animator animator;

    // String representing the current animation state
    public string anim;

    // Horizontal input axis
    float horizontal;

    // Vertical input axis
    float vertical;

    // Time when the next bullet can be fired
    private float nextFireTime = 0f;

    // Rate at which bullets can be fired
    public float fireRate = 0.5f;

    // Time when the next slam can be executed
    private float nextSlamTime = 0f;

    // Rate at which slams can be executed
    public float slamRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize player's score and health
        score = 0;
        currentHealth = maxHealth;
        Healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Retrieve input from arrow keys
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Update score text
        scoreText.text = "Score: " + score;

        // Set moving animation state based on input
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

        // Fire bullets based on input and cooldown
        if (Time.time >= nextFireTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.K))
            {
                animator.SetBool("Punching", true);
                anim = "Punching";
                newTag = "Player2Bullet";
                shootBullet();
                nextFireTime = Time.time + fireRate;
            }
        }

        // Execute slam attack based on input and cooldown
        if (Time.time >= nextSlamTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.O))
            {
                animator.SetBool("Slamming", true);
                anim = "Slamming";
                newTag = "Player2Slam";
                shootBullet();
                nextSlamTime = Time.time + slamRate;
            }
        }

        // Activate blocking based on input
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("Blocking", true);
            block.SetActive(true);
            isBlocking = true;
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            animator.SetBool("Blocking", false);
            block.SetActive(false);
            isBlocking = false;
        }

        // Update health bar
        Healthbar.SetHealth(currentHealth);
    }

    // FixedUpdate is called at fixed intervals
    private void FixedUpdate()
    {
        // Move the player based on input
        Vector3 movement = new Vector3(horizontal * runSpeed, vertical * runSpeed, 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;
    }

    // Handle collisions with other objects
    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        // Check collision tags and apply damage accordingly
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

    // Method to handle player taking damage
    public void TakeDamage(int damage)
    {
        // Reduce player's health
        currentHealth -= damage;

        // Check if player's health is depleted
        if (currentHealth <= 0)
        {
            // Update player's score and destroy the player object
            PlayerPrefs.SetInt("Player2Score", score);
            Healthbar.SetHealth(currentHealth);
            Destroy(gameObject);
        }
    }

    // Method to rotate the player object left
    void RotateObject()
    {
        if (!isRotated)
        {
            transform.Rotate(0, 180, 0); // Rotate by 180 degrees on the Y-axis
            isRotated = true; // Set the flag to true
        }
    }

    // Method to reset the rotation of the player object
    void ResetRotation()
    {
        if (isRotated)
        {
            transform.Rotate(0, -180, 0); // Reset rotation by rotating back by 180 degrees on the Y-axis
            isRotated = false; // Reset the flag to false
        }
    }

    // Method to fire bullets
    private void shootBullet()
    {
        // Instantiate a bullet object and set its velocity
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed;
        bullet.tag = newTag; // Assign tag to the bullet

        StartCoroutine(ResetPunchAnimation()); // Start coroutine to reset punching animation
    }

    // Coroutine to reset the punching or slamming animation after a short delay
    private IEnumerator ResetPunchAnimation()
    {
        // Wait for a short duration
        yield return new WaitForSeconds(0.2f); // Adjust the duration as needed

        // Reset the punching or slamming animation
        animator.SetBool(anim, false); // Set the animation parameter to false
    }
}
