using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHealth;
    public int maxhealth = 5;
    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public GameObject target;

    private float targetDistance;

    private void Start()
    {
        currentHealth = maxhealth;
    }

    void Update()
    {
        targetDistance = Vector2.Distance(transform.position, target.transform.position);
        if (targetDistance < chaseDistance && targetDistance > stopDistance)
        {
            ChasePlayer();
        }
        else
        {
            StopChasePlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("Bullet"))
        {
            TakeDamage(PlayerMovement.damage);
        }
    }

    private void StopChasePlayer()
    {

    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        /*if (transform.position.x < target.transform.position.x)
        {

        }*/
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
