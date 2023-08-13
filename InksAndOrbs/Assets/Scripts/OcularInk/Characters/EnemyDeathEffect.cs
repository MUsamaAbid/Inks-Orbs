using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    [SerializeField] private Rigidbody[] limbs;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var limb in limbs)
        {
            var x = Random.value < 0.5f ? Random.Range(-25f, -10f) : Random.Range(10f, 25f);
            var z = Random.value < 0.5f ? Random.Range(-25f, -10f) : Random.Range(10f, 25f);
            limb.AddForce(new Vector3(x, 0, z), ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
