using System;
using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters;
using UnityEngine;
using UnityEngine.AI;

public class BombEnemy : EnemyAI
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject explosion;
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private bool willExplode;
    [SerializeField] private Vector3 explosionRotation;

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.Instance.PlayerController.transform;
    }

    private void Update()
    {
        switch (State)
        {
            case AIState.Idle:
                ScanTarget();
                break;
            case AIState.Patrol:
                break;
            case AIState.Chase:
                Chase();
                ScanAttackRange();
                break;
            case AIState.Attack:
                ScanTargetOnly();
                MaintainAttack();
                break;
            case AIState.Disabled:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ScanTarget()
    {
        // If the target is within the detect distance
        if (Vector3.Distance(target.position, transform.position) < detectRange)
        {
            SetState(AIState.Chase);
        }
    }

    private void ScanTargetOnly()
    {
        var dist = Vector3.Distance(target.position, transform.position);
        // If the target is within the detect distance
        if (dist > attackDistance && dist < detectRange)
        {
            navMeshAgent.isStopped = false;
            SetState(AIState.Chase);
        }
    }

    private void ScanAttackRange()
    {
        // If the target is within the detect distance
        if (Vector3.Distance(target.position, transform.position) < attackDistance)
        {
            SetState(AIState.Attack);
        }
    }

    private void Chase()
    {
        var currentPos = transform.position;

        Quaternion targetRot = Quaternion.LookRotation(target.position - currentPos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 8f);


        navMeshAgent.SetDestination(target.position);
    }

    protected override void Attack()
    {
        navMeshAgent.isStopped = true;
        Animator.SetTrigger("Attack");
        attackTime = Time.time;
    }

    private void Explode()
    {
        if (!willExplode)
        {
            willExplode = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        var exp = Instantiate(explosion, transform.position, Quaternion.identity);
        exp.transform.eulerAngles = explosion.transform.eulerAngles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (collision.relativeVelocity.magnitude < 8f)
                return;

            TakeDamage(Mathf.Round(3 + collision.relativeVelocity.magnitude));
        }
    }

    public override void TakeDamage(float damage)
    {
        AudioManager.Instance.PlayAudio("EnemyHurt");
        base.TakeDamage(damage);
    }
}