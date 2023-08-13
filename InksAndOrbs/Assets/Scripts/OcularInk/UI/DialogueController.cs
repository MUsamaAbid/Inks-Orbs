using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueLabel;
    [SerializeField] private Image dialogueBackdrop;

    [Header("Dialogue Settings")] [SerializeField]
    private float durationRate = 0.25f;

    [SerializeField] private float minDuration = 1.5f;
    [SerializeField] private float maxDuration = 8f;

    private DialogueFlow currentDialogue;

    // Dialogue iteration
    private int dialogueIndex;
    private bool isDialogueOn;
    private float nextDialogueTime;

    public UnityAction onComplete;

    void Start()
    {
        GameManager.Instance.DialogueController = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ForceNextDialogue();
        
        if (!isDialogueOn)
            return;

        if (Time.time > nextDialogueTime)
        {
            IterateDialogue();
        }
    }

    public void LoadDialogue(DialogueFlow flow)
    {
        if (currentDialogue == flow)
        {
           
        }
        
        currentDialogue = flow;
    }

    public void ShowDialogue()
    {
        if (isDialogueOn)
        {
            ForceNextDialogue();
            return;
        }
        
        // Reset iteration values
        dialogueIndex = -1;
        
        dialogueBackdrop.DOKill();
        dialogueBackdrop.DOFade(0.9f, 0.25f);

        isDialogueOn = true;
    }

    public void ForceNextDialogue()
    {
        IterateDialogue();
    }

    public void Terminate()
    {
        dialogueLabel.DOKill();
        dialogueLabel.DOFade(0f, 0.25f);

        dialogueBackdrop.DOKill();
        dialogueBackdrop.DOFade(0f, 0.25f);

        isDialogueOn = false;
    }

    private void IterateDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex >= currentDialogue.Lines.Length)
        {
            Terminate();
            onComplete?.Invoke();
            return;
        }

        dialogueLabel.DOKill();
        dialogueLabel.DOFade(1f, 0.25f);
        var line = currentDialogue.Lines[dialogueIndex];
        var lineDuration = Mathf.Clamp(line.Length * durationRate, minDuration, maxDuration);

        dialogueLabel.text = line;

        dialogueLabel.DOFade(0f, 0.25f).SetDelay(lineDuration - 0.25f);
        nextDialogueTime = Time.time + lineDuration;
    }

    // private IEnumerator IterateDialogue(DialogueFlow flow)
    // {
    //     for (int i = 0; i < flow.Lines.Length; i++)
    //     {
    //         dialogueLabel.DOKill();
    //         dialogueLabel.DOFade(1f, 0.25f);
    //         var line = flow.Lines[i];
    //         var lineDuration = Mathf.Clamp(line.Length * durationRate, minDuration, maxDuration);
    //
    //         dialogueLabel.text = line;
    //
    //         dialogueLabel.DOFade(0f, 0.25f).SetDelay(lineDuration - 0.25f);
    //         yield return new WaitForSeconds(lineDuration);
    //     }
    // }
}