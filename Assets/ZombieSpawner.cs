using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject bigZombiePrefab;
    public GameObject fastZombiePrefab;

    public GameObject player;
    public Transform[] spawnPoints;

    public int zombiesToSpawn = 1;
    public int maxZombies = 50;
    
    public TextMeshProUGUI roundsCompletedText;

    private int zombiesAlive = 0;
    private int roundsCompleted = 0;

    void Start()
    {
        UpdateRoundsUI();
        SpawnWave();
    }

    public void ZombieDied()
    {
        zombiesAlive--;

        if (zombiesAlive <= 0)
        {
            roundsCompleted++;
            UpdateRoundsUI();

            zombiesToSpawn = Mathf.CeilToInt(zombiesToSpawn * 1.5f);

            if (zombiesToSpawn > maxZombies)
            {
                zombiesToSpawn = maxZombies;
            }

            SpawnWave();
        }
    }

    void SpawnWave()
    {
        int currentRound = roundsCompleted + 1;

        Debug.Log("ROUND " + currentRound + " spawning " + zombiesToSpawn + " zombies");

        zombiesAlive = zombiesToSpawn;

        bool spawnBigZombie = currentRound % 3 == 0;

        if (spawnBigZombie)
        {
            zombiesAlive++;
            Debug.Log("Big zombie added. Total alive: " + zombiesAlive);
        }

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            GameObject zombieToSpawn = zombiePrefab;

            if (fastZombiePrefab != null && currentRound >= 2 && Random.value < 0.25f)
            {
                zombieToSpawn = fastZombiePrefab;
            }

            SpawnZombie(zombieToSpawn, i);
        }

        if (spawnBigZombie && bigZombiePrefab != null)
        {
            SpawnZombie(bigZombiePrefab, zombiesToSpawn);
        }
    }

    void SpawnZombie(GameObject prefab, int spawnIndex)
    {
        
        Transform spawnPoint = spawnPoints[spawnIndex % spawnPoints.Length];

        Vector3 randomOffset = new Vector3(
            Random.Range(-3f, 3f),
            0,
            Random.Range(-3f, 3f)
        );

        GameObject zombie = Instantiate(
            prefab,
            spawnPoint.position + randomOffset,
            spawnPoint.rotation
        );

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

    void UpdateRoundsUI()
    {
        if (roundsCompletedText != null)
        {
            roundsCompletedText.text = "Rounds Completed: " + roundsCompleted;
        }
    }
}