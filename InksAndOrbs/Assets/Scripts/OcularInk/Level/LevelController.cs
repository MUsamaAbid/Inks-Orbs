using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private string levelName;

    public int level;

    void Awake()
    {
        AudioManager.Instance.SetMusic($"BGM/{levelName}");

        GameManager.Instance.LevelController = this;
        
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level_{level + 1}");
    }

    public void NextLevel()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level_{level + 1}");
        try
        {
            var unlocked = GameManager.GameData.UnlockedSuperpowers;

            int skillSelected = -1;

            // Check if power unlocked
            switch (level)
            {
                case 1: // Verdantia 2
                    skillSelected = 0;
                    break;
                case 2: // Verdantia 3
                    skillSelected = 1;
                    break;
                case 4: // Scorchia 2
                    skillSelected = 2;
                    break;
                case 5: // Scorchia 3
                    skillSelected = 3;
                    break;
                case 7: // Frostpeak 1
                    skillSelected = 4;
                    break;
                case 8: // Tropicora 1
                    skillSelected = 5;
                    break;
            }
            
            if (skillSelected >= 0)
            {
                if (unlocked[skillSelected])
                {
                    SceneMaster.EnterLevel(GameManager.GameData.CurrentLevel);
                    return;
                }

                GameManager.GameData.UnlockedSuperpowers[skillSelected] = true;
                DataManager.Save();

                SkillUnlockController.SkillIndex = skillSelected;
                SceneMaster.EnterScene(GameScene.Shards);

                return;
            }

            SceneMaster.EnterLevel(GameManager.GameData.CurrentLevel);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    public void LogGameOver()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"Level_{level + 1}");
    }
}