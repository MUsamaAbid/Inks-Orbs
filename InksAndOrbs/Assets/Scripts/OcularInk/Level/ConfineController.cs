using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConfineController : MonoBehaviour
{
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private BoxCollider[] confineAreas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ExecuteInEditMode]
    void Update()
    {
        OnValidate();
        OnDrawGizmos();        
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;
        
        foreach (BoxCollider collider in confineAreas)
        {
            var color = Color.yellow;
            color.a = 100;
            Gizmos.color = color;
            Gizmos.matrix = collider.transform.localToWorldMatrix;
        
            Gizmos.DrawCube(collider.center, collider.size * 1f);        
        }
    }

    private void OnValidate()
    {
        confineAreas = GetComponentsInChildren<BoxCollider>();
    }
}
