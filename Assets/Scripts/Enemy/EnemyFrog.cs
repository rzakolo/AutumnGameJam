using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : EnemyBase
{
    Vector3 lookDirection;
    Vector3 nextPosition;
    bool jump;
    float jumpTimer;
    [SerializeField] float _jumpTimer;
    [SerializeField] ParticleSystem frogAttackParticle;
    float animationSpeedInSeconds = 0.2f;

    [SerializeField] private int speeds, healths, attackDmg, attackDist, attackT;


    private void Awake()
    {
        gameManager = GetComponent<GameManager>();

    }
    void Start()
    {
        jumpTimer = _jumpTimer;
       StartSettings(speed: speeds, health: healths, attackDamage: attackDmg, attackDistance: attackDist, attackTimer: attackT);
        navMeshAgent.updatePosition = false;
    }
    private void Update()
    {
        CheckConditions();
        if (jumpTimer < 0)
        {
            jump = false;
            CheckDistance();
            jumpTimer = _jumpTimer;
        }
        else
            jumpTimer -= Time.deltaTime;
        if (jump)
        {
            transform.position = Vector3.Slerp(transform.position, nextPosition, animationSpeedInSeconds);
        }

    }
    protected override void Attack()
    {
        frogAttackParticle.transform.position = transform.position;
        Instantiate(frogAttackParticle);
        base.Attack();
    }
    public void UpdateNextPosition()
    {
        lookDirection = navMeshAgent.nextPosition - transform.position;
        navMeshAgent.nextPosition += lookDirection;
        nextPosition = navMeshAgent.nextPosition;
        jump = true;
    }
    public void CheckDistance()
    {
        //target = gameManager.GetPlayerPos();
        //if (target != null)
        //    navMeshAgent.SetDestination(target.position);
        if (navMeshAgent.remainingDistance > attackDistance)
        {
            animator.SetTrigger("Jump");
            UpdateNextPosition();
        }
    }
    private void OnAnimatorMove()
    {

    }
}
