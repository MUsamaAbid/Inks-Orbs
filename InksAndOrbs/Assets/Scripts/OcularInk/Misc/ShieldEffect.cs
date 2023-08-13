using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    [SerializeField] private Transform shields;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        
        shields.Rotate(new Vector3(0, 100 * Time.deltaTime, 0));
    }
}
