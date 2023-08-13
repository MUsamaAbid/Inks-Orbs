using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using OcularInk.Utils;
using UnityEngine;

public class AuthService : Singleton<AuthService>
{
    public bool IsAuthenticated { get; private set; }
    public bool IsGuest { get; private set; }

    public Action<bool> onAuthenticate;

    private FirebaseAuth auth;

    protected override void Awake()
    {
        base.Awake();
        auth = FirebaseAuth.DefaultInstance;
        
#if UNITY_ANDROID && !UNITY_EDITOR
        InitializeGooglePlayGames();
#elif UNITY_IOS && !UNITY_EDITOR
        SignInWithGameCenter();
#endif
    }

    public void SignIn()
    {
        #if UNITY_ANDROID
        SignInWithGooglePlay();
        #elif UNITY_IOS
        SignInWithGameCenter();
        #endif
    }

    private void InitializeGooglePlayGames()
    {
#if UNITY_ANDROID
        PlayGamesPlatform.DebugLogEnabled = true;

        var config = new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestServerAuthCode(false)
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
#endif
    }
    
    public void SignInWithGooglePlay()
    {
#if UNITY_ANDROID
        Social.localUser.Authenticate(async (success) =>
        {
            Debug.Log("Authenticated: " + success);

            if (success)
            {
                var authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                
                var credential = PlayGamesAuthProvider.GetCredential(authCode);
                
                var user = await auth.SignInWithCredentialAsync(credential);

                UserManager.Instance.CreateUserIfNotExists(user.UserId);
                
                UserManager.Instance.ListenToUser(user.UserId);

                onAuthenticate.Invoke(true);

                IsAuthenticated = true;
                IsGuest = false;

                // var email = GameManager.GameData.AccountEmail;
                //
                // if (email != null)
                // {
                //     StartCoroutine(MigrateWorkflow(email, user.UserId));
                // }
            }
        });
#endif
    }

    public void SignInWithGameCenter()
    {
        Social.localUser.Authenticate(async (res) =>
        {
            if (res)
            {
                var credential = await GameCenterAuthProvider.GetCredentialAsync();
                var user = await auth.SignInWithCredentialAsync(credential);
                
                UserManager.Instance.CreateUserIfNotExists(user.UserId);

                UserManager.Instance.ListenToUser(user.UserId);
                
                onAuthenticate.Invoke(true);

                IsAuthenticated = true;
                IsGuest = false;

            }
        });
    }
}
