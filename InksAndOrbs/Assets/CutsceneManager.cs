using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator brushAnimator;
    //[SerializeField] Camera cam1,cam2;
    [SerializeField] PlayableDirector playableDirector;

    private void Start()
    {
        //PlayerPrefs.SetInt("Forest_cutscene", 0);
    }
    private void OnEnable()
    {
        playableDirector.stopped += OnPlayerDirectorStopped;
    }

    private void OnDisable()
    {
        playableDirector.stopped -= OnPlayerDirectorStopped;
    }
    private void OnPlayerDirectorStopped(PlayableDirector obj)
    {
        playerAnimator.enabled = false;
        brushAnimator.enabled = false;
    }

    public void StartCutscene()
    {
        Debug.Log("Play CutScene");
        playerAnimator.enabled = true;
        brushAnimator.enabled = true;
        //cam1.gameObject.SetActive(true);
        //cam2.gameObject.SetActive(true);
        playableDirector.Play();
    }

    //private void Update()
    //{
    //    if(playableDirector.stopped)
    //    {
    //        playerAnimator.enabled = false;
    //        brushAnimator.enabled = false;
    //    }
        
    //}

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triigger Enter: " + other.gameObject.name);

        if (other.gameObject.tag == "Player")
        {
           
            Debug.Log("PlAYER");
            if (PlayerPrefs.GetInt("Forest_cutscene", 0) == 0)
            {
                Debug.Log("CutScene");
                PlayerPrefs.SetInt("Forest_cutscene", 1);
                StartCutscene();
            }
            else
            {
                Debug.Log("CutScene Played already");
            }
        }
    }

    
}
