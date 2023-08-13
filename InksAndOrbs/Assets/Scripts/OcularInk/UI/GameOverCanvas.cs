using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverCanvas : CanvasController
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    
    public override void Show()
    {
        base.Show();
        scoreLabel.text = GameManager.Instance.GameController.Score.ToString();
    }
    
    
    private void Update()
    {
        if (canvas.enabled)
        {
            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        SceneMaster.ReloadScene();
    }

    public void MainMenu()
    {
        SceneMaster.EnterScene(GameScene.Home);
    }
}
