using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class AllyController : MonoBehaviour
{
    [Header("Asset References")] [SerializeField]
    protected Transform skin;

    [SerializeField] protected Animator animator;
    [SerializeField] protected AllyWeapon weapon;
    [SerializeField] protected NavMeshAgent navMeshAgent;

    [Header("General Settings")] [SerializeField]
    protected float attackRange;

    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackInterval;
    [SerializeField] protected float enemyScanRadius;

    [SerializeField] private Vector3 velocity;
    private Vector3 oldPos;

    private Transform targetEnemy;

    // Animator fields
    private int hitHash;
    private int attackHash;
    private int deathHash;

    // Timing fields
    private float lastAttackTime;

    // Flags
    private bool CanAttackAgain => Time.time - lastAttackTime > attackInterval;

    protected virtual void Awake()
    {
        hitHash = Animator.StringToHash("Hit");
        attackHash = Animator.StringToHash("Attack");
        deathHash = Animator.StringToHash("Death");
    }

    private void Update()
    {
        // velocity = (transform.position - oldPos) / Time.deltaTime; 
        //
        // oldPos = transform.position;

        if (!navMeshAgent.isStopped)
        {
            skin.Rotate(transform.right * 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }

        ScanEnemies();
        HandleAttack();
    }

    public void SetDestination(Vector3 pos)
    {
        navMeshAgent.SetDestination(pos);
    }

    public void SetTarget(Transform target)
    {
        targetEnemy = target;
    }

    protected virtual void ScanEnemies()
    {
        var enemyMask = LayerMask.GetMask("Enemy");
        var overlap = Physics.OverlapSphere(transform.position, enemyScanRadius, enemyMask);

        if (overlap.Length > 0)
        {
            targetEnemy = overlap[0].transform;
        }
    }

    protected virtual void Shoot()
    {
        weapon.Fire();
    }

    protected virtual void HandleAttack()
    {
        if (targetEnemy == null)
        {
            navMeshAgent.isStopped = false;
            return;
        }

        if (Vector3.Distance(transform.position, targetEnemy.position) > attackRange)
        {
            navMeshAgent.isStopped = false;
            return;
        }

        navMeshAgent.isStopped = true;
        Attack();
    }

    protected virtual void Attack()
    {
        if (!CanAttackAgain)
            return;

        var targetRot = Quaternion.LookRotation(targetEnemy.position - transform.position, Vector3.up);

        skin.DOKill();
        skin.DORotate(Vector3.zero, 1f);
        transform.DOKill();
        transform.DORotateQuaternion(targetRot, 0.2f).onComplete = () =>
        {
            weapon.Fire();
            weapon.SetTarget(targetEnemy.position);
            animator.SetTrigger(attackHash);
        };

        lastAttackTime = Time.time;
    }
}