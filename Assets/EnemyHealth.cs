using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    public ZombieSpawner spawner;

    public GameObject betterGunPrefab;  
    public float dropChance = 0.1f;     

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (Random.value <= dropChance)
        {
            Instantiate(
                betterGunPrefab,
                transform.position + Vector3.up * 1f,
                Quaternion.identity
            );
        }

        if (spawner != null)
        {
            spawner.ZombieDied();
        }

        Destroy(gameObject);
    }
}