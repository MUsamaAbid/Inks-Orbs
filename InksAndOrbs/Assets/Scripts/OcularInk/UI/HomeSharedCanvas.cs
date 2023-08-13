using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeSharedCanvas : CanvasController
{
    [SerializeField] private TextMeshProUGUI moneyLabel;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        UpdateMoneyLabel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMoneyLabel()
    {
        moneyLabel.text = GameManager.GameData.Money.ToString();
    }
}
