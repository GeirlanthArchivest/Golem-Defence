using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnpoints;

    public GameObject enemy;

    public int numberOfEnemies = 5;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int r = Random.Range(0, spawnpoints.Length);
            GameObject Enemy = Instantiate(enemy, spawnpoints[r].position, Quaternion.identity);

            GameManager gameManager = GameManager.instance;
            if (gameManager != null)
            {
                Enemy enemyScript = Enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.targets = gameManager.players;
                }
            }
        }    
        
    }
}
