using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizard : EnemyBase
{
    Vector3 pumkinPos;
    Vector3 playerPos;
    [SerializeField] Projectile projectile;

    [SerializeField] private int speeds, healths, attackDmg, attackDist, attackT;

    private void Start()
    {
      StartSettings(speed: speeds, health: healths, attackDamage: attackDmg, attackDistance: attackDist, attackTimer: attackT);
        pumkinPos = gameManager.GetPumpkinPos().position;
    }
    private void Update()
    {
        CheckConditions();
    }
    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        projectile.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward;
        projectile.transform.rotation = transform.rotation;
        Instantiate(projectile).SetDamage(attackDamage);
    }

}
