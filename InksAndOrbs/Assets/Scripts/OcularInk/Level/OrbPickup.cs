using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class OrbPickup : PickupObject
{
    public UnityEvent onPickedUp;

    private void Start()
    {
        gameObject.layer = 0;
        GameManager.Instance.GameController.FocusOnOrb(transform);
        
        Invoke(nameof(Activate), 4f);
    }

    private void Activate()
    {
        gameObject.layer = 7;
    }

    public override void OnHit(Collider other)
    {
        base.OnHit(other);
        onPickedUp?.Invoke();
        transform.DOScale(0f, 0.3f).onComplete =()=>Destroy(gameObject);;
    }
}
