using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string fileDir = "/data/";
    private const string fileName = "ocular.json";

    public static void Save()
    {
        string dir = Application.persistentDataPath + fileDir;

        Debug.Log(Application.persistentDataPath + fileDir);

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string hash = EncryptHelper.CreateHash(JsonUtility.ToJson(GameManager.GameData));
        PlayerPrefs.SetString("hash", hash);

        string json = JsonUtility.ToJson(GameManager.GameData);
        Debug.Log(json);
        File.WriteAllText(dir + fileName, json);
    }

    public static GameData Load()
    {
        string fullPath = Application.persistentDataPath + fileDir + fileName;

        GameData data = new GameData();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<GameData>(json);

            Debug.Log(fullPath);

            if (!EncryptHelper.VerifyHash(json, PlayerPrefs.GetString("hash", "initial_hash")))
            {
                Debug.LogWarning("INVALID HASH!");
            }
            else
            {
                Debug.Log("Hash is valid!");
            }
        }
        else
        {
            string dir = Application.persistentDataPath + fileDir;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string json = JsonUtility.ToJson(data);

            string hash = EncryptHelper.CreateHash(json);
            PlayerPrefs.SetString("hash", hash);

            File.WriteAllText(dir + fileName, json);
        }

        return data;
    }
}

[System.Serializable]
public class GameData
{
    public int Money;
    public int HealthLevel;
    public int CurrentLevel;
    public bool IsStarted;
    public bool NoAds;
    public int[] Superpowers = { 3, 3, 3, 3, 3, 3 };
    public bool[] UnlockedSuperpowers = { false, false, false, false, false, false };

    public GameData()
    {
        Money = 0;
        HealthLevel = 0;
        CurrentLevel = 0;
        IsStarted = false;
        NoAds = false;
    }

    public void NextLevel()
    {
        CurrentLevel = Mathf.Clamp(CurrentLevel + 1, 0, 9);
    }

    public void ConsumeSuperpower(int index)
    {
        Superpowers[index] = Mathf.Max(0, Superpowers[index] - 1);
        DataManager.Save();
    }
}