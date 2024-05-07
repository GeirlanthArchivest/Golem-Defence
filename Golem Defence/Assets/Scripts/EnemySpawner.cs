using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnpoints;

    public Transform bossSpawnPoint;

    public GameObject enemy;

    public GameObject bossPrefab;

    public static bool bossSpawn = false;

    public static bool bossSpawned = false;

    public int numberOfEnemies = 3;

    // Start is called before the first frame update
    void Start()
    {
        bossSpawned = false;
        //InvokeRepeating("SpawnEnemy", 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (bossSpawn == true && bossSpawned == false)
        {
            bossSpawned = true;
            SpawnBoss();
        }
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
    void SpawnBoss()
    {
        GameObject Boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

        GameManager gameManager = GameManager.instance;
        if (gameManager != null)
        {
            Boss enemyScript = Boss.GetComponent<Boss>(); // Accessing the component of the instantiated boss
            if (enemyScript != null)
            {
                enemyScript.targets = gameManager.players;
            }
        }
    }
}
