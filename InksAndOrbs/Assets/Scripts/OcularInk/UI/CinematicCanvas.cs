using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CinematicCanvas : CanvasController
{
    [SerializeField] private Animator blackBar;
    [SerializeField] private TextMeshProUGUI messageLabel;

    public void ToggleBlackbars(bool value)
    {
        blackBar.SetBool("Show", value);
    }
    
    public void ShowLine(string message)
    {
        messageLabel.DOFade(1f, 0.3f);
        messageLabel.text = message;
        
        CancelInvoke(nameof(HideLine));
        
        Invoke(nameof(HideLine), Mathf.Clamp((float)message.Length / 8f, 2f, 10f));
    }

    public void HideLine()
    {
        messageLabel.DOFade(0f, 0.3f);
    }
}
