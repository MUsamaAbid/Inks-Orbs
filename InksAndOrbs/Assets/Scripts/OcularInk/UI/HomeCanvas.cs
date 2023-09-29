using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeCanvas : CanvasController
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject removeAdsButton;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider fxVolumeSlider;

    [SerializeField] private GameObject signInButton;
    [SerializeField] private GameObject gameCenterIcon;
    [SerializeField] private GameObject playGamesIcon;

    [SerializeField] private NameModal nameModal;

    private void Start()
    {
        continueButton.SetActive(GameManager.GameData.IsStarted);
        removeAdsButton.SetActive(!GameManager.GameData.NoAds);


        musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
        fxVolumeSlider.value = AudioManager.Instance.FxVolume;
        AudioManager.Instance.SetMusic("BGM/MainMenu");

        if (AuthService.Instance.IsAuthenticated)
        {
            signInButton.SetActive(false);
            return;
        }
        
#if !UNITY_EDITOR
        SignIn();
#endif

        AuthService.Instance.onAuthenticate = (value) =>
        {
            if (value && signInButton.activeSelf)
            {
                signInButton.SetActive(false);

                UserManager.Instance.onSnapshot = (user) =>
                {
                    AlertManager.Instance.ShowSingleAlert("SUCCESS", "Signed in successfully!");

                    if (user.Name == null || user.Name.Trim().Length == 0)
                    {
                        nameModal.Show();
                    }

                    UserManager.Instance.onSnapshot = null;
                };
            }
        };

#if UNITY_ANDROID
        playGamesIcon.SetActive(true);
#else
        gameCenterIcon.SetActive(true);
#endif
    }

    public void Continue()
    {
        SceneMaster.EnterLevel(GameManager.GameData.CurrentLevel);
    }

    public void NewGame()
    {
        GameManager.GameData.IsStarted = true;
        GameManager.GameData.CurrentLevel = 0;
        DataManager.Save();
        Continue();
    }

    public void Upgrades()
    {
        UIManager.Instance.GetCanvas<ShopCanvas>().Show();
        Hide();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HandleRemoveAdsClick()
    {
        AlertManager.Instance.ShowAlert("PROCESSING", "Initiating purchase...", false);
    }

    public void RemoveAds()
    {
        AlertManager.Instance.ShowSingleAlert("SUCCESS", "Ads are removed successfully!");
        GameManager.GameData.NoAds = true;
        removeAdsButton.SetActive(false);
        DataManager.Save();
        //AdmobService.instance.HideBanner(); usama
    }

    public void PurchaseFailed()
    {
        AlertManager.Instance.ShowSingleAlert("FAILED", "Failed to purchase product");
    }

    public void ToggleSettings(bool value)
    {
        mainMenu.SetActive(!value);
        settingsMenu.SetActive(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void OnFxVolumeChanged(float value)
    {
        AudioManager.Instance.SetFxVolume(value);
    }

    public void SignIn()
    {
        //Usama commented
        //AlertManager.Instance.ShowAlert("SIGNING IN", "Please wait...", false);
        //AuthService.Instance.SignIn();
    }
}