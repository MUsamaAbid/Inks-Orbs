using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OcularInk.Characters.Protagonist;
using TMPro;
using UnityEngine;

public class WheelSkill : MonoBehaviour
{
    [SerializeField] private SpriteRenderer frameSr;
    [SerializeField] private SpriteRenderer iconSr;
    [SerializeField] private Superpower superPower;
    [SerializeField] private TextMeshPro amountLabel;

    public SkillData SkillData => superPower.SkillData;
    
    public void Toggle(bool value)
    {
        frameSr.DOKill();
        iconSr.DOKill();
        if (value)
        {
            frameSr.DOFade(1f, 0.1f).SetUpdate(true);
            iconSr.DOFade(1f, 0.1f).SetUpdate(true);
        }
        else
        {
            frameSr.DOFade(0.4f, 0.1f).SetUpdate(true);
            iconSr.DOFade(0.4f, 0.1f).SetUpdate(true);
        }

        UpdateView();
    }

    public void Activate(Vector3 pos)
    {
        if (superPower.Amount <= 0)
            return;

        superPower.Consume();
        
        UpdateView();
        
        Instantiate(superPower, pos, Quaternion.identity);
    }

    public void UpdateView()
    {
        amountLabel.text = superPower.Amount.ToString();
    }
}
