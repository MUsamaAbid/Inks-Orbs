using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters.Protagonist;
using UnityEngine;

public class BrushAreaParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    
    [SerializeField] private List<BrushArea> brushAreas;
    [SerializeField] private ParticleSystem.Particle[] particles;
    [SerializeField] private BrushArea brushAreaPrefab;
    [SerializeField] private int brushAreaAmount;

    void Awake()
    {
        brushAreas = new List<BrushArea>();
        for (int i = 0; i < brushAreaAmount; i++)
        {
            var area = Instantiate(brushAreaPrefab);

            area.gameObject.SetActive(false);

            brushAreas.Add(area);
        }

        particles = new ParticleSystem.Particle[brushAreaAmount];
    }


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            foreach (var item in brushAreas)
            {
                if (!item.gameObject.activeSelf)
                    continue;

                item.Activate();
            }

            return;
        }

        if (!Input.GetMouseButton(0))
            return;

        ps.GetParticles(particles);

        if (particles.Length == 0)
            return;

        foreach (var particle in particles)
        {
            var area = brushAreas.Find(area => !area.gameObject.activeSelf);

            if (area == null)
                return;

            area.gameObject.SetActive(true);
            area.transform.position = particle.position;
        }
    }
}
