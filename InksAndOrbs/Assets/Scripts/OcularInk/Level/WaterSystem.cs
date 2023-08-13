using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    [SerializeField] private GameObject splash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var sp = Instantiate(splash, other.transform.position + Vector3.down, Quaternion.identity);
        sp.transform.localScale = other.bounds.size * 2f;
    }
}
