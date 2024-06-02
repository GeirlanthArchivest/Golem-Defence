using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Boss attributes
    public int currentHealth;
    public int bulletSpeed;
    public int maxhealth = 25;
    public static int damage = 15;
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public bool inRange = false;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public GameObject[] targets;
    public Animator animator;

    // Internal variables
    private GameObject currentTarget;
    private float targetDistance;
    private float nextFireTime = 0f;
    public float fireRate = 0.5f;
    private bool isRotated = false;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxhealth;
        currentTarget = targets[Random.Range(0, targets.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current target is valid
        if (currentTarget == null || !currentTarget.activeSelf)
        {
            FindNewTarget();
            if (currentTarget == null)
            {
                // No valid target found, return or handle accordingly
                return;
            }
        }

        // Calculate the distance between the boss and its target
        targetDistance = Vector2.Distance(transform.position, currentTarget.transform.position);

        // Check if the target is within chase distance
        if (targetDistance < chaseDistance && targetDistance > stopDistance)
        {
            ChasePlayer();
        }
        else
        {
            StopChasePlayer();
        }

        // Check if it's time to fire a bullet
        if (Time.time >= nextFireTime && inRange == true)
        {
            shootBullet();
            nextFireTime = Time.time + fireRate;
        }

        // Rotate the boss if needed
        if (targetDistance > stopDistance && currentTarget.transform.position.x < transform.position.x)
        {
            RotateObject();
        }
        else if (targetDistance > stopDistance && currentTarget.transform.position.x > transform.position.x)
        {
            ResetRotation();
        }
    }

    // Handle collision with player attacks
    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
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

    // Method to stop chasing the player
    private void StopChasePlayer()
    {
        animator.SetBool("Walking", false);
        inRange = true;
    }

    // Method to chase the player
    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        inRange = false;
        animator.SetBool("Walking", true);
    }

    // Method to take damage from player 1
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PlayerMovement.score += 50;
            Destroy(gameObject);
        }
    }

    // Method to take damage from player 2
    public void TakeDamage2(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Player2Movement.score += 50;
            Destroy(gameObject);
        }
    }

    // Method to find a new target (player)
    private void FindNewTarget()
    {
        foreach (GameObject potentialTarget in targets)
        {
            if (potentialTarget != null && potentialTarget.activeSelf)
            {
                currentTarget = potentialTarget;
                return; // Found a new target, exit the loop
            }
        }
        // No active targets found
        currentTarget = null;
    }

    // Method to rotate the boss
    void RotateObject()
    {
        if (!isRotated)
        {
            transform.Rotate(0, -180, 0); // Rotate by 180 degrees on the Y-axis
            isRotated = true; // Set the flag to true
        }
    }

    // Method to reset the boss rotation
    void ResetRotation()
    {
        if (isRotated)
        {
            transform.Rotate(0, 180, 0); // Reset rotation by rotating back by 180 degrees on the Y-axis
            isRotated = false; // Reset the flag to false
        }
    }

    // Method to make the boss shoot a bullet
    private void shootBullet()
    {
        animator.SetBool("Punching", true);
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed;
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

