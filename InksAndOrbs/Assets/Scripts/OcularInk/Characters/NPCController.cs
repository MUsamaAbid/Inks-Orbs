using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private float detectDistance = 5f;
    [SerializeField] private DialogueFlow dialogueFlow;
    [SerializeField] private GameObject alertIcon;
    
    private Transform player;

    private Quaternion startRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.PlayerController.transform;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        
        if (Vector3.Distance(transform.position, player.position) < detectDistance)
        {
            var lookDir = player.position;
            lookDir.y = transform.position.y;

            Quaternion targetRot = Quaternion.LookRotation(lookDir - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 2f);
            return;
        }
        
        transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.GetCanvas<GameCanvas>().ToggleInteractButton(true);
            GameManager.Instance.DialogueController.LoadDialogue(dialogueFlow);
            GameManager.Instance.DialogueController.onComplete += OnDialogueComplete;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.GetCanvas<GameCanvas>().ToggleInteractButton(false);
            GameManager.Instance.DialogueController.Terminate();
            GameManager.Instance.DialogueController.onComplete -= OnDialogueComplete;
        }
    }

    private void OnDialogueComplete()
    {
        alertIcon.SetActive(false);
    }
}
