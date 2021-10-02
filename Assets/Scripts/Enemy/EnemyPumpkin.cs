using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPumpkin : EnemyBase
{

    [SerializeField] private int speeds, healths, attackDmg, attackDist, attackT;
    void Start()
    {
        StartSettings(speed: speeds, health: healths, attackDamage: attackDmg, attackDistance: attackDist, attackTimer: attackT);
    }

    void Update()
    {
        CheckConditions();
    }
    protected override void Attack()
    {
        base.Attack();
        Death();
    }
}
