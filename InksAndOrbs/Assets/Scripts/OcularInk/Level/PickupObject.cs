using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private GameObject hitFx;

    private void Awake()
    {
        gameObject.layer = 0;
        Invoke(nameof(ActivateLayer), Random.Range(0.8f, 1.2f));
    }

    private void ActivateLayer()
    {
        gameObject.layer = 7;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnHit(other);
        }
    }

    public virtual void OnHit(Collider other)
    {
        if (hitFx != null)
        {
            hitFx.SetActive(true);
            hitFx.transform.parent = null;
        }
    }
}
