using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] Vector3 axis = new Vector3(0, 1, 0);
    [SerializeField] float speed = 1;

    void Update()
    {
        transform.Rotate(axis * speed * Time.deltaTime * 50, Space.Self);
    }

    public void ChangeAxis(Vector3 value) 
    {
        axis = value;
    }
}
