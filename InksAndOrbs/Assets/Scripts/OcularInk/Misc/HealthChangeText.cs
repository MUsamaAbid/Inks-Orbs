using System;
using DG.Tweening;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace OcularInk.Misc
{
 //   [RequireComponent(typeof(TextMeshPro))]
    public class HealthChangeText : MonoBehaviour
    {
        [SerializeField] private Text tmp;

        private void OnValidate()
        {
            tmp = GetComponent<Text>();
        }

        public void SetText(string text)
        {
            tmp.text = text;
        }

        public void Fire()
        {
         //   transform.DOMoveY(transform.position.y + 1f, 1.1f);
        //    var start = 1f;
         //   DOTween.To(() => start, x => start = x, 0f, 0.5f).SetDelay(0.5f).onUpdate = () =>
        //    {
         //       tmp.alpha = start;
         //   };
        }
    }
}