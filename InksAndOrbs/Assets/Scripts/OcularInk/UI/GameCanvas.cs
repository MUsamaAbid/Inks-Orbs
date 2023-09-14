using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCanvas : CanvasController
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image hitFeedback;
    [SerializeField] private Image blackout;

    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI moneyLabel;
    [SerializeField] private TextMeshProUGUI debugLabel;
    [SerializeField] private BasicModal pauseModal;

    [SerializeField] private Image bossDefeated;

    public SuperpowerPanel superpowerPanel;

    protected override void Awake()
    {
        base.Awake();
        SetMoneyLabel(GameManager.GameData.Money.ToString());
        var hpBar = (RectTransform)healthBar.transform.parent;
        Debug.Log(GameManager.GameData.HealthLevel);
        hpBar.sizeDelta = new Vector2(250 + (GameManager.GameData.HealthLevel * 50f), hpBar.sizeDelta.y);
    }

    public void SetHealth(float value)
    {
        healthBar.DOKill();
        healthBar.DOFillAmount(value, 0.2f).SetEase(Ease.OutFlash);
    }

    public void HitFeedback()
    {
        hitFeedback.DOComplete();
        hitFeedback.DOFade(1f, 0.25f);
        hitFeedback.DOFade(0f, 1f).SetDelay(0.5f);
    }

    public void ToggleInteractButton(bool value)
    {
        if (value)
        {
            interactButton.interactable = true;
            interactButton.gameObject.SetActive(true);
            interactButton.transform.DOKill();
            interactButton.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
            return;
        }

        interactButton.interactable = false;
        interactButton.transform.DOKill();
        interactButton.transform.DOScale(0f, 0.1f).SetEase(Ease.InBack).onComplete = () =>
        {
            interactButton.gameObject.SetActive(false);
        };
    }

    public void Interact()
    {
        GameManager.Instance.DialogueController.ShowDialogue();
    }

    public void SetMoneyLabel(string value)
    {
        moneyLabel.text = value;
    }

    public void TogglePauseMenu(bool value)
    {
        if (value)
        {
            pauseModal.Show();
            Time.timeScale = 0f;
        }
        else
        {
            pauseModal.Hide();
            Time.timeScale = 1f;
        }
    }

    public void ShowBossDefeated()
    {
        bossDefeated.gameObject.SetActive(true);
        bossDefeated.DOFade(1f, 0.4f).From(0f);
        bossDefeated.transform.DOScale(1f, 0.3f).From(0f).SetEase(Ease.OutBack);
        
        bossDefeated.DOFade(0f, 0.5f).SetDelay(5f).onComplete = () => bossDefeated.gameObject.SetActive(false);
    }

    public void SetDebugText(string text)
    {
        debugLabel.text = text;
    }
    public void IncreaseHealthBar(int health)
    {
        Debug.Log("extra raise by: " + health);
        healthBar.rectTransform.sizeDelta = new Vector2(healthBar.rectTransform.rect.width+100 * health, healthBar.rectTransform.rect.height);
    }
}