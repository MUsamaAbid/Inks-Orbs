using System;
using UnityEngine;

namespace OcularInk.Characters
{
    public class TrapEnemy : EnemyAI
    {
        [SerializeField] protected float attackDelay;
        [SerializeField] protected GameObject trapFx;
        [SerializeField] protected Transform target;
        [SerializeField] protected bool isMelee;

        private void Start()
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
                    break;
                case AIState.Attack:
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
                SetState(AIState.Attack);
            }
        }

        private void AttackFx()
        {
            Instantiate(trapFx, transform.position, Quaternion.identity);
        }

        protected override void Attack()
        {
            Animator.SetTrigger("Attack");
            attackTime = Time.time;

            if (!isMelee)
            {
                Invoke(nameof(AttackFx), attackDelay);
            }
        }


    }
}
