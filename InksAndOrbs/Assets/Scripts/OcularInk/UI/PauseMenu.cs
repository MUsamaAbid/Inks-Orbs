using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BasicModal
{
    [SerializeField] private GameObject defaultMenu;
    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider fxVolumeSlider;

    private void Start()
    {
        musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
        fxVolumeSlider.value = AudioManager.Instance.FxVolume;
    }

    private void Update()
    {
        if (modalRoot.gameObject.activeSelf)
            Time.timeScale = 0f;
    }

    public void ToggleOptions(bool value)
    {
        defaultMenu.SetActive(!value);
        optionsMenu.SetActive(value);
    }

    public void MainMenu()
    {
        AlertManager.Instance.ShowAlert("WARNING", "Are you sure to go back to the main menu? The progress will be lost.",
            (value) =>
            {
                SceneMaster.EnterScene(GameScene.Home);
            });
    }

    public void Restart()
    {
        AlertManager.Instance.ShowAlert("WARNING", "Are you sure to restart game? The progress will be lost.",
            (value) =>
            {
                SceneMaster.ReloadScene();
            });
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }
    
    public void OnFxVolumeChanged(float value)
    {
        AudioManager.Instance.SetFxVolume(value);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
