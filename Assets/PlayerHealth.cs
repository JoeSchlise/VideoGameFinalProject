using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isDead = false;

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        FindObjectOfType<GameManager>().GameOver();
    }
}
