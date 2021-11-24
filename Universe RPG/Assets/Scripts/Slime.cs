using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour, IEnemy
{

    public LayerMask aggroLayerMask;
    public float currentHealth;
    public float maxHealth;

    private Player player;
    private NavMeshAgent navAgent;
    private CharacterStats characterStats;
    private Collider[] withInAggroCollider;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        characterStats = new CharacterStats(6, 10, 2);
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        withInAggroCollider = Physics.OverlapSphere(transform.position, 10, aggroLayerMask);
        if(withInAggroCollider.Length > 0)
        {
            ChasePlayer(withInAggroCollider[0].GetComponent<Player>());
        }
    }

    public void PerformAttack()
    {
        player.TakeDamage(5);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
            Die();
    }

    void ChasePlayer(Player player)
    {
        navAgent.SetDestination(player.transform.position);
        this.player = player;
        if(navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if(!IsInvoking("PerformAttack"))
                InvokeRepeating("PerformAttack", .5f, 2f);
        }
        else
        {
            CancelInvoke("PerformAttack");
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
