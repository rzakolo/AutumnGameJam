using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhostRange : EnemyBase
{
    [SerializeField] Projectile projectile;

    [SerializeField] private int speeds, healths, attackDmg, attackDist, attackT;

    void Awake()
    {
        StartSettings(speed: speeds, health: healths, attackDamage: attackDmg, attackDistance: attackDist, attackTimer: attackT);
    }
    private void Update()
    {
        CheckConditions();
    }
    protected override void Attack()
    {
        projectile.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward;
        projectile.transform.rotation = transform.rotation;
        Instantiate(projectile).SetDamage(attackDamage);
    }
}
