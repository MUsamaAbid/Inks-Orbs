using System;
using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters.Protagonist;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BrushableObject : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] private float forceMultiplier;

    private float _brushTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (Time.time - _brushTime < 0.25f)
        {
            return;
        }
        
        if (other.CompareTag("BrushArea"))
        {
            rb.AddForce(other.transform.forward * (1 / rb.mass) * forceMultiplier * (BrushController.dist * 0.15f), ForceMode.Impulse);
            _brushTime = Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision);
    }

    protected virtual void OnHit(Collision collision) {}

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
    }
}
