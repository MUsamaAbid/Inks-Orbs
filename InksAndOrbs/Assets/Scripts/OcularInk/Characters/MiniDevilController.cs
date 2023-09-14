using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OcularInk.Characters;
using UnityEngine;

public class MiniDevilController : EnemyAI
{
    [SerializeField] private WeaponProjectile projectile;

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
                MaintainAttack();
                break;
            case AIState.Attack:
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

    private void Attack1()
    {
        var lookDirection = GameManager.Instance.PlayerController.transform.position;
        lookDirection.y = transform.position.y;
        var targetRot = Quaternion.LookRotation(lookDirection - transform.position, Vector3.up);

        transform.DORotateQuaternion(targetRot, 0.5f).onComplete = () =>
        {
            Animator.SetTrigger(attack1Hash);
            var projInstance = Instantiate(projectile, transform.position + (Vector3.up * 3f), Quaternion.identity);
            projInstance.Fire(transform.forward);
        };
        
        AudioManager.Instance.PlayAudio("BossGrunt");
    }

    private void Attack2()
    {
        Animator.SetTrigger(attack2Hash);
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
        if(GameManager.GameData.CurrentLevel == 1)
            GameManager.Instance.GameController.SpawnFinishPoint(this.transform.position);
        base.Die();
    }
}