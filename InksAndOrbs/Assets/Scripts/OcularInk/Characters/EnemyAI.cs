using System;
using OcularInk.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace OcularInk.Characters
{
    public abstract class EnemyAI : MonoBehaviour, ICharacter
    {
        [field: SerializeField] protected Animator Animator { get; set; }
        [field: SerializeField] protected AIState State { get; set; }

        [SerializeField] private float initialHealth = 10f;

        [SerializeField] protected GameObject deathEffect;
        [SerializeField] protected Vector3 healthNoticeOffset;

        [Header("Attack Settings")]
        [SerializeField]
        protected float attackDistance = 10f;

        [SerializeField] protected float attackInterval = 2f;
        [SerializeField] protected float attackIntervalRage = 1.5f;
        [SerializeField] protected float attackDamage = 1f;
        [SerializeField] protected float attackRewindDuration = 0.75f;
        [SerializeField] protected float detectRange = 16f;

        [SerializeField] protected UnityEvent onActivate;
        [SerializeField] protected UnityEvent onDeath;
        [SerializeField] protected float particleSize = 1f;

        [SerializeField] protected MeshFilter meshFilter;
        [SerializeField] protected SkinnedMeshRenderer meshRenderer;

        [SerializeField] protected bool canObjectify;
        [SerializeField] protected bool isBoss;
        [SerializeField] protected bool hasOrb;
        private float AttackInterval => Health <= (initialHealth * 0.5f) ? attackIntervalRage : attackInterval;

        private float stateChangeTime;
        protected float statusTickTime;

        protected float Health { get; set; }
        protected StatusEffect ActiveEffect { get; private set; }
        protected float attackTime;

        protected GameObject statusVfx;

        protected virtual void Awake()
        {
            Health = initialHealth;
            SetState(AIState.Idle);
        }

        public virtual void TakeDamage(float damage)
        {
            //damage = 51; //New
            Animator.SetTrigger("Hurt");
            Health -= damage;

            var textPos = transform.position + healthNoticeOffset;

            // Show health text, only if larger than 0
            if (damage > 0)
            {
                GameManager.Instance.GameController.ShowHealthText($"-{damage}", textPos);
            }

            if (Health <= 0)
            {
                SetState(AIState.Disabled);
                Die();
            }
        }

        public virtual void AddStatusEffect(StatusEffect effect)
        {
            Debug.Log("Add status effect: " + effect);

            ActiveEffect = effect;

            if (effect == StatusEffect.None)
                return;

            GameObject effectPrefab = null;

            switch (effect)
            {
                case StatusEffect.Freeze:
                    effectPrefab = AssetManager.Instance.iceStatusEffect;
                    break;
                case StatusEffect.Burn:
                    effectPrefab = AssetManager.Instance.burnStatusEffect;
                    break;
                case StatusEffect.Poison:
                    effectPrefab = AssetManager.Instance.poisonStatusEffect;
                    break;
                case StatusEffect.Objectify:
                    if (canObjectify)
                        Objectify();
                    break;
            }

            if (effectPrefab == null)
                return;

            statusVfx = Instantiate(effectPrefab, transform);
            statusVfx.transform.localPosition = Vector3.up * particleSize;
            // statusVfx.transform.localScale = Vector3.one;
        }

        public virtual void Objectify()
        {
            var throwablePrefab = Resources.Load<ThrowableEnemy>("ThrowableEnemy");
            var enemyInstance = Instantiate(throwablePrefab, transform.position, Quaternion.identity);
            enemyInstance.Assign(meshRenderer.sharedMesh, meshRenderer.material);

            GameManager.Instance.GameController.IncreaseScore((int)initialHealth);

            Destroy(gameObject);
        }

        protected void TickStatusEffect()
        {
            if (Time.time - statusTickTime < 1f)
                return;

            if (ActiveEffect == StatusEffect.None)
                return;

            statusTickTime = Time.time;

            switch (ActiveEffect)
            {
                case StatusEffect.Burn:
                    TakeDamage(3f);
                    break;

                case StatusEffect.Poison:
                    TakeDamage(2f);
                    break;
            }
        }

        protected virtual void DisableStatusEffect()
        {
            ActiveEffect = StatusEffect.None;
        }

        protected abstract void Attack();

        protected virtual void SetState(AIState newState)
        {
            if (State == newState)
                return;

            stateChangeTime = Time.time;

            State = newState;

            switch (newState)
            {
                case AIState.Idle:
                    Animator.SetTrigger("Idle");
                    Animator.SetBool("Chase", false);
                    break;
                case AIState.Patrol:
                    break;
                case AIState.Chase:
                    Animator.SetBool("Chase", true);
                    break;
                case AIState.Attack:
                    onActivate?.Invoke();
                    break;
            }
        }

        protected virtual void Die()
        {
            try
            {
                if (Random.value < 0.63f)
                {
                    var moneyPickup = Resources.Load<GameObject>("MoneyPickup");
                    var money = Instantiate(moneyPickup, transform.position, Quaternion.identity);
                }

                if (isBoss && !hasOrb)
                {
                    UIManager.Instance.GetCanvas<GameCanvas>().ShowBossDefeated();
                }

                deathEffect.SetActive(true);
                deathEffect.transform.parent = null;

                onDeath?.Invoke();

                GameManager.Instance.GameController.IncreaseScore((int)initialHealth);
                GameManager.Instance.killedbosses++;
                if (GameManager.Instance.killedbosses >= GameManager.Instance.bossestokill)
                {
                    GameController.instance.GameFinishPoint.SetActive(true);
                    GameController.instance.GameFinishPointblocker.SetActive(false);
                }
                Destroy(gameObject);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        protected void MaintainAttack()
        {
            if (EnemyController.ForceDisable)
                return;

            if (Time.time - stateChangeTime < 0.5f)
                return;

            if (Time.time - attackTime > AttackInterval)
            {
                Attack();
            }
        }

        private void OnValidate()
        {
            Animator = GetComponent<Animator>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "Enemy");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectRange);
        }
    }
}

public enum StatusEffect
{
    None,
    Freeze,
    Burn,
    Poison,
    Objectify
}