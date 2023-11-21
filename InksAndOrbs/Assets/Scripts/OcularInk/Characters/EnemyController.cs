using System;
using OcularInk.Characters.Protagonist;
using UnityEngine;
using UnityEngine.AI;

namespace OcularInk.Characters
{
    public class EnemyController : EnemyAI
    {
        [Header("General Settings")]
        
        [SerializeField] private float movementSpeed = 0.5f;
        [SerializeField] private float detectDistance = 15f;
        
        [SerializeField] private WeaponProjectile projectilePrefab;

        [Header("Component References")]
        
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;

        [SerializeField] private bool isMelee;

        public Transform target;
        private float health;
        private float damagedTime;

        public static bool ForceDisable;

        private void Start()
        {
            target = GameManager.Instance.PlayerController.transform;
        }

        private void Update()
        {
            if (ForceDisable)
                return;
            
            TickStatusEffect();
            
            switch (State)
            {
                case AIState.Idle:
                    ScanTarget();
                    break;
                case AIState.Patrol:
                    break;
                case AIState.Chase:
                    Chase();
                    MaintainAttack();
                    PreventHitting();
                    break;
                case AIState.Attack:
                    break;
            }
        }

        protected override void Attack()
        {
            if (navMeshAgent.isStopped)
                return;

            if (Vector3.Distance(target.position, transform.position) > detectDistance * 1f)
                return;
            
            attackTime = Time.time;

            if (isMelee)
            {
                animator.SetTrigger("Attack");
                return;
            }

            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.Fire(transform.forward);
        }

        private void ScanTarget()
        {
            // If the target is within the detect distance
            if (Vector3.Distance(target.position, transform.position) < detectDistance * 2f)
            {
                SetState(AIState.Chase);
            }
        }
        
        private void Chase()
        {
            var currentPos = transform.position;
            
            Quaternion targetRot = Quaternion.LookRotation(target.position - currentPos, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 8f);
            
            // transform.position =
            //     Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5f * movementSpeed);

            
            navMeshAgent.SetDestination(target.position);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                if (collision.relativeVelocity.magnitude < 8f)
                    return;

                TakeDamage(Mathf.Round(3 + collision.relativeVelocity.magnitude));//Old
                //TakeDamage(Mathf.Round(2 * collision.relativeVelocity.magnitude));//New

                Debug.Log("Damage given: " + Mathf.Round(3 + collision.relativeVelocity.magnitude).ToString());
            }
        }

        public override void TakeDamage(float damage)
        {
            damagedTime = Time.time;
            base.TakeDamage(damage);
            
            AudioManager.Instance.PlayAudio("EnemyHurt");
        }

        protected override void Die()
        {
            base.Die();
            AudioManager.Instance.PlayAudio("EnemyDeath");
        }

        public override void AddStatusEffect(StatusEffect effect)
        {
            base.AddStatusEffect(effect);

            switch (effect)
            {
                case StatusEffect.Freeze:
                    navMeshAgent.speed = 0.3f;
                    animator.speed = 0.1f;
                    break;
                case StatusEffect.Burn:
                    break;
                case StatusEffect.Poison:
                    break;
            }
        }

        protected override void DisableStatusEffect()
        {
            switch (ActiveEffect)
            {
                case StatusEffect.Freeze:
                    navMeshAgent.speed = 2f;
                    animator.speed = 1f;
                    break;
                case StatusEffect.Burn:
                    break;
                case StatusEffect.Poison:
                    break;
            }
            
            base.DisableStatusEffect();
        }

        private void PreventHitting()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
            {
                if (hit.collider.CompareTag("BossGate"))
                {
                    navMeshAgent.isStopped = true;
                }
                else
                {
                    navMeshAgent.isStopped = false;
                }
            }
        }
    }

    public enum AIState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Disabled
    }
}