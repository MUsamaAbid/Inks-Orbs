using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioMixerData")]
public class AudioMixerData : ScriptableObject
{
    public AudioMixer baseMixer;
    public AudioMixerGroup sfxGroup;
    public AudioMixerGroup musicGroup;
}