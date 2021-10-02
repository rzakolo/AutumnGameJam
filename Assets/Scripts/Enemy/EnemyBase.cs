using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    protected Transform target;
    protected NavMeshAgent navMeshAgent;
    private GameObject[] obstacles;
    protected int maxHealth;
    protected int health;
    protected float speed;
    protected int attackDamage;
    protected float attackDistance;
    float attackTimer;
    float _attackTimer;
    protected GameManager gameManager;
    protected Animator animator;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] GameObject lootPrefab;
    protected int dropChanse = 20;

    internal Transform GetTarget()
    {
        return target;
    }

    protected virtual void StartSettings(int speed, int health, int attackDamage, float attackDistance, float attackTimer)
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = speed;
        this.speed = speed;
        this.health = health;
        this.maxHealth = health;
        this.attackDamage = attackDamage;
        this.attackDistance = attackDistance;
        navMeshAgent.stoppingDistance = this.attackDistance;
        this.attackTimer = attackTimer;
        _attackTimer = attackTimer;
        target = gameManager.GetPumpkinPos();
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }
    /// <summary>
    /// Проверяет соответсвует условиям для атаки или нет
    /// </summary>
    protected void CheckConditions()
    {
        int weight = gameManager.player.health > 30 ? 0 : 10;
        float distanceToPlayer = Vector3.Distance(transform.position, gameManager.GetPlayerPos().position);
        float distanceToPumpkin = Vector3.Distance(transform.position, gameManager.GetPumpkinPos().position);
        if (distanceToPlayer > distanceToPumpkin + weight)
        {
            target = gameManager.GetPumpkinPos();
        }
        else
        {
            target = gameManager.GetPlayerPos();
        }
        ObstacleInterferes();
        if (target != null)
            navMeshAgent.SetDestination(target.position);
        float distance = Vector3.Distance(transform.position, target.position);
        distance -= gameManager.pumpkin.transform.localScale.x / 2;

        if (distance - 0.2f <= attackDistance && attackTimer < 0)
        {
            FaceTarget(target.position);
            Attack();
            attackTimer = _attackTimer;
        }
        attackTimer -= Time.deltaTime;
    }
    private void ObstacleInterferes()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obstacle in obstacles)
        {
            float distance = Vector3.Distance(transform.position, obstacle.transform.position);
            if (distance < attackDistance)
            {
                target = obstacle.transform;
            }
        }
    }

    protected virtual void Attack()
    {
        if (target.CompareTag("Pumpkin"))
            gameManager.pumpkin.health -= attackDamage;
        if (target.CompareTag("Player"))
            gameManager.player.health -= attackDamage;
        if (target.CompareTag("Obstacle"))
            target.GetComponent<Obstacle>().health -= attackDamage;
    }
    /// <summary>
    /// Останавливать ли противника когда он атакует.
    /// </summary>
    protected void StopWhenAttack()
    {
        navMeshAgent.isStopped = true;
    }
    protected void StopAnimation()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.updateRotation = true;
        if (animator != null)
            animator.SetTrigger("Attack");
    }
    protected void StopRotationWhileAttack()
    {
        if (navMeshAgent.remainingDistance <= attackDistance)
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.isStopped = true;
        }
    }
    protected void DropLoot()
    {
        int random = UnityEngine.Random.Range(0, 100);
        if (random <= dropChanse)
        {
            lootPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(lootPrefab);
        }
        gameManager.money += 23;
    }
    public void TakeDamage(int damage, Vector3 direction)
    {
        health -= damage;
        UIDamage.Create(transform.position + Vector3.up, damage.ToString());
        navMeshAgent.Move(direction);
        if (health <= 0)
        {
            Death();
        }
    }

    protected void Death()
    {
        DropLoot();
        if (deathParticle != null)
        {
            deathParticle.transform.position = transform.position + Vector3.up;
            deathParticle.transform.rotation = Quaternion.identity;
            Instantiate(deathParticle);
        }
        Destroy(gameObject);
    }
    protected void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5);
    }
}
