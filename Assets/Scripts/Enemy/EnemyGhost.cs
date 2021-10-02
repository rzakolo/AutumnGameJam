using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : EnemyBase
{
    [SerializeField] private int speeds, healths, attackDmg, attackDist, attackT;
    void Start()
    {
        StartSettings(speed: speeds, health: healths, attackDamage: attackDmg, attackDistance: attackDist, attackTimer: attackT);
    }
    private void Update()
    {
        CheckConditions();
    }
}
