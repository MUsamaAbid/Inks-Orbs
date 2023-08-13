using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankLabel;
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI scoreLabel;

    public void SetView(int rank, string userName, int score)
    {
        rankLabel.text = $"{rank}";
        nameLabel.text = userName;
        scoreLabel.text = $"{score}";
    }
}
