using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyHitArea : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private HitAreaType hitAreaType;
    [SerializeField] private float damage;
    [SerializeField] private float hitInterval;
    [SerializeField] private bool autoDestroy;
    
    private float lastAttackTime;
    private bool isPlayerOnArea;

    void Update()
    {
        TickAttack();
    }

    private void TickAttack()
    {
        if (!collider.enabled)
        {
            isPlayerOnArea = false;
            return;
        }
        
        if (hitAreaType != HitAreaType.Constant)
            return;
        
        if (!isPlayerOnArea)
            return;
        
        if (Time.time - lastAttackTime > hitInterval)
        {
            GameManager.Instance.PlayerController.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnArea = true;
            if (hitAreaType == HitAreaType.Single)
            {
                GameManager.Instance.PlayerController.TakeDamage(damage);
            }
        }
    }

    private void OnDisable()
    {
        isPlayerOnArea = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnArea = false;
        }
    }

    public enum HitAreaType
    {
        Single, Constant
    }
}
