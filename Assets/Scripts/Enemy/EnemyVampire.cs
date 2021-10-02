using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVampire : EnemyBase
{
    [SerializeField] private int speeds, healths, attackDmg, attackDist, attackT;
    void Start()
    {
        StartSettings(speed: speeds, health: healths, attackDamage: attackDmg, attackDistance: attackDist, attackTimer: attackT);
        //StartSettings(speed: 2, health: 30, attackDamage: 4, attackDistance: 2, attackTimer: 2);
    }
    private void Update()
    {
        CheckConditions();
    }
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        base.Attack();
    }
}
