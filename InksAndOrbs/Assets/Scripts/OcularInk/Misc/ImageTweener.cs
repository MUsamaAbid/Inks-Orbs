using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageTweener : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private float duration = 0.5f;

    [SerializeField] private float targetAlpha = 0.8f;

    void Start()
    {
        image.DOFade(targetAlpha, duration).From(0f).SetLoops(-1, LoopType.Yoyo);
    }
}
