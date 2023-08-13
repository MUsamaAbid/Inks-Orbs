using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BasicModal : MonoBehaviour
{
    [SerializeField] protected Image backdropImage;

    [SerializeField] protected Transform modalRoot;

    [SerializeField] private float backdropAlpha = 0.4f;

    public bool Dismissable { get; set; } = true;

    public UnityAction onDismiss;

    public virtual void OnEnable()
    {
        backdropImage.raycastTarget = true;
        
        backdropImage.DOKill();
        modalRoot.DOKill();
        
        backdropImage.DOFade(backdropAlpha, 0.5f).From(0).SetUpdate(true);
        modalRoot.DOScale(1f, 0.5f).SetEase(Ease.OutBack).From(0).SetUpdate(true);
    }

    public void Show() => gameObject.SetActive(true);
    
    public virtual void Hide()
    {
        if (!Dismissable)
            return;

        backdropImage.DOKill();
        modalRoot.DOKill();
        
        backdropImage.DOFade(0f, 0.5f).SetUpdate(true);
        modalRoot.DOScale(0f, 0.5f).SetEase(Ease.InBack).SetUpdate(true).onComplete = () =>
        {
            gameObject.SetActive(false);
        };
        backdropImage.raycastTarget = false;
        
        onDismiss?.Invoke();
    }
}
