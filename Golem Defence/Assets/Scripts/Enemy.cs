using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHealth;
    public int bulletSpeed;
    public int maxhealth = 25;
    public static int damage = 10;
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public bool inRange = false;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public GameObject[] targets;
    public bool isBoss;

    private GameObject currentTarget;
    private float targetDistance;

    private float nextFireTime = 0f;
    public float fireRate = 0.5f;

    private bool isRotated = false;

    private void Start()
    {
        if (gameObject.CompareTag("Boss"))
        {
            isBoss = true;
        }
        else
        {
            isBoss = false;
        }
        currentHealth = maxhealth;
        currentTarget = targets[Random.Range(0, targets.Length)];
    }

    void Update()
    {
        if (currentTarget == null || !currentTarget.activeSelf)
        {
            FindNewTarget();
            if (currentTarget == null)
            {
                // No valid target found, return or handle accordingly
                return;
            }
        }

        targetDistance = Vector2.Distance(transform.position, currentTarget.transform.position);
        if (targetDistance < chaseDistance && targetDistance > stopDistance)
        {
            ChasePlayer();
        }
        else
        {
            StopChasePlayer();
        }

        if (Time.time >= nextFireTime && inRange == true)
        {
            shootBullet();
            nextFireTime = Time.time + fireRate;
        }

        if (targetDistance > stopDistance && currentTarget.transform.position.x < transform.position.x)
        {
            RotateObject();
        }
        else if (targetDistance > stopDistance && currentTarget.transform.position.x > transform.position.x)
        {
            ResetRotation();
        }

    }

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

    private void StopChasePlayer()
    {
        inRange = true;
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        inRange = false;
        /*if (transform.position.x < target.transform.position.x)
        {

        }*/
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PlayerMovement.score += 10;
            Destroy(gameObject);
        }
    }

    public void TakeDamage2(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Player2Movement.score += 10;
            Destroy(gameObject);
        }
    }

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

    void RotateObject()
    {
        if (!isRotated)
        {
            transform.Rotate(0, -180, 0); // Rotate by 180 degrees on the Y-axis
            isRotated = true; // Set the flag to true
        }
    }

    void ResetRotation()
    {
        if (isRotated)
        {
            transform.Rotate(0, 180, 0); // Reset rotation by rotating back by 180 degrees on the Y-axis
            isRotated = false; // Reset the flag to false
        }
    }

    private void shootBullet()
    {
        var bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingPoint.right * bulletSpeed;
    }
}
