using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Profiling.Memory.Experimental;

public class AdmobService : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private InterstitialAd interstitialAd;
    private RewardedInterstitialAd rewardedInterstitialAd;

    public static AdmobService instance;

    [Header("AdMob Settings")]
    // Replace IDs with your own IDs
    public string interstitialIDAndroid = "ca-app-pub-3940256099942544/1033173712";

    public string rewardedVideoIDAndroid = "ca-app-pub-3940256099942544/5224354917";
    public string bannerIDAndroid = "ca-app-pub-3940256099942544/5224354917";
    public string rewInstIDAndroid = "ca-app-pub-8399662907709420/4718924658";

    public string interstitialIDIOS = "ca-app-pub-8399662907709420/3391786140";
    public string rewardedVideoIDIOS = "ca-app-pub-8399662907709420/8001114998";
    public string bannerIDIOS = "ca-app-pub-8399662907709420/9777231098";
    public string rewInstIDIOS = "ca-app-pub-8399662907709420/8370448950";

//#if UNITY_ANDROID
    private string interstitialID => interstitialIDAndroid;
    private string rewardedVideoID => rewardedVideoIDAndroid;
    private string bannerID => bannerIDAndroid;
    private string rewInstID => rewInstIDAndroid;
/*#elif UNITY_IOS
    private string interstitialID => interstitialIDIOS;
    private string rewardedVideoID => rewardedVideoIDIOS;
    private string bannerID => bannerIDIOS;
    private string rewInstID => rewInstIDIOS;
#endif*/

    private BannerView bannerView;

    // Set to false in production
    public bool testMode;

    [Header("Interstitial Ad Options")] public float interstitialAdDelay = 0f;
    public InterstitialAdFrequency interstitialAdFrequency;

    private Action<bool> rewardCallback;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Initialize();
    }

    public void Initialize()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(status => { });

        RequestRewardBasedVideo();
        RequestInterstitial();
        RequestBanner();
        RequestRewardedInterstitialAd();

        interstitialAdFrequency.Value = interstitialAdFrequency.BaseValue;
    }

    private void RequestInterstitial()
    {
        if (interstitialID == string.Empty)
            return;

        interstitialAd = new InterstitialAd(interstitialID);
        // Create an empty ad request.
        AdRequest request = CreateAdRequest();
        // Load the interstitial with the request.
        interstitialAd.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
        if (rewardedVideoID == string.Empty)
            return;

        rewardedAd = new RewardedAd(rewardedVideoID);
        // Create an empty ad request.
        AdRequest request = CreateAdRequest();
        // Load the rewarded video ad with the request.
        rewardedAd.LoadAd(request);
        // Setting up event handler
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }

    private void RequestBanner()
    {
        if (GameManager.GameData.NoAds)
            return;

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.TopRight);
        bannerView.LoadAd(CreateAdRequest());
        bannerView.Show();
    }

    public void RequestRewardedInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }

        Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = CreateAdRequest();

        // send the request to load the ad.
        RewardedInterstitialAd.Load(rewInstID, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("rewarded interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }

    private AdRequest CreateAdRequest()
    {
        // Create an empty ad request.
        return new AdRequest.Builder()
            .Build();
    }

    public void ShowRewardedAd(Action<bool> callback)
    {
#if UNITY_EDITOR
        callback.Invoke(true);
        return;
#endif

        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            RequestRewardBasedVideo();
        }

        rewardCallback = callback;
        RequestRewardBasedVideo();
    }

    public void ShowBanner()
    {
        if (GameManager.GameData.NoAds)
            return;

        bannerView.Show();
    }

    public void ShowInterstitial()
    {
#if UNITY_EDITOR
        return;
#endif

        _showInterstitial();
        return;

        if (interstitialAdFrequency.Value + 1 < interstitialAdFrequency.ShowFrequency)
        {
            interstitialAdFrequency.Value++;
            return;
        }

        // Start coroutine instead if there is a delay
        if (interstitialAdDelay > 0)
        {
            Invoke(nameof(_showInterstitial), interstitialAdDelay);
            return;
        }

        _showInterstitial();
    }

    public void ShowRewardedInterstitial(Action<Reward> callback)
    {
        rewardedInterstitialAd.Show(callback);
    }

    private void _showInterstitial()
    {
        if (GameManager.GameData.NoAds)
            return;

        // IF AdMob ready, show
        if (interstitialAd != null && interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        else
        {
            RequestInterstitial();
            return;
        }

        interstitialAdFrequency.Value = 0;
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        rewardCallback.Invoke(true);
    }

    public bool IsRewIntAvailable => rewardedInterstitialAd.CanShowAd();
}

[System.Serializable]
public struct InterstitialAdFrequency
{
    [Tooltip("Used for declaring the frequency of showing interstitial ads. For example: Show per X requests.")]
    public int ShowFrequency;

    [Tooltip(
        "Base value for interstitial ad frequency. If you set it same with show frequency, then it will show at first request.")]
    public int BaseValue;

    [HideInInspector] public int Value;
}