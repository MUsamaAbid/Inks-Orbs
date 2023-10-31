using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster
{
    public static bool LevelLoading { get; private set; }
    
    private static object parameters;

    private const int LevelStartOffset = 3;

    static SceneMaster()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    public static void EnterScene(GameScene scene)
    {
        SceneManager.LoadScene((int)scene);

        LevelLoading = true;
    }

    public static void EnterLevel(int levelId)
    {
        SceneManager.LoadScene(levelId + LevelStartOffset);

        LevelLoading = true;
    }
    
    public static void SetParams(object param)
    {
        parameters = param;
    }

    public static T GetParams<T>() where T : class
    {
        return parameters as T;
    }

    public static void ClearParams()
    {
        parameters = null;
    }

    private static void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelLoading = false;
    }

    public static void ReloadScene()
    {
        LevelLoading = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum GameScene
{
    Loading, Home, Shards, Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Level10, Level11, Level12, Level13, Level14, Level15, Level16, Level17, Level18, Level19, Level20, Level21, Level22, Level23, Level24
}