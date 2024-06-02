using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the UnityEngine namespace

public class InvisPunchBullet : MonoBehaviour
{
    public float life = 1; // Lifetime of the bullet in seconds

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Destroy the bullet after 'life' seconds
        Destroy(gameObject, life);
    }

    // Called when the bullet collides with another object
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object's tag is not "Enemy", "Player", or "Player2"
        if (collision.collider.tag != "Enemy" && collision.collider.tag != "Player" && collision.collider.tag != "Player2")
        {
            // Destroy the bullet on collision
            Destroy(gameObject);
        }
    }
}
