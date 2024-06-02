using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the UnityEngine namespace

public class Enemy : MonoBehaviour
{
    public int currentHealth; // Current health of the enemy
    public int bulletSpeed; // Speed of the bullets fired by the enemy
    public int maxhealth = 25; // Maximum health of the enemy
    public static int damage = 10; // Damage dealt by the enemy
    public float speed; // Movement speed of the enemy
    public float chaseDistance; // Distance within which the enemy starts chasing the player
    public float stopDistance; // Distance within which the enemy stops chasing the player
    public bool inRange = false; // Flag to check if the enemy is in range to attack
    public Transform shootingPoint; // Transform representing the shooting point of the enemy
    public GameObject bulletPrefab; // Prefab of the bullet fired by the enemy
    public GameObject[] targets; // Array of potential targets (players)
    public bool isBoss; // Flag to check if the enemy is a boss
    public Animator animator; // Animator component for controlling animations

    private GameObject currentTarget; // Current target of the enemy
    private float targetDistance; // Distance to the current target

    private float nextFireTime = 0f; // Time until the next bullet can be fired
    public float fireRate = 0.5f; // Rate of fire for the enemy

    private bool isRotated = false; // Flag to check if the enemy is rotated

    // Start is called before the first frame update
    private void Start()
    {
        if (gameObject.CompareTag("Boss"))
        {
            isBoss = true; // Set the isBoss flag if the enemy is tagged as "Boss"
        }
        else
        {
            isBoss = false; // Otherwise, set the isBoss flag to false
        }
        currentHealth = maxhealth; // Initialize current health to max health
        currentTarget = targets[Random.Range(0, targets.Length)]; // Select a random target from the targets array
    }

    // Update is called once per frame
    void Update()
    {
        // If the current target is null or inactive, find a new target
        if (currentTarget == null || !currentTarget.activeSelf)
        {
            FindNewTarget();
            if (currentTarget == null)
            {
                // No valid target found, return or handle accordingly
                return;
            }
        }

        // Calculate the distance to the current target
        targetDistance = Vector2.Distance(transform.position, currentTarget.transform.position);

        // If within chase distance but outside stop distance, chase the target
        if (targetDistance < chaseDistance && targetDistance > stopDistance)
        {
            ChasePlayer();
        }
        else
        {
            StopChasePlayer(); // Stop chasing the target
        }

        // If ready to fire and in range, shoot a bullet
        if (Time.time >= nextFireTime && inRange == true)
        {
            shootBullet();
            nextFireTime = Time.time + fireRate; // Update the next fire time
        }

        // Rotate towards the target based on its position relative to the enemy
        if (targetDistance > stopDistance && currentTarget.transform.position.x < transform.position.x)
        {
            RotateObject();
        }
        else if (targetDistance > stopDistance && currentTarget.transform.position.x > transform.position.x)
        {
            ResetRotation();
        }
    }

    // Called when the enemy collides with another object
    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        // Check if the collided object is a bullet or player attack and apply damage accordingly
        if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("Bullet"))
        {
            TakeDamage(PlayerMovement.punchDamage);
        }
        else if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("player1Slam"))
        {
            TakeDamage(PlayerMovement.slamDamage);
        }
        else if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("Player2Bullet"))
        {
            TakeDamage2(Player2Movement.punchDamage);
        }
        else if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("Player2Slam"))
        {
            TakeDamage2(PlayerMovement.slamDamage);
        }
    }

    // Stop chasing the player
    private void StopChasePlayer()
    {
        inRange = true; // Set in range flag to true
        animator.SetBool("Walking", false); // Set walking animation to false
    }

    // Chase the player
    private void ChasePlayer()
    {
        // Move towards the current target
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        inRange = false; // Set in range flag to false
        animator.SetBool("Walking", true); // Set walking animation to true
    }

    // Apply damage to the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by the damage amount
        if (currentHealth <= 0)
        {
            PlayerMovement.score += 10; // Increase player score on enemy death
            Destroy(gameObject); // Destroy the enemy object
        }
    }

    // Apply damage to the enemy from Player 2
    public void TakeDamage2(int damage)
    {
        currentHealth -= damage; // Reduce current health by the damage amount
        if (currentHealth <= 0)
        {
            Player2Movement.score += 10; // Increase Player 2 score on enemy death
            Destroy(gameObject); // Destroy the enemy object
        }
    }

    // Find a new target from the list of targets
    private void FindNewTarget()
    {
        foreach (GameObject potentialTarget in targets)
        {
            if (potentialTarget != null && potentialTarget.activeSelf)
            {
                currentTarget = potentialTarget; // Set the current target to the new target
                return; // Found a new target, exit the loop
            }
        }
        // No active targets found
        currentTarget = null;
    }

    // Rotate the enemy to face the target
    void RotateObject()
    {
        if (!isRotated)
        {
            transform.Rotate(0, -180, 0); // Rotate by 180 degrees on the Y-axis
            isRotated = true; // Set the flag to true
        }
    }

    // Reset the enemy's rotation
    void ResetRotation()
    {
        if (isRotated)
        {
            transform.Rotate(0, 180, 0); // Reset rotation by rotating back by 180 degrees on the Y-axis
            isRotated = false; // Reset the flag to false
        }
    }

    // Shoot a bullet towards the target
    private void shootBullet()
    {
        animator.SetBool("Punching", true); // Set punching animation to true
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation); // Instantiate the bullet
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed; // Set the bullet's velocity
        StartCoroutine(ResetPunchAnimation()); // Reset the punching animation after a short delay
    }

    // Coroutine to reset the punching animation
    private IEnumerator ResetPunchAnimation()
    {
        yield return new WaitForSeconds(0.2f); // Wait for a short duration
        animator.SetBool("Punching", false); // Reset the punching animation
    }
}
