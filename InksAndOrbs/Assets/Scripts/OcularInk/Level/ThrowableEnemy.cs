using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableEnemy : BrushableObject
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private MeshFilter meshFilter;
    
    public void Assign(Mesh mesh, Material material)
    {
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.material = material;
        
        rb.AddForceAtPosition(transform.position, new Vector3(Random.Range(-1f, 1f), 10f, Random.Range(-1f, 1f)) * 70f);
    }
    
    protected override void OnHit(Collision collision)
    {
        base.OnHit(collision);
        if (collision.collider.name.Contains("Rock"))
        {
            AudioManager.Instance.PlayAtPosition("Rock", transform.position);
        }
    }
}
