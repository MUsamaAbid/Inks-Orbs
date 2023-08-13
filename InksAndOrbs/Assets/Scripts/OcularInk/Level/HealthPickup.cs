using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HealthPickup : PickupObject
{
    [SerializeField] private float healAmount = 5f;
    public override void OnHit(Collider other)
    {
        base.OnHit(other);

        GameManager.Instance.PlayerController.AddHealth(healAmount);
        AudioManager.Instance.PlayAudio("HealthPickup");
        transform.DOScale(0f, 0.25f).onComplete = () =>
            Destroy(gameObject);
    }
}