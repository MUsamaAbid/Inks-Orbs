using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsPanel : MonoBehaviour
{
    [SerializeField] private SkillPurchaseButton[] skillButtons;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        var unlocked = GameManager.GameData.UnlockedSuperpowers;

        var coins = GameManager.GameData.Money;
        var amount = GameManager.GameData.Superpowers;

        for (var i = 0; i < skillButtons.Length; i++)
        {
            var index = i;
            var skillPurchaseButton = skillButtons[i];
            skillPurchaseButton.SetView(amount[i], coins > skillPurchaseButton.skillData.Price);
            skillPurchaseButton.SetListener(() => { PurchasePowerup(index); });
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            var btn = skillButtons[i];
            
            btn.gameObject.SetActive(unlocked[i]);
        }
    }

    private void PurchasePowerup(int index)
    {
        var coins = GameManager.GameData.Money;

        var data = skillButtons[index].skillData;

        if (coins < data.Price)
        {
            AlertManager.Instance.ShowSingleAlert("PURCHASE FAILED", "Insufficient balance");
            return;
        }

        GameManager.GameData.Superpowers[index]++;
        GameManager.GameData.Money -= 100;

        AlertManager.Instance.ShowSingleAlert("SUCCESS", "Purchased superpower successfully!");
        
        DataManager.Save();
        
        UpdateView();
    }

    private void UpdateView()
    {
        var coins = GameManager.GameData.Money;
        var amount = GameManager.GameData.Superpowers;
        
        for (var i = 0; i < skillButtons.Length; i++)
        {
            var skillPurchaseButton = skillButtons[i];
            skillPurchaseButton.SetView(amount[i], coins > skillPurchaseButton.skillData.Price);
        }
        
        UIManager.Instance.GetCanvas<HomeSharedCanvas>().UpdateMoneyLabel();
    }
}