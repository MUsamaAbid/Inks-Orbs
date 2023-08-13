using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SuperpowerTimer : MonoBehaviour
{
    [SerializeField] private Image fill;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    public void StartTimer(float duration)
    {
        gameObject.SetActive(true);
        transform.DOKill();
        fill.DOKill();

        gameObject.SetActive(true);
        transform.DOScale(1f, 0.35f).SetEase(Ease.OutBack);
        fill.DOFillAmount(0f, duration).From(1f).SetEase(Ease.Linear).onComplete = OnComplete;
    }

    private void OnComplete()
    {
        transform.DOScale(0f, 0.35f).SetEase(Ease.InBack).onComplete = () => gameObject.SetActive(false);
    }
}
