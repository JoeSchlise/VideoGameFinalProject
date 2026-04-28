using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3.5f;
    public float attackDistance = 2f;

    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed = speed;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            agent.isStopped = true;
            anim.Play("zombie_attack");
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            anim.Play("zombie_walk_forward");
        }
    }
}
