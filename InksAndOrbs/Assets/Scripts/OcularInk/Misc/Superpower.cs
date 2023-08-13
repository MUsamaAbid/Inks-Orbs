using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Superpower : MonoBehaviour
{
    public enum SuperpowerType
    {
        Area,
        Self
    }

    [SerializeField] public SuperpowerType Type;
    [SerializeField] public SkillData SkillData;

    public int index;
    public int Amount => GameManager.GameData.Superpowers[index]; 

    private void Awake()
    {
        // Activate();
    }

    public abstract void Activate();
    public abstract void Disable();

    public virtual void Consume()
    {
        GameManager.GameData.Superpowers[index]--;
        DataManager.Save();
    }
}
