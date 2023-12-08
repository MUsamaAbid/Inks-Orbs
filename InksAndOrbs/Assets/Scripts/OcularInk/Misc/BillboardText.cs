using UnityEngine;

namespace OcularInk.Misc
{
    public class BillboardText : MonoBehaviour
    {
        private Camera mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
         //   transform.LookAt(transform.position + mainCamera.transform.forward);
            // transform.Rotate(0, 180, 0);
        }
    }
}