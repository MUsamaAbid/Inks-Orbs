using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushableRock : BrushableObject
{
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
        base.OnHit(collision);
        if (collision.collider.name.Contains("Rock"))
        {
            AudioManager.Instance.PlayAtPosition("Rock", transform.position);
        }
    }
}
