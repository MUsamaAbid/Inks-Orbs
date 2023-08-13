using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public Transform pos;
    private Dictionary<string, GameObject> loadedParticles = new();

    public void SpawnParticle(string particleName)
    {
        GameObject particle;
        
        // Check if it exists
        if (loadedParticles.ContainsKey(particleName))
        {
            particle = loadedParticles[particleName];
        }
        else
        {
            particle = Resources.Load<GameObject>($"Particles/{particleName}");
            loadedParticles.Add(particleName, particle);
        }

        GameManager.Instance.GameController.ShakeScreen(.5f, 0.6f);
        Instantiate(particle, pos.position, Quaternion.identity);
    }
}
