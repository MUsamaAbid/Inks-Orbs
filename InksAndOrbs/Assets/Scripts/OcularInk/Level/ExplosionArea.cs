using System;
using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters;
using OcularInk.Interfaces;
using UnityEngine;

public class ExplosionArea : MonoBehaviour
{
    [SerializeField] private Collider collider;
    [SerializeField] private StatusEffect statusEffect;
    
    private float damage;
    
    public void Activate(float _damage)
    {
        collider.enabled = true;
        damage = _damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var character = other.GetComponent<ICharacter>();
            character.TakeDamage(Mathf.RoundToInt(damage));
            
            if (statusEffect != StatusEffect.None)
            {
                Debug.Log("Add status effect: " + statusEffect);
                character.AddStatusEffect(statusEffect);
            }
            Destroy(gameObject, Time.deltaTime * 3f);
        }
    }
}
