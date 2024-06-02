using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using TMPro; // Import the TMPro namespace
using UnityEngine; // Import the UnityEngine namespace

public class PlayerMovement : MonoBehaviour
{
    public int runSpeed = 1; // Movement speed of the player
    public int bulletSpeed; // Speed of bullets fired by the player
    public static int punchDamage = 1; // Damage dealt by player's punch
    public static int slamDamage = 3; // Damage dealt by player's slam attack
    public Transform shootingPoint; // Point where bullets are spawned
    public GameObject bulletPrefab; // Prefab of the bullet
    public GameObject block; // GameObject representing the blocking action
    public bool isBlocking = false; // Flag indicating if player is blocking
    public int currentHealth; // Current health of the player
    public int maxHealth; // Maximum health of the player
    public Healthbar Healthbar; // Reference to the Healthbar script for managing health UI
    public bool isRotated = false; // Flag indicating if player is rotated
    public string newTag; // New tag to be assigned to bullets
    public TextMeshProUGUI scoreText; // Text UI element for displaying score
    public static int score = 0; // Current score of the player
    public GameObject BossSpawn; // GameObject representing boss spawn point
    public Animator animator; // Reference to the Animator component for player animations
    private string anim; // String to store the name of the animation being played

    // Variables for input axes
    float horizontal;
    float vertical;

    private float nextFireTime = 0f; // Time for next bullet fire
    public float fireRate = 0.5f; // Rate of fire for bullets
    private float nextSlamTime = 0f; // Time for next slam attack
    public float slamRate = 0.5f; // Rate of slam attacks

    // Start is called before the first frame update
    void Start()
    {
        score = 0; // Reset score
        currentHealth = maxHealth; // Set current health to max health
        Healthbar.SetMaxHealth(maxHealth); // Set maximum health in health bar UI
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for horizontal and vertical axes
        horizontal = Input.GetAxis("Horizontal Arrow Keys");
        vertical = Input.GetAxis("Vertical Arrow Keys");

        // Update score text UI
        scoreText.text = "Score: " + score;

        // Handle horizontal movement and animations
        if (horizontal < 0)
        {
            animator.SetBool("Moving", true); // Set moving animation parameter
            RotateObject(); // Rotate left
        }
        else if (horizontal > 0)
        {
            animator.SetBool("Moving", true); // Set moving animation parameter
            ResetRotation(); // Reset rotation
        }
        else if (vertical > 0 || vertical < 0)
        {
            animator.SetBool("Moving", true); // Set moving animation parameter
        }
        else
        {
            animator.SetBool("Moving", false); // Set moving animation parameter to false
        }

        // Handle punch attack
        if (Time.time >= nextFireTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.Keypad5))
            {
                animator.SetBool("Punching", true); // Set punching animation parameter
                anim = "Punching"; // Store animation name
                newTag = "Bullet"; // Assign new tag to bullet
                shootBullet(); // Execute bullet firing
                nextFireTime = Time.time + fireRate; // Set next fire time
            }
        }

        // Handle slam attack
        if (Time.time >= nextSlamTime && isBlocking != true)
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                animator.SetBool("Slamming", true); // Set slamming animation parameter
                anim = "Slamming"; // Store animation name
                newTag = "player1Slam"; // Assign new tag to bullet
                shootBullet(); // Execute bullet firing
                nextSlamTime = Time.time + slamRate; // Set next slam time
            }
        }

        // Handle blocking action
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            block.SetActive(true); // Activate blocking object
            animator.SetBool("Blocking", true); // Set blocking animation parameter
            isBlocking = true; // Set blocking flag
        }
        else if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            block.SetActive(false); // Deactivate blocking object
            animator.SetBool("Blocking", false); // Set blocking animation parameter to false
            isBlocking = false; // Set blocking flag to false
        }

        // Update health in health bar UI
        Healthbar.SetHealth(currentHealth);
    }

    // FixedUpdate is called a fixed number of times per second
    private void FixedUpdate()
    {
        // Move player based on input axes
        Vector3 movement = new Vector3(horizontal * runSpeed, vertical * runSpeed, 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;
    }

    // Handle collisions with other objects
    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        // Check collision tags and take appropriate actions
        if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("EnemyBullet"))
        {
            TakeDamage(Enemy.damage); // Take damage from enemy bullet
        }
        else if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("BossBullet"))
        {
            TakeDamage(Boss.damage); // Take damage from boss bullet
        }
        else if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("Respawn"))
        {
            BossSpawn.SetActive(false); // Deactivate boss spawn point
            EnemySpawner.bossSpawn = true; // Set boss spawn flag
        }
    }

    // Method to handle player taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Decrease current health
        if (currentHealth <= 0)
        {
            PlayerPrefs.SetInt("Player1Score", score); // Save player score
            Healthbar.SetHealth(currentHealth); // Update health bar UI
            Destroy(gameObject); // Destroy player GameObject
        }
    }

    // Method to rotate player left
    void RotateObject()
    {
        if (!isRotated)
        {
            transform.Rotate(0, 180, 0); // Rotate by 180 degrees on the Y-axis
            isRotated = true; // Set the flag to true
        }
    }

    // Method to reset player rotation
    void ResetRotation()
    {
        if (isRotated)
        {
            transform.Rotate(0, -180, 0); // Reset rotation by rotating back by 180 degrees on the Y-axis
            isRotated = false; // Reset the flag to false
        }
    }

    // Method to shoot bullets
    private void shootBullet()
    {
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation); // Instantiate bullet prefab
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right *
                bulletSpeed; // Set bullet velocity
        bullet.tag = newTag; // Assign new tag to the bullet

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
