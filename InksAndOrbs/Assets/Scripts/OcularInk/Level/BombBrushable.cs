using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBrushable : BrushableObject
{
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private ExplosionArea explosionArea;
    [SerializeField] private float damageMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void OnHit(Collision collision)
    {
        if (Time.timeSinceLevelLoad < 3f)
        {
            return;
        }

        if (collision.relativeVelocity.magnitude > 10f)
        {
            var damage = collision.relativeVelocity.magnitude;
            Explode(damage * damageMultiplier);
        }
    }

    private void Explode(float damage)
    {
        explosionParticle.SetActive(true);
        explosionParticle.transform.SetParent(null);
        
        explosionArea.Activate(damage);
        explosionArea.transform.SetParent(null);
        Destroy(gameObject);
    }
}