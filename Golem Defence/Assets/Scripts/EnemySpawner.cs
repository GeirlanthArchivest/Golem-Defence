using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Array of spawn points for regular enemies
    public Transform[] spawnpoints;

    // Spawn point for the boss enemy
    public Transform bossSpawnPoint;

    // Prefab for regular enemies
    public GameObject enemy;

    // Prefab for the boss enemy
    public GameObject bossPrefab;

    // Boolean to track if the boss should spawn
    public static bool bossSpawn = false;

    // Boolean to track if the boss has already spawned
    public static bool bossSpawned = false;

    // Number of regular enemies to spawn
    public int numberOfEnemies = 3;

    // Start is called before the first frame update
    void Start()
    {
        bossSpawned = false;
        // Start spawning regular enemies repeatedly after a delay of 0 seconds and repeat every 10 seconds
        InvokeRepeating("SpawnEnemy", 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the boss should spawn and it has not already spawned
        if (bossSpawn == true && bossSpawned == false)
        {
            bossSpawned = true;
            SpawnBoss(); // Spawn the boss
        }
    }

    // Spawn regular enemies
    void SpawnEnemy()
    {
        // Loop to spawn the specified number of regular enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int r = Random.Range(0, spawnpoints.Length); // Get a random index for spawn points
            GameObject Enemy = Instantiate(enemy, spawnpoints[r].position, Quaternion.identity); // Instantiate a regular enemy

            GameManager gameManager = GameManager.instance; // Get the GameManager instance
            if (gameManager != null) // Check if GameManager exists
            {
                Enemy enemyScript = Enemy.GetComponent<Enemy>(); // Get the Enemy component of the instantiated enemy
                if (enemyScript != null) // Check if the Enemy component exists
                {
                    enemyScript.targets = gameManager.players; // Set the targets for the enemy
                }
            }
        }
    }

    // Spawn the boss enemy
    void SpawnBoss()
    {
        // Instantiate the boss enemy at the boss spawn point
        GameObject Boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

        GameManager gameManager = GameManager.instance; // Get the GameManager instance
        if (gameManager != null) // Check if GameManager exists
        {
            Boss enemyScript = Boss.GetComponent<Boss>(); // Get the Boss component of the instantiated boss
            if (enemyScript != null) // Check if the Boss component exists
            {
                enemyScript.targets = gameManager.players; // Set the targets for the boss enemy
            }
        }
    }
}
