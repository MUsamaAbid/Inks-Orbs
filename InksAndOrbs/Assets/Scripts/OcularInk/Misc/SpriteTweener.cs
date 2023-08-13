using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpriteTweener : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float duration = 0.5f;

    [SerializeField] private float targetAlpha = 0.8f;

    void Start()
    {
        spriteRenderer.DOFade(targetAlpha, duration).From(0f).SetLoops(-1, LoopType.Yoyo);
    }
}

