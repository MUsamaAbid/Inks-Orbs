using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class CanvasController : MonoBehaviour
{
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected CanvasGroup canvasGroup;

    public UnityAction<bool> OnToggle;

    public bool Enabled => canvas.enabled;

    private bool isQuitting;

    public virtual void Show()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1f, 0.25f).From(0f).SetUpdate(true);
        canvas.enabled = true;
        OnToggle?.Invoke(false);
    }

    public virtual void Hide()
    {
        canvasGroup.DOFade(0f, 0.25f).SetUpdate(true).onComplete = () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvas.enabled = false;
            OnToggle?.Invoke(true);
        };
    }

    private void OnValidate()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void Awake()
    {
        UIManager.Instance.RegisterCanvas(this);
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        if (isQuitting)
            return;
        
        UIManager.Instance.UnregisterCanvas(this);
    }

}