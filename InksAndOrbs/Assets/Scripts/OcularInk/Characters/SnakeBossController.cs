using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OcularInk.Characters;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnakeBossController : EnemyAI
{
    [SerializeField] private WeaponProjectile projectile;
    [SerializeField] private Transform projectilePos;

    private int attack1Hash;
    private int attack2Hash;

    private Transform player;

    void Start()
    {
        attack1Hash = Animator.StringToHash("Attack1");
        attack2Hash = Animator.StringToHash("Attack2");

        SetState(AIState.Idle);

        player = GameManager.Instance.PlayerController.transform;
    }

    void Update()
    {
        switch (State)
        {
            case AIState.Idle:
                ScanPlayer();
                break;
            case AIState.Patrol:
                break;
            case AIState.Chase:
                LookPlayer();
                MaintainAttack();
                break;
            case AIState.Attack:
                LookPlayer();
                MaintainAttack();
                break;
        }
    }

    private void ScanPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < detectRange)
        {
            SetState(AIState.Attack);
        }
    }

    private void LookPlayer()
    {
        var lookDirection = GameManager.Instance.PlayerController.transform.position;
        lookDirection.y = transform.position.y;
        var targetRot = Quaternion.LookRotation(lookDirection - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 5f);
    }

    protected override void Attack()
    {
        attackTime = Time.time;
        if (Random.value < 0.5f)
        {
            Attack1();
        }
        else
        {
            Attack2();
        }
    }

    private void Attack2()
    {
        Animator.SetTrigger(attack2Hash);
        AudioManager.Instance.PlayAudio("BossGrunt");

        attackTime = Time.time;
    }

    private void Attack1()
    {
        Animator.SetTrigger(attack1Hash);
        var proj = Instantiate(projectile, projectilePos.position, Quaternion.identity);
        Debug.Log(proj.gameObject.name);
        proj.FireToPosition(GameManager.Instance.PlayerController.transform.position, .4f);

        attackTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (collision.relativeVelocity.magnitude < 8f)
                return;

            TakeDamage(Mathf.Round(3 + collision.relativeVelocity.magnitude));
            AudioManager.Instance.PlayAudio("BossGrunt");
        }
    }

    protected override void Die()
    {
        AudioManager.Instance.PlayAudio("BossDying");
        base.Die();
    }
}