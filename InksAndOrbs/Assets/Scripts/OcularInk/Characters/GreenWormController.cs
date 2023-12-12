using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OcularInk.Characters;
using UnityEngine;

public class GreenWormController : EnemyAI
{
    private int attack1Hash;
    private int attack2Hash;

    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.PlayerController.transform;
        
        attack1Hash = Animator.StringToHash("Attack1");
        attack2Hash = Animator.StringToHash("Attack2");
    }

    private void Update()
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
        lookDirection.y = this.transform.position.y;
        Quaternion targetRot = Quaternion.LookRotation(lookDirection - transform.position, Vector3.up);

        transform.DORotateQuaternion(targetRot, 0.5f);       
        Animator.SetTrigger(attack1Hash);
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
        AudioManager.Instance.CrossFadeMusic("Desert");
    }
    public void attacksound()
    {
        AudioManager.Instance.CrossFadeMusic("Desert_Boss");
    }
}
