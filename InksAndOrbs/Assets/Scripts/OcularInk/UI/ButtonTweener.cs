using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTweener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Ease downEase = Ease.OutQuart;
    [SerializeField] private Ease upEase = Ease.OutBack;
    [SerializeField] private Vector3 baseScale = Vector3.one;
    [SerializeField] private Vector3 targetScale = new (0.9f, 0.9f, 0.9f);
    [SerializeField] private float downDuration = 0.15f;
    [SerializeField] private float upDuration = 0.25f;

    [SerializeField] private Button button;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button && !button.interactable)
            return;

        transform.DOScale(targetScale, downDuration).From(baseScale).SetEase(downEase).SetUpdate(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button && !button.interactable)
            return;

        transform.DOScale(baseScale, upDuration).SetEase(upEase).SetUpdate(true);
    }

    private void OnValidate()
    {
        button = GetComponent<Button>();
    }
}
