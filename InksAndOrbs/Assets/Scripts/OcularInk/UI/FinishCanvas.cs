using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishCanvas : CanvasController
{
    [SerializeField] private BasicModal rewardedInterstitialModal;
    [SerializeField] private TextMeshProUGUI rewardedAdCountdown;
    [SerializeField] private TextMeshProUGUI scoreLabel;

    public override void Show()
    {
        base.Show();
        UIManager.Instance.GetCanvas<GameCanvas>().gameObject.SetActive(false);
        scoreLabel.text = GameManager.Instance.GameController.Score.ToString();
    }
    
    public void NextLevel()
    {
        GameManager.Instance.LevelController.NextLevel();
        // SceneMaster.EnterLevel(GameManager.GameData.CurrentLevel);
    }

    public void ShowAdModal()
    {
        rewardedInterstitialModal.Show();
        StartCoroutine(ShowRewardedInterstitial());
    }

    public void MainMenu()
    {
        SceneMaster.EnterScene(GameScene.Home);
    }

    private bool isSkipped;

    private IEnumerator ShowRewardedInterstitial()
    {
        for (int i = 5; i > 0; i--)
        {
            rewardedAdCountdown.text = $"Video loading in {i}";
            yield return new WaitForSeconds(1f);
        }

        if (isSkipped)
        {
            yield break;
        }

        /*AdmobService.instance.ShowRewardedInterstitial((reward) =>
        {
            AlertManager.Instance.ShowSingleAlert("SUCCESS", "You've earned 5 coins!");
            
            GameManager.GameData.Money += 5;
            DataManager.Save();
        });*///usama
        
        rewardedInterstitialModal.Hide();
    }

    public void SkipInterstitialRewarded()
    {
        rewardedInterstitialModal.Hide();
        isSkipped = true;
        StopCoroutine(ShowRewardedInterstitial());
    }
}
