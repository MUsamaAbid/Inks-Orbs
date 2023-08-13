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

    public static GameState State { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        GameData = DataManager.Load();
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