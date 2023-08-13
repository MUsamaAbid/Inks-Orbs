using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperpowerPanel : MonoBehaviour
{
    [SerializeField] private SuperpowerTimer[] timers;
    
    public void ShowTimer(int type, float duration)
    {
        if (type >= timers.Length)
            return;
        
        timers[type].StartTimer(duration);
    }
}
