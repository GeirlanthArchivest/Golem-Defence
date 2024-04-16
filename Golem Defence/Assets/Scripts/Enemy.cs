using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float currentHealth;
    public int maxhealth = 5;

    private void Start()
    {
        currentHealth = maxhealth;
    }


    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        if (collisioninfo.collider != null && collisioninfo.collider.CompareTag("Bullet"))
        {
            TakeDamage(PlayerMovement.damage);
        }
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
