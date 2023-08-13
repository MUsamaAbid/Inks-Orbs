using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChestObject : MonoBehaviour
{
    [SerializeField] private Transform chestLid;
    [SerializeField] private GameObject content;
    [SerializeField] private List<MoneyPickup> moneyPickups;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private bool autoOpen;
    
    private bool isUnlocked;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var moneyPickup in moneyPickups)
        {
            moneyPickup.gameObject.layer = 0;
        }

        if (autoOpen)
        {
            Open();
        }
        else
        {
            content.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Open()
    {
        if (isUnlocked)
            return;

        isUnlocked = true;
        chestLid.DOLocalRotate(new Vector3(0, 0, -120f), 1f).SetEase(Ease.OutBack);
        content.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (collision.relativeVelocity.magnitude < 8f)
                return;
            Open();
        }
    }
}
