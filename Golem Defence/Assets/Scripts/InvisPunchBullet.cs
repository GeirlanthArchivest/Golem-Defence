using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisPunchBullet : MonoBehaviour
{
    public float life = 1;


    private void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Enemy" && collision.collider.tag != "Player" && collision.collider.tag != "Player2")
        {
            Destroy(gameObject);
        }
    }
}
