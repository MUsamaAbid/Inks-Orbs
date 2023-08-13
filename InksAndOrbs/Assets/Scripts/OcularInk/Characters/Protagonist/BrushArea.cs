using System;
using UnityEngine;

namespace OcularInk.Characters.Protagonist
{
    public class BrushArea : MonoBehaviour
    {
        [SerializeField] private Collider collider;
        
        private void OnDisable()
        {
            collider.enabled = false;
        }

        public void Activate()
        {
            collider.enabled = true;
            Invoke(nameof(Disable), Time.deltaTime * 3f);
        }

        private void OnValidate()
        {
            collider = GetComponent<Collider>();
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
