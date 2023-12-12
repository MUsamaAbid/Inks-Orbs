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
        if (GameData.CurrentLevel > 28)
        {
            bossestokill = 4;
            killedbosses = 0;
        }
        else if (GameData.CurrentLevel > 18 && GameData.CurrentLevel <29)
        {
            bossestokill = 3;
            killedbosses = 0;
        }
        else if (GameData.CurrentLevel > 8 && GameData.CurrentLevel < 19)
        {
            bossestokill = 2;
            killedbosses = 0;
        }
        else 
        {
            bossestokill = 1;
            killedbosses = 0;
        }
        print("killedbosses" + killedbosses + "and bossestokill" + bossestokill);
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