using System;
using DG.Tweening;
using OcularInk.Interfaces;
using UnityEngine;

namespace OcularInk.Characters
{
    public class WeaponProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float damage = 5f;
        [SerializeField] private GameObject hitParticle;
        [SerializeField] private Collider collider;
        [SerializeField] private bool autoFire;

        [SerializeField] private bool isAlly;

        private Vector3 targetVelocity;

        private void Start()
        {
            if (autoFire)
            {
                Fire(transform.forward);
            }
        }

        public void Fire(Vector3 forward)
        {
            targetVelocity = forward * speed;
        }

        public void FireToPosition(Vector3 pos, float time)
        {
            transform.DOMove(pos, time).SetEase(Ease.Linear).onComplete = () =>
            {
                Instantiate(hitParticle, transform.position, Quaternion.identity);
                Destroy(collider);
                Disable();            };
        }

        private void Disable()
        {
            targetVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
            collider.enabled = false;
        }

        private void FixedUpdate()
        {
            if (targetVelocity.magnitude == 0f)
                return;

            rigidbody.velocity = targetVelocity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Environment"))
            {
                Debug.Log("Environment");
                
                Instantiate(hitParticle, transform.position, Quaternion.identity);
                Destroy(collider);
                Disable();
            }
            
            if (!isAlly)
            {
                if (other.CompareTag("Player"))
                {
                    Disable();
                    Instantiate(hitParticle, transform.position, Quaternion.identity);
                
                    GameManager.Instance.PlayerController.TakeDamage(damage);
                }
            }
            else
            {
                if (other.CompareTag("Enemy"))
                {
                    Disable();
                    Instantiate(hitParticle, transform.position, Quaternion.identity);
                    other.GetComponent<ICharacter>().TakeDamage(damage);
                }
            }
        }
    }
}
