using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : CanvasController
{
    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject upgradeMaxWrapper;
    [SerializeField] private GameObject upgradeWrapper;
    [SerializeField] private TextMeshProUGUI upgradeCostLabel;
    [SerializeField] private int[] upgradeCosts;
    [SerializeField] private GameObject skillsTab;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateView();
        var unlocked = GameManager.GameData.UnlockedSuperpowers;
        
        if (!unlocked.Any(superpower => superpower))
        {
            skillsTab.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        UIManager.Instance.GetCanvas<HomeCanvas>().Show();
        Hide();
    }

    public void Upgrade()
    {
        var healthLevel = GameManager.GameData.HealthLevel;
        var money = GameManager.GameData.Money;

        if (healthLevel + 1 >= upgradeCosts.Length)
            return;

        var cost = upgradeCosts[healthLevel + 1];

        if (money < cost)
        {
            // Insufficient money
            AlertManager.Instance.ShowSingleAlert("UPGRADE FAILED", "You don't have enough money");
            return;
        }

        GameManager.GameData.Money -= cost;
        healthLevel = ++GameManager.GameData.HealthLevel;
        DataManager.Save();

        AlertManager.Instance.ShowSingleAlert("UPGRADE SUCCESS", $"Health upgraded to Lv.{healthLevel}");
        UpdateView();
    }

    public void WatchAd()
    {
        /*AdmobService.instance.ShowRewardedAd((value) =>
        {
            if (value)
            {
                GameManager.GameData.Money += 100;
                DataManager.Save();
                
                AlertManager.Instance.ShowSingleAlert("SUCCESS", "You earned 100$");
                UIManager.Instance.GetCanvas<HomeSharedCanvas>().UpdateMoneyLabel();
            }
        });*/ //usama
    }

    private void UpdateView()
    {
        var healthLevel = GameManager.GameData.HealthLevel;
        
        levelLabel.text = $"Lv.{healthLevel + 1}";

        if (healthLevel + 1 >= upgradeCosts.Length)
        {
            upgradeWrapper.SetActive(false);
            upgradeMaxWrapper.SetActive(true);
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeCostLabel.text = upgradeCosts[healthLevel + 1].ToString();
            upgradeWrapper.SetActive(true);
            upgradeMaxWrapper.SetActive(false);
            upgradeButton.interactable = true;
        }
        
        UIManager.Instance.GetCanvas<HomeSharedCanvas>().UpdateMoneyLabel();
    }

    public void PurchaseCoins(int index)
    {
        var coins = new[] { 500, 1500, 5000 };

        var coinAmount = coins[index];
        
        AlertManager.Instance.ShowSingleAlert("SUCCESS", $"Purchased {coinAmount} coins successfully!");

        GameManager.GameData.Money += coinAmount;
        DataManager.Save();

        Debug.Log($"Purchase {coinAmount} coins");
        
        UIManager.Instance.GetCanvas<HomeSharedCanvas>().UpdateMoneyLabel();
    }

    public void PurchaseFailed()
    {
        AlertManager.Instance.ShowSingleAlert("FAILED", $"Failed to purchase product.");
    }
}
