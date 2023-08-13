using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    [SerializeField] private Object target;

    void Start()
    {
        if (!target)
            target = gameObject;
        
        Destroy(target, destroyTime);
    }
}
