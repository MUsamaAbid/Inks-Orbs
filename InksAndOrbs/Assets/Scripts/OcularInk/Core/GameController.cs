using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters;
using OcularInk.Characters.Protagonist;
using OcularInk.Misc;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int CollectedMoney { get; private set; }
    public int Score { get; private set; }
    public float Playtime { get; private set; }

    [SerializeField] private GameObject GameFinishPoint;
    [SerializeField] private HealthChangeText hct;
    [SerializeField] private GameCanvas gameCanvas;
    [SerializeField] private GameplayCameraController gameplayCameraController;
    [SerializeField] private CinematicController cinematicController;
    [SerializeField] private ObjectPool brushAreaPool;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("current level: " + GameManager.GameData.CurrentLevel);
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
        GameManager.Instance.GameController = this;
        
        EnemyController.ForceDisable = false;
        PlayerController.ForceDisable = false;

        GameManager.Instance.PlayerController.brushController.brushAreaPool = brushAreaPool;

        Playtime = 0f;
        
        GameManager.SetGameState(GameState.Playing);

        if (GameManager.Instance.LevelController.level == 0 && PlayerPrefs.GetInt("first_cinematic", 0) == 0)
        {
            cinematicController.ShowCinematic(1);
            PlayerPrefs.SetInt("first_cinematic", 1);
        }

#if !UNITY_EDITOR
        AdmobService.instance.ShowBanner();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        Playtime += Time.deltaTime;

        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameManager.Instance.PlayerController.transform.position =
                GameObject.FindGameObjectWithTag("Finish").transform.position;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            var obj = GameObject.FindObjectsOfType<EnemyAI>();

            foreach (var item in obj)
            {
                item.TakeDamage(10000);
            }
        }
        #endif
    }

    public void ShowHealthText(string txt, Vector3 pos)
    {
        var text = Instantiate(hct, pos, Quaternion.identity);
        text.SetText(txt);
        text.Fire();
    }

    public void SetHealth(float value)
    {
        gameCanvas.SetHealth(value);
    }

    public void HitFeedback()
    {
        gameCanvas.HitFeedback();
    }

    public void ShakeScreen(float intensity, float time)
    {
        gameplayCameraController.Shake(intensity, time);
    }

    public void AddMoney(int amount = 1)
    {
        CollectedMoney += amount;
        
        gameCanvas.SetMoneyLabel((GameManager.GameData.Money + CollectedMoney).ToString());
    }

    public void UpdateConfineArea(Collider col)
    {
        gameplayCameraController.SetConfineArea(col);
    }

    public void SwitchMusic(string musicName)
    {
        AudioManager.Instance.CrossFadeMusic(musicName);
    }

    public void FinishGame()
    {
        gameCanvas.Hide();
        GameManager.GameData.Money += CollectedMoney;
        GameManager.GameData.NextLevel();
        DataManager.Save();
        if(GameManager.GameData.CurrentLevel % 4 == 0)
        {
            int extraHealth = GameManager.GameData.CurrentLevel / 4;
            Debug.Log("Increase HealthBar");
            gameCanvas.IncreaseHealthBar(extraHealth);
        }
        
        var playtimeScore = Mathf.RoundToInt(Mathf.Max(0, (600 - Playtime) * 10f));
        IncreaseScore(playtimeScore);
        
        EnemyController.ForceDisable = true;
        PlayerController.ForceDisable = true;

        var finishCanvas = UIManager.Instance.GetCanvas<FinishCanvas>();
        finishCanvas.Show();
        
        UserManager.Instance.SaveScore(Score, GameManager.Instance.LevelController.level);

        if (AdmobService.instance.IsRewIntAvailable)
        {
            finishCanvas.ShowAdModal();
        }
        else
        {
            AdmobService.instance.ShowInterstitial();
        }
    }

    public void GameOver()
    {
        gameCanvas.Hide();
        UIManager.Instance.GetCanvas<GameOverCanvas>().Show();
        EnemyController.ForceDisable = true;
        AdmobService.instance.ShowInterstitial();
        GameManager.Instance.LevelController.LogGameOver();
    }

    public void FocusOnOrb(Transform orb)
    {
        StartCoroutine(OrbFocusFlow(orb, GameManager.Instance.PlayerController));
    }

    private IEnumerator OrbFocusFlow(Transform orb, PlayerController player)
    {
        gameCanvas.Hide();
        
        gameplayCameraController.SetFocus(orb);
        player.preventAttacks = true;

        yield return new WaitForSeconds(2f);
        
        gameCanvas.Show();
        
        gameCanvas.ShowBossDefeated();

        yield return new WaitForSeconds(0.5f);
        player.preventAttacks = false;
        
        gameplayCameraController.SetFocus(player.transform);
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;

        Debug.Log($"Score: {Score}");
    }

    public void SpawnFinishPoint(Vector3 position)
    {
        Debug.Log("Spawning Finish Point");
        GameObject finishPoint = Instantiate(GameFinishPoint);
        finishPoint.transform.position = position;
    }
}
