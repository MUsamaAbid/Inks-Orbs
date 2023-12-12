using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters.Protagonist;
using OcularInk.Utils;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameController GameController { get; set; }
    public PlayerController PlayerController { get; set; }
    public DialogueController DialogueController { get; set; }
    public CollisionController CollisionController { get; set; }
    public LevelController LevelController { get; set; }

    public static GameData GameData;
    public int bossestokill;
    public int killedbosses;
    public static GameState State { get; private set; }
    public void Start()
    {
        killedbosses = 0;
    }
    protected override void Awake()
    {
        base.Awake();
       
        GameData = DataManager.Load();
        if (PlayerPrefs.GetInt("ft") == 0)
        {
            
            PlayerPrefs.SetInt("ft", 1);
            PlayerPrefs.SetInt("selectedball", 0);
            PlayerPrefs.SetInt("ball0", 1);
        }
    }

    public static void SetGameState(GameState newState)
    {
        State = newState;
    }
}

public enum GameState
{
    Home,
    Playing,
    Cinematic,
    GameOver,
    Finish
}