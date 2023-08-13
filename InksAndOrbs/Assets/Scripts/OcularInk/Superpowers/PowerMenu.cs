using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using OcularInk.Characters.Protagonist;
using TMPro;
using UnityEngine;

public class PowerMenu : MonoBehaviour
{
    [SerializeField] private Superpower[] superpowers;
    [SerializeField] private GameObject powerMenu;
    [SerializeField] private GameObject selectAreaMenu;
    [SerializeField] private TextMeshProUGUI descLabel;
    [SerializeField] private TextMeshProUGUI[] amountLabels;
    [SerializeField] private CanvasGroup gameUI;
    [SerializeField] private CanvasGroup selfUI;
    [SerializeField] private GameObject selectMarker;
    [SerializeField] private Animator[] animators;
    [SerializeField] private GameObject powerButton;
    [SerializeField] private GameObject[] skills;

    private int selected = -1;

    private int state;

    private BrushController brush;

    private int up;

    private void OnEnable()
    {
        selfUI.DOFade(1f, 0.3f).From(0f).SetUpdate(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateView();
        brush = GameManager.Instance.PlayerController.brushController;

        var unlockedPowers = GameManager.GameData.UnlockedSuperpowers;
        var isActive = unlockedPowers.Any(superpower => superpower);

        powerButton.SetActive(isActive);
        
        for (var i = 0; i < skills.Length; i++)
        {
            var skill = skills[i];
            skill.SetActive(unlockedPowers[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isMouseUp = Input.GetMouseButtonUp(0);
        if (isMouseUp && selected >= 0 && state == 1)
        {
            UseSuperpower();
        }

        if (state == 2)
        {
            selectMarker.transform.position = brush.transform.position;

            if (isMouseUp)
            {

                up++;

                if (up <= 1)
                    return;
                
                UseAreaSuperpower();
            }
        }
    }

    public void ToggleMenu(bool value)
    {
        up = 0;
        state = value ? 1 : 0;
        UpdateView();
    }

    public void UseSuperpower()
    {
        var power = superpowers[selected];

        if (power.Amount <= 0)
            return;
        
        UpdateView();

        if (power.Type == Superpower.SuperpowerType.Area)
        {
            brush.CenterBrush();
            state = 2;
            UpdateView();
        }
        else
        {
            var powerInstance = Instantiate(power);
            powerInstance.Activate();

            GameManager.GameData.ConsumeSuperpower(selected);
            
            selected = -1;
            state = 0;
            UpdateView();
        }
        
    }

    private void UseAreaSuperpower()
    {
        var power = superpowers[selected];

        if (power.Amount <= 0)
            return;

        var powerInstance = Instantiate(power, selectMarker.transform.position, Quaternion.identity);
        powerInstance.Activate();

        GameManager.GameData.ConsumeSuperpower(selected);
        
        selected = -1;
        state = 0;

        UpdateView();
    }

    public void HighlightSuperpower(int index)
    {
        var amount = GameManager.GameData.Superpowers;
        if (amount[index] <= 0)
            return;
        
        selected = index;
        descLabel.text = superpowers[index].SkillData.Description;
        
        for (var i = 0; i < animators.Length; i++)
        {
            var animator = animators[i];
            
            animator.SetTrigger(selected == i ? "Highlight" : "Normal");
        }
    }

    public void ResetSelected()
    {
        selected = -1;
    }

    private void UpdateView()
    {
        var amount = GameManager.GameData.Superpowers;
        
        for (var i = 0; i < amountLabels.Length; i++)
        {
            var label = amountLabels[i];
            label.text = amount[i].ToString();
        }

        gameUI.alpha = state == 0 ? 1 : 0;
        gameUI.interactable = state == 0;
        gameUI.blocksRaycasts = state == 0;
        powerMenu.SetActive(state == 1);
        selectAreaMenu.SetActive(state == 2);
        selectMarker.SetActive(state == 2);
        
        Time.timeScale = state == 0 ? 1 : 0;
    }
}
