using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class GAIDFA : MonoBehaviour, IGameAnalyticsATTListener
{
    private bool willLaunch;
    
    void Start()
    {
        if(Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GameAnalytics.RequestTrackingAuthorization(this);
            Invoke(nameof(LaunchGame), 1f);
        }
        else
        {
            GameAnalytics.Initialize();
            LaunchGame();
        }
    }

    public void GameAnalyticsATTListenerNotDetermined()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerRestricted()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerDenied()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerAuthorized()
    {
        GameAnalytics.Initialize();
    }

    private void LaunchGame()
    {
        if (willLaunch)
            return;

        willLaunch = true;
        SceneMaster.EnterScene(GameScene.Home);
    }
}
