using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueFlow", menuName = "Dialogue/DialogueFlow")]
public class DialogueFlow : ScriptableObject
{
    [field:SerializeField] public string[] Lines { get; private set; }
}