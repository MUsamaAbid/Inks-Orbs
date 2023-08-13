using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OcularInk.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertManager : Singleton<AlertManager>
{
    [SerializeField] private GameObject modal;
    [SerializeField] private GameObject okButton;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private Image backdrop;
    [SerializeField] private Transform popup;
    [SerializeField] private TextMeshProUGUI titleLabel;
    [SerializeField] private TextMeshProUGUI descLabel;

    private bool canDismissModal;

    private Action<bool> onDismiss;

    public void ShowAlert(string title, string desc)
    {
        onDismiss = null;
        cancelButton.SetActive(false);
        modal.SetActive(true);
        titleLabel.text = title;
        descLabel.text = desc;

        popup.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBack).SetUpdate(true);
        backdrop.DOFade(0.7f, 0.9f).SetUpdate(true);

        canDismissModal = true;
        
        okButton.SetActive(canDismissModal);
        cancelButton.SetActive(canDismissModal);
    }
    
    public void ShowAlert(string title, string desc, bool canDismiss)
    {
        onDismiss = null;
        cancelButton.SetActive(false);
        canDismissModal = canDismiss;
        modal.SetActive(true);
        titleLabel.text = title;
        descLabel.text = desc;
        
        cancelButton.SetActive(canDismissModal);
        okButton.SetActive(canDismissModal);

        popup.transform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBack).SetUpdate(true);
        backdrop.DOFade(0.7f, 0.9f).SetUpdate(true);
    }

    public void ShowAlert(string title, string desc, Action<bool> onDismiss)
    {
        ShowAlert(title, desc);
        cancelButton.SetActive(true);

        this.onDismiss = onDismiss;
    }
    
    public void ShowSingleAlert(string title, string desc)
    {
        ShowAlert(title, desc);
        okButton.SetActive(true);
        cancelButton.SetActive(false);

        this.onDismiss = onDismiss;
    }

    public void Dismiss()
    {
        if (!canDismissModal)
            return;
        
        popup.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).SetUpdate(true);
        backdrop.DOFade(0f, 0.9f).SetUpdate(true).onComplete = () => modal.SetActive(false);
    }

    public void ForceDismiss()
    {
        popup.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack).SetUpdate(true);
        backdrop.DOFade(0f, 0.9f).SetUpdate(true).onComplete = () => modal.SetActive(false);
    }

    public void Continue()
    {
        onDismiss?.Invoke(true);
        Dismiss();
    }
}
