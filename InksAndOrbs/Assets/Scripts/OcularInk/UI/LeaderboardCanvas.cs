using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardCanvas : CanvasController
{
    [SerializeField] private LeaderboardEntry leaderboardEntryPrefab;
    [SerializeField] private Transform listRoot;
    [SerializeField] private GameObject loadingView;
    [SerializeField] private GameObject leaderboardView;

    [SerializeField] private Image[] levelButtons;
    [SerializeField] private TextMeshProUGUI[] levelButtonTexts;
    [SerializeField] private LeaderboardEntry selfUserEntry;
    [SerializeField] private GameObject noRecords;
    
    private int selectedLevel;

    private void Start()
    {
        selectedLevel = 0;
        FetchLeaderboard();
    }

    public async Task FetchLeaderboard()
    {
        loadingView.SetActive(true);
        leaderboardView.SetActive(false);

        Debug.Log(selectedLevel);
        
        var users = await UserManager.Instance.GetLeaderboard(selectedLevel);
        
        // clear children
        foreach (Transform child in listRoot)
        {
            if (child.gameObject.name == "NoRecords")
                continue;
            
            Destroy(child.gameObject);
        }

        for (var i = 0; i < users.Count; i++)
        {
            var user = users[i];

            var userEntryInstance = Instantiate(leaderboardEntryPrefab, listRoot);
            userEntryInstance.SetView(i + 1, user.Name, user.GetScore(selectedLevel));
        }
        
        noRecords.SetActive(users.Count == 0);

        var selfUser = UserManager.Instance.User;
        selfUserEntry.SetView(0, selfUser.Name, selfUser.GetScore(selectedLevel));
        
        
        loadingView.SetActive(false);
        leaderboardView.SetActive(true);
    }

    public void SelectLevel(int index)
    {
        for (var i = 0; i < levelButtons.Length; i++)
        {
            var levelButton = levelButtons[i];
            var levelButtonText = levelButtonTexts[i];

            if (i == index)
            {
                levelButton.DOFade(1f, 0.3f);
                levelButtonText.DOColor(new Color32(53, 173, 56, 255), 0.3f);
            }
            else
            {
                levelButton.DOFade(0f, 0.3f);
                levelButtonText.DOColor(Color.white, 0.3f);
            }
        }

        selectedLevel = index;
        FetchLeaderboard();
    }
}
