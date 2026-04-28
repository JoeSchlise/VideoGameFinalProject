using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    public ZombieSpawner spawner;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            if (spawner != null)
            {
                spawner.ZombieDied();
            }

            Destroy(gameObject);
        }
    }
}
