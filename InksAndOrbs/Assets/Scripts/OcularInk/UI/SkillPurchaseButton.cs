using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillPurchaseButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI amountLabel;
    [SerializeField] private TextMeshProUGUI priceLabel;
    [SerializeField] private Button purchaseButton;
    public SkillData skillData;

    public void SetView(int amount, bool canPurchase)
    {
        amountLabel.text = amount.ToString();
        priceLabel.text = skillData.Price.ToString();
    }

    public void SetListener(UnityAction onClick)
    {
        button.onClick.AddListener(onClick);
    }
}
