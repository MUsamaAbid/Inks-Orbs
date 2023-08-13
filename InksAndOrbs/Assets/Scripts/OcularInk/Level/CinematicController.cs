using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class CinematicController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinematicCamera;
    [SerializeField] private CinemachineVirtualCamera gameplayCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private CinematicCanvas cinematicCanvas;

    private CinematicData activeCinematic;

    public void ShowCinematic(int index)
    {
        GameManager.SetGameState(GameState.Cinematic);
        cinematicCamera.enabled = true;
        gameplayCamera.enabled = false;
        
        var cinematicData = Resources.Load<CinematicData>($"Cinematics/Cinematic_{index}");

        if (cinematicData == null)
        {
            Debug.LogError("Failed to run cinematic, reason: Invalid cinematic index");
            return;
        }
        
        animator.Play(cinematicData.clipName);

        if (!string.IsNullOrEmpty(cinematicData.clipName))
        {
            AudioManager.Instance.SetMusic(cinematicData.audioName);
        }
        
        cinematicCanvas.ToggleBlackbars(true);
        activeCinematic = cinematicData;
        UIManager.Instance.GetCanvas<GameCanvas>().Hide();
    }

    public void PlayLine(int index)
    {
        if (index > activeCinematic.lines.Length - 1)
        {
            Debug.LogError("Failed to play line, reason: Index out of bounds");
            return;
        }
        
        var line = activeCinematic.lines[index];
        cinematicCanvas.ShowLine(line);
    }

    public void FinishCinematic()
    {
        cinematicCamera.enabled = false;
        gameplayCamera.enabled = true;
        GameManager.SetGameState(GameState.Playing);
        cinematicCanvas.ToggleBlackbars(false);
        UIManager.Instance.GetCanvas<GameCanvas>().Show();

    }

    private void OnValidate()
    {
        // if (cinematicCamera == null)
        // {
        //     cinematicCamera = GameObject.Find("CinematicCamera").GetComponent<CinemachineVirtualCamera>();
        // }
        //
        // if (gameplayCamera == null)
        // {
        //     gameplayCamera = GameObject.Find("GameplayCamera").GetComponent<CinemachineVirtualCamera>();
        // }
    }
}
