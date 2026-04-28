using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject player;
    public Transform[] spawnPoints;

    public int zombiesToSpawn = 1;
    public int maxZombies = 10;

    private int zombiesAlive = 0;

    void Start()
    {
        SpawnWave();
    }

    public void ZombieDied()
    {
        zombiesAlive--;

        if (zombiesAlive <= 0)
        {
            zombiesToSpawn *= 2;

            if (zombiesToSpawn > maxZombies)
            {
                zombiesToSpawn = maxZombies;
            }

            SpawnWave();
        }
    }

    void SpawnWave()
    {
        zombiesAlive = zombiesToSpawn;

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];

            GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

            ZombieChase chase = zombie.GetComponent<ZombieChase>();
            if (chase != null)
            {
                chase.player = player.transform;
            }

            EnemyHealth health = zombie.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.spawner = this;
            }
        }
    }
}
