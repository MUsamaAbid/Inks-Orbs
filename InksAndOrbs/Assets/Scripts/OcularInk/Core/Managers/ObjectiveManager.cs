using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private List<AllyController> allies = new ();
    [SerializeField] private List<GameObjective> objectiveList;
    
    public int CurrentObjective { get; private set; }

    private void Awake()
    {
        foreach (var allyController in allies)
        {
            if(allyController.isActiveAndEnabled)
                allyController.SetDestination(objectiveList[0].objectiveArea.position);
        }
    }

    public void NextObjective()
    {
        
    }

    private void OnDrawGizmos()
    {
        foreach (Transform child in transform)
        {
            Gizmos.DrawWireSphere(child.position, 5f);
        }
    }
}

[System.Serializable]
public class GameObjective
{
    public int targetEnemyCount;
    public Transform objectiveArea;
}