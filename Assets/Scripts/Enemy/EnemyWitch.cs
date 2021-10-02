using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWitch : EnemyBase
{
    [SerializeField] EnemyBase frog;
    SpawnManager spawnManager;
    [SerializeField] private int speeds, healths, attackDmg, attackDist, attackT;
    void Start()
    {
        StartSettings(speed: speeds, health: healths, attackDamage: attackDmg, attackDistance: attackDist, attackTimer: attackT);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    private void Update()
    {
        CheckConditions();
    }
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        frog.transform.position = transform.position + transform.right;
        spawnManager.enemyList.Add(Instantiate(frog));
        frog.transform.position = transform.position + transform.up;
        spawnManager.enemyList.Add(Instantiate(frog));
        frog.transform.position = transform.position + transform.forward;
        spawnManager.enemyList.Add(Instantiate(frog));
    }
}
