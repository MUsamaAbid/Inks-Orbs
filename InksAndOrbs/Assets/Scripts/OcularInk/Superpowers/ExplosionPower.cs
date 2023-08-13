using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPower : Superpower
{
    [SerializeField] private ExplosionArea explosionArea;
    [SerializeField] private float damage = 50f;

    public override void Activate()
    {
        explosionArea.Activate(damage);
    }

    public override void Disable()
    {
        
    }
}
