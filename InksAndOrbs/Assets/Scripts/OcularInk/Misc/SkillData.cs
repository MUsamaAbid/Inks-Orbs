using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "SkillData")]
public class SkillData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public GameObject SkillPrefab;
    public int Price;
}
