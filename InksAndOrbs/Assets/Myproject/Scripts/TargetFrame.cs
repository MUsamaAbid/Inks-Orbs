using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrame : MonoBehaviour
{
    [SerializeField] int target;
    private void Awake()
    {
        Application.targetFrameRate = target;
    }
}
