using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters;
using UnityEngine;

public class IceBossController : EnemyAI
{
    [SerializeField] private WeaponProjectile projectile;
    [SerializeField] private Transform projectilePos;
    [SerializeField] private float projectileDelay;

    [SerializeField] private string attack1Trigger;
    [SerializeField] private string attack2Trigger;

    private int attack1Hash;
    private int attack2Hash;

    private Transform player;

    void Start()
    {
        attack1Hash = Animator.StringToHash(attack1Trigger);
        attack2Hash = Animator.StringToHash(attack2Trigger);

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
        // var proj = Instantiate(projectile, projectilePos.position, Quaternion.identity);
        // proj.FireToPosition(GameManager.Instance.PlayerController.transform.position, .4f);

        attackTime = Time.time;
    }

    private void DoProj()
    {
        
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
        GameManager.Instance.killedbosses++;
        if (GameManager.Instance.killedbosses >= GameManager.Instance.bossestokill)
        {
            GameController.instance.GameFinishPoint.SetActive(true);
            GameController.instance.GameFinishPointblocker.SetActive(false);
        }
    }
    public void endsound()
    {
        AudioManager.Instance.CrossFadeMusic("Snowland");
    }
    public void attacksound()
    {
        AudioManager.Instance.CrossFadeMusic("Snowland_Boss");
    }
}
