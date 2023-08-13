using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cinematic_1", menuName = "Cinematic Data")]
public class CinematicData : ScriptableObject
{
    public string[] lines;
    public string audioName;
    public string clipName;
}